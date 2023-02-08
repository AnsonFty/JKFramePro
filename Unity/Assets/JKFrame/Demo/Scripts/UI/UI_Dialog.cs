using DG.Tweening;
using JKFrame;
using main;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[UIWindowData(typeof(UI_Dialog), true, "UI_Dialog", 0)]
public class UI_Dialog : UI_WindowBase
{
    [Serializable]
    class UISelect
    {
        public BaseButton BtnSelect;
        public Text TextSelect;
    }

    private const string DataBag = "Dialog";
    private const string Name = "Name";
    private const string Btn = "Button";

    [SerializeField] CanvasGroup m_CGroup;
    [SerializeField] GameObject m_ObjBtnNext;
    [SerializeField] GameObject m_ObjSelectPanel;
    [SerializeField] UISelect[] m_Selects;
    [SerializeField] Text m_NameText;
    [SerializeField] Text m_ContentText;
    [SerializeField] Text m_SelectNameText;
    [SerializeField] Text m_AsidePanelText;
    [SerializeField] Text m_AsideText;

    [SerializeField] float m_CharsPerSecond = 0.03f;//打字时间间隔
    List<string> m_Names;//一行名字字段列表
    List<string> m_Contents;//一行内容字段列表
    string m_CurrentContent;//打字机控制的文字内容
    List<Dialog> m_Dialogs;//一组对话列表

    int m_LineIndex;//一行对话的索引，代表读到m_NextIndex索引行的第几个
    int m_DialogIndex;//读到这组的第几行

    bool isPlay = false;//是否开始打字效果
    float timer;//计时器

    Text m_PlayText;//打字机控制的Text，打字完成会调用OnFinish方法
    int m_CurrentPos = 0;//当前打字位置

    UnityAction m_CloseCallBack;//外部传入的关闭回调
    UnityAction m_OnFinishCallBack;//内部使用的打字完成回调

    public override void OnShow()
    {
        base.OnShow();
        JKInputSystem.StartUI();
        ResetText();
        timer = 0;
        m_LineIndex = 0;
        m_DialogIndex = 0;
        m_CGroup.alpha = 0;
    }

    public override void OnClose()
    {
        base.OnClose();
        m_CloseCallBack?.Invoke();//关闭窗口时执行关闭回调
        m_Names = null;
        m_Contents = null;
        m_CloseCallBack = null;
        m_Dialogs = null;
        JKInputSystem.StartRole();
    }

    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="dialogID">对话配置表中头ID，XX_X为一组</param>
    /// <param name="closeCallBack">一组对话结束后的回调方法</param>
    public void Show(string dialogID, UnityAction closeCallBack)
    {
        m_CloseCallBack = closeCallBack;
        m_CharsPerSecond = Mathf.Max(0.01f, m_CharsPerSecond); //控制时间间隔最小是0.01
        SetDialog(dialogID);
        SetInfos();
        m_CGroup.DOFade(1, 0.1f);
    }

    /// <summary>
    /// 设置配置表中事件的内容存入临时存档
    /// </summary>
    /// <param name="event"></param>
    void SetEvent(string @event)
    {
        string[] eventInfos = @event.Split(',');
        var eventConfig = ConfigManager.Instance.SaveConfigTemp.EventConfig;
        foreach (var eventInfo in eventInfos)
        {
            if (eventInfo.Contains("NULL"))
                continue;
            else if (eventInfo.Contains("+"))
            {
                string[] kv = eventInfo.Split('+');
                eventConfig.SetEventValue(kv[0], eventConfig.GetEventValue(kv[0]) + int.Parse(kv[1]));
            }
            else if (eventInfo.Contains("-"))
            {
                string[] kv = eventInfo.Split('-');
                eventConfig.SetEventValue(kv[0], eventConfig.GetEventValue(kv[0]) - int.Parse(kv[1]));
            }
            else if (eventInfo.Contains("="))
            {
                string[] kv = eventInfo.Split('=');
                eventConfig.SetEventValue(kv[0], int.Parse(kv[1]));
            }
            else if (eventInfo.Contains("*"))
            {
                string[] kv = eventInfo.Split('*');
                eventConfig.SetEventValue(kv[0], eventConfig.GetEventValue(kv[0]) * int.Parse(kv[1]));
            }
            else if (eventInfo.Contains("/"))
            {
                string[] kv = eventInfo.Split('/');
                eventConfig.SetEventValue(kv[0], eventConfig.GetEventValue(kv[0]) / int.Parse(kv[1]));
            }
        }
    }

