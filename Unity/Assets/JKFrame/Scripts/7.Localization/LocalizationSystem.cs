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
        public static main.Table Table;
        private static int LanguageIndexCount;

        public static void LoadAllTable()
        {
            LanguageIndexCount = Enum.GetNames(CurrentLanguageType.GetType()).Length - 1;
            using (var stream = new FileStream($"{Application.dataPath}/JKFrame/Table/table_gen.bin", FileMode.Open))
            {
                stream.Position = 0;

                var reader = new tabtoy.TableReader(stream);

                Table = new main.Table();

                try
                {
                    Table.Deserialize(reader);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw;
                }

                // 建立所有数据的索引
                Table.IndexData();
            }
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
                    return Table.LanguageByID[bagName].SimplifiedChinese;
                case LanguageType.English:
                    return Table.LanguageByID[bagName].English;
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
