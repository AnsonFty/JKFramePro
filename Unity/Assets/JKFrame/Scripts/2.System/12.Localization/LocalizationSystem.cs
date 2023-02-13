using System.IO;
using System;
using UnityEngine;

namespace JKFrame
{
    public enum LanguageType
    {
        SimplifiedChinese,
        English
    }

    /// <summary>
    /// 本地化管理器
    /// 持有本地化配置
    /// 提供本地化内容获取函数
    /// </summary>
    public static class LocalizationSystem
    {
        private static int LanguageIndexCount;

        public static void Init()
        {
            LanguageIndexCount = Enum.GetNames(CurrentLanguageType.GetType()).Length - 1;
        }

        private static LanguageType m_CurrentLanguageType;

        public static LanguageType CurrentLanguageType
        {
            get => m_CurrentLanguageType;
            set
            {
                m_CurrentLanguageType = value;
                UpdateLanguage();
            }
        }

        public static string GetContentText(string dataBag, string contentKey)
        {
            string bagName = $"{dataBag}|{contentKey}";
            switch (m_CurrentLanguageType)
            {
                case LanguageType.SimplifiedChinese:
                    return TableSystem.Table.LanguageByID[bagName].SimplifiedChinese;
                case LanguageType.English:
                    return TableSystem.Table.LanguageByID[bagName].English;
            }
            return string.Empty;
        }

        public static void SetInputLanguage(bool isActive)
        {
            if (isActive)
            {
                JKInputSystem.AddListenerLeftStickLeftRight(LastLanguage, NextLanguage);
                return;
            }
            JKInputSystem.RemoveLeftStickLeftRight();
        }

        public static void LastLanguage()
        {
            int LanguageIndex = (int)CurrentLanguageType;
            if (LanguageIndex - 1 < 0)
                LanguageIndex = LanguageIndexCount;
            else
                LanguageIndex--;
            CurrentLanguageType = (LanguageType)LanguageIndex;
        }

        public static void NextLanguage()
        {
            int LanguageIndex = (int)CurrentLanguageType;
            if (LanguageIndex + 1 > LanguageIndexCount)
                LanguageIndex = 0;
            else
                LanguageIndex++;
            CurrentLanguageType = (LanguageType)LanguageIndex;
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        static void UpdateLanguage()
        {
#if UNITY_EDITOR
            JKFrameRoot.InitForEditor();
#endif
            // 触发更新语言 事件
            JKEventSystem.EventTrigger("UpdateLanguage");
        }

    }
}