    /// <summary>
    /// 根据一组条件返回是否满足全部条件
    /// </summary>
    /// <param name="eventInfos">Dialog配置表中Condition条件字段</param>
    /// <returns>true为全部满足false为存在不满足</returns>
    bool ReadCondition(List<string> eventInfos)
    {
        var eventConfig = ConfigManager.Instance.SaveConfigTemp.EventConfig;
        foreach (string eventInfo in eventInfos)
        {
            if (eventInfo.Contains(">="))
            {
                string[] kv = eventInfo.Split(">=");
                if (eventConfig.GetEventValue(kv[0]) >= int.Parse(kv[1])) continue;
                return false;
            }
            else if (eventInfo.Contains("<="))
            {
                string[] kv = eventInfo.Split("<=");
                if (eventConfig.GetEventValue(kv[0]) <= int.Parse(kv[1])) continue;
                return false;
            }
            else if (eventInfo.Contains('>'))
            {
                string[] kv = eventInfo.Split('>');
                if (eventConfig.GetEventValue(kv[0]) > int.Parse(kv[1])) continue;
                return false;
            }
            else if (eventInfo.Contains('<'))
            {
                string[] kv = eventInfo.Split('<');
                if (eventConfig.GetEventValue(kv[0]) < int.Parse(kv[1])) continue;
                return false;
            }
            else if (eventInfo.Contains('='))
            {
                string[] kv = eventInfo.Split('=');
                if (eventConfig.GetEventValue(kv[0]) == int.Parse(kv[1])) continue;
                return false;
            }
        }
        return true;
    }

    void ResetText()
    {
        m_NameText.text = string.Empty;
        m_ContentText.text = string.Empty;
        m_SelectNameText.text = string.Empty;
        m_AsideText.text = string.Empty;
    }

    void SetNameText()
    {
        m_NameText.text = $"{LocalizationSystem.GetContentText(Name, m_Names[m_LineIndex])}：";
    }

    void SetContentText()
    {
        m_CurrentContent = LocalizationSystem.GetContentText(DataBag, m_PlayText == m_ContentText || m_PlayText == m_AsideText
            ? m_Contents[m_LineIndex] : m_Names[m_LineIndex]);
        if (m_PlayText == m_AsideText)
            m_AsidePanelText.text = m_CurrentContent;
    }

    /// <summary>
    /// 下一个按钮事件
    /// </summary>
    public void OnClickNext()
    {
        if (isPlay)
        {
            isPlay = false;
            OnFinish();
            return;
        }
        Next();
    }

    /// <summary>
    /// 选择项按钮事件
    /// </summary>
    /// <param name="index">第几个按钮</param>
    public void OnClickSelect(int index)
    {
        SetEvent(m_Dialogs[m_DialogIndex].DialogEvent[index]);
        Next();
    }

    /// <summary>
    /// 根据头ID设置m_Dialogs一组对话列表
    /// </summary>
    /// <param name="id">Dialog配置表头ID(XX_X)</param>
    void SetDialog(string id)
    {
        int index = 0;
        string dialogId = $"{id}_{index}";
        List<Dialog> dialogs = new List<Dialog>();
        Table table = TableSystem.Table;
        while (table.DialogByID.ContainsKey(dialogId))
        {
            dialogs.Add(table.DialogByID[dialogId]);
            dialogId = $"{id}_{++index}";
        }
        m_Dialogs = dialogs;
    }

    /// <summary>
    /// 根据对话类型设置内容，如果不满足条件则结束对话
    /// </summary>
    void SetInfos()
    {
        switch (m_Dialogs[m_DialogIndex].DialogType)
        {
            case DialogType.DIALOG:
                if (InitCondition) return;
                m_PlayText = m_ContentText;
                m_Names = m_Dialogs[m_DialogIndex].Name;
                SetNameText();
                m_Contents = m_Dialogs[m_DialogIndex].Content;
                SetContentText();
                isPlay = true;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_ObjBtnNext);
                break;
            case DialogType.ASIDE:
                if (InitCondition) return;
                m_PlayText = m_AsideText;
                m_Contents = m_Dialogs[m_DialogIndex].Content;
                SetContentText();
                isPlay = true;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_ObjBtnNext);
                break;
            case DialogType.SELECT:
                if (m_ObjSelectPanel.activeInHierarchy)
                    m_ObjSelectPanel.SetActive(false);
                if (!m_ObjBtnNext.activeInHierarchy)
                    m_ObjBtnNext.SetActive(true);
                for (int i = 0; i < m_Selects.Length; i++)
                    if (i < m_Dialogs.Count)
                        m_Selects[i].TextSelect.text = LocalizationSystem.GetContentText(Btn, m_Dialogs[m_DialogIndex].Content[i]);
                m_OnFinishCallBack = delegate
                {
                    m_OnFinishCallBack = null;//先删除回调防止重复调用
                    if (m_ObjBtnNext.activeInHierarchy)
                        m_ObjBtnNext.SetActive(false);
                    for (int i = 0; i < m_Selects.Length; i++)
                    {
                        if (i < m_Dialogs[m_DialogIndex].Content.Count)
                        {
                            m_Selects[i].BtnSelect.gameObject.SetActive(true);
                            continue;
                        }
                        m_Selects[i].BtnSelect.gameObject.SetActive(false);
                    }
                    if (!m_ObjSelectPanel.activeInHierarchy)
                        m_ObjSelectPanel.SetActive(true);
                    UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_Selects[0].BtnSelect.gameObject);//选中选择项第一个
                };
                m_Names = m_Dialogs[m_DialogIndex].Name;
                m_Contents = m_Names;//Next是根据m_Contents来判断行结束，赋值防止调用Next时继续自增，继续下一行内容
                m_PlayText = m_SelectNameText;
                SetContentText();
                isPlay = true;
                break;
        }
    }

    /// <summary>
    /// 进入行条件Condition判断通用方法和部分界面逻辑(文本显示类型通用)
    /// </summary>
    /// <returns>返回true时跳过后面逻辑</returns>
    bool InitCondition
    {
        get
        {
            if (!ReadCondition(m_Dialogs[m_DialogIndex].Condition))//如果条件不满足的判断
            {
                m_DialogIndex++;
                if (m_DialogIndex >= m_Dialogs.Count - 1)//如果一组对话已经读完关闭窗口
                {
                    Close();
                    return true;
                }
                SetInfos();//否则跳过此行递归进入下一行对话
                return true;
            }
            if (m_ObjSelectPanel.activeInHierarchy)//如果上一组选择项开着，关闭选择项窗口，清空内容
            {
                m_ObjSelectPanel.SetActive(false);
                m_SelectNameText.text = string.Empty;
            }
            if (!m_ObjBtnNext.activeInHierarchy)//如果下一步按钮关着则打开
                m_ObjBtnNext.SetActive(true);
            return false;
        }
    }

    void Update()
    {
        OnStartWriter();
    }

    /// <summary>
    /// 执行打字任务
    /// </summary>
    void OnStartWriter()
    {
        if (isPlay)
        {
            timer += Time.deltaTime;
            if (timer >= m_CharsPerSecond)
            {//判断计时器时间是否到达
                timer = 0;
                m_CurrentPos++;
                m_PlayText.text = m_CurrentContent.Substring(0, m_CurrentPos);//刷新文本显示内容

                if (m_CurrentPos >= m_CurrentContent.Length)
                {
                    OnFinish();
                }
            }

        }
    }

    void Next()
    {
        if (m_LineIndex >= m_Contents.Count - 1)//一行内容显示完成
        {
            m_DialogIndex++;
            if (m_DialogIndex < m_Dialogs.Count)//如果没到最后一行播放下一行
            {
                m_LineIndex = 0;
                ResetText();
                SetInfos();
                return;
            }
            m_CGroup.DOFade(0, 0.1f).OnComplete(Close);//到了最后一行关闭窗口
            return;
        }
        m_LineIndex++;//一行内容未显示完成播放下一索引内容
        SetNameText();
        SetContentText();
        isPlay = true;
    }

    /// <summary>
    /// 结束打字，初始化相关数据，执行完成回调
    /// </summary>
    void OnFinish()
    {
        isPlay = false;
        timer = 0;
        m_CurrentPos = 0;
        m_PlayText.text = m_CurrentContent;
        m_OnFinishCallBack?.Invoke();
    }
}
