using DG.Tweening;
using JKFrame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum TipType
{
    /// <summary>
    /// Information Dialog with OK button
    /// </summary>
    Information = 1,

    /// <summary>
    /// Confirm Dialog whit OK and Cancel buttons
    /// </summary>
    Confirm = 2,

    /// <summary>
    /// Warning Dialog whit OK and Cancel buttons
    /// </summary>
    Warning = 3,

    /// <summary>
    /// Error Dialog with OK buttons
    /// </summary>
    Error = 4
}

[UIWindowData(typeof(UI_Tip), true, "UI_Tip", 3)]
public class UI_Tip : UI_WindowBase
{
    private const string Btn = "Button";
    private const string DataBag = "TipInfo";
    [SerializeField] CanvasGroup m_CGroup;
    [SerializeField] Text m_Message;
    [SerializeField] GameObject m_ObjBtnConfirm;
    [SerializeField] GameObject m_ObjBtnError;
    [SerializeField] GameObject m_ObjBtnCancel;
    [SerializeField] Text m_TextBtnCofirm;
    [SerializeField] Text m_TextBtnError;
    [SerializeField] Text m_TextBtnCancel;
    [SerializeField] Text m_TextCancel;

    private event UnityAction m_OnYes = null;
    private event UnityAction m_OnNo = null;
    private event UnityAction m_OnClose = null;
    TipType m_TipType;

    public override void Init()
    {
        base.Init();
        m_CGroup.alpha = 0;
    }

    public void Show(string messageContentKey, TipType type = TipType.Information, UnityAction yesOnClick = null, UnityAction noOnClick = null,
        string textBtnYesContentKey = "Confirm", string textBtnNoContentKey = "Cancel", UnityAction closeOnClick = null)
    {
        m_TipType = type;
        SelectableSystem.Lock = true;
        bool hasCancel = type == TipType.Confirm || type == TipType.Warning;
        JKInputSystem.AddListenerSubmit(OnClickYes);
        switch (type)
        {
            case TipType.Information:
                m_TextBtnCofirm.text = LocalizationSystem.GetContentText(Btn, textBtnYesContentKey);
                break;
            case TipType.Confirm:
                m_TextBtnCofirm.text = LocalizationSystem.GetContentText(Btn, textBtnYesContentKey);
                break;
            case TipType.Warning:
                m_TextBtnError.text = LocalizationSystem.GetContentText(Btn, textBtnYesContentKey);
                break;
            case TipType.Error:
                m_TextBtnError.text = LocalizationSystem.GetContentText(Btn, textBtnYesContentKey);
                break;
        }
        if (hasCancel)
        {
            JKInputSystem.AddListenerCancel(OnClickNo);
            m_TextBtnCancel.text = LocalizationSystem.GetContentText(Btn, textBtnNoContentKey);
        }

        m_ObjBtnConfirm.SetActive(type == TipType.Confirm || type == TipType.Information);
        m_ObjBtnError.SetActive(type == TipType.Warning || type == TipType.Error);
        m_ObjBtnCancel.SetActive(hasCancel);

        m_Message.text = LocalizationSystem.GetContentText(DataBag, messageContentKey);
        if (yesOnClick != null) m_OnYes = yesOnClick;
        if (noOnClick != null) m_OnNo = noOnClick;
        if (closeOnClick != null)
        {
            m_OnClose = closeOnClick;
            JKInputSystem.AddListenerActionX(OnClickClose);
        }
        m_CGroup.DOFade(1, 0.5f);
    }

    public override void OnClose()
    {
        base.OnClose();
        m_CGroup.DOFade(0, 0.5f).OnComplete(delegate
        {
            SelectableSystem.Lock = false;
        });
    }

    public void RemoveInputListenter()
    {
        JKInputSystem.RemoveListenerSubmit(OnClickYes);
        if (m_TipType == TipType.Confirm || m_TipType == TipType.Warning)
            JKInputSystem.RemoveListenerCancel(OnClickNo);
        if (m_OnClose != null)
            JKInputSystem.RemoveListenerActionX(OnClickClose);
    }

    public void OnClickYes()
    {
        RemoveInputListenter();
        Close();
        m_OnYes?.Invoke();
        ResetAction();
    }

    public void OnClickNo()
    {
        RemoveInputListenter();
        Close();
        m_OnNo?.Invoke();
        ResetAction();
    }

    public void OnClickClose()
    {
        RemoveInputListenter();
        Close();
        if (m_OnClose != null)
        {
            m_OnClose.Invoke();
            return;
        }
        m_OnNo?.Invoke();
    }

    private void ResetAction()
    {
        if (m_OnYes != null) m_OnYes = null;
        if (m_OnNo != null) m_OnNo = null;
        if (m_OnClose != null) m_OnClose = null;
    }

    protected override void OnUpdateLanguage()
    {
        base.OnUpdateLanguage();
        m_TextCancel.text = LocalizationSystem.GetContentText(Btn, "Cancel");
    }
}
