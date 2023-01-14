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
        // 窗口类型
        public Type Type { get { return GetType(); } }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// 显示前初始化
        /// </summary>
        public virtual void OnInit()
        {
            OnUpdateLanguage();
            RegisterEventListener();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void OnShow() { }

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