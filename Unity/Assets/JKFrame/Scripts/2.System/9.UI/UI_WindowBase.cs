using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JKFrame
{
    /// <summary>
    /// 窗口基类
    /// </summary>
    public class UI_WindowBase : MonoBehaviour
    {
        protected bool uiEnable;
        public bool UIEnable { get => uiEnable; }
        protected int currentLayer;
        public int CurrentLayer { get => currentLayer; }

        // 窗口类型
        public Type Type { get { return GetType(); } }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void OnShow()
        {
            uiEnable = true;
            OnUpdateLanguage();
            RegisterEventListener();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            UISystem.Close(Type);
        }
        /// <summary>
        /// 关闭时额外执行的内容
        /// </summary>
        public virtual void OnClose()
        {
            uiEnable = false;
            CancelEventListener();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        protected virtual void RegisterEventListener()
        {
            JKEventSystem.AddEventListener("UpdateLanguage", OnUpdateLanguage);
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        protected virtual void CancelEventListener()
        {
            JKEventSystem.RemoveEventListener("UpdateLanguage", OnUpdateLanguage);
        }
        protected virtual void OnUpdateLanguage() { }

        protected void ResetSelect()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}