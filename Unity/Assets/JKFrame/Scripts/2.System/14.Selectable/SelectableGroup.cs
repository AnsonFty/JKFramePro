using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableGroup : MonoBehaviour
{
    /// <summary>
    /// 使用返回时根据m_CurrentGameObject来判断返回时导航到哪一个Selectable，如果固定只导航一个，那么只拖入一个即可
    /// </summary>
    public List<Selectable> Selectables;
    /// <summary>
    /// 如果使用返回时到上一个界面触发的委托
    /// </summary>
    public Action ActionReturn;
    GameObject m_CurrentGameObject;
    bool m_Lock;

    public GameObject CurrentGameObject
    {
        get => m_CurrentGameObject;
        set
        {
            if (m_CurrentGameObject != value)
                m_CurrentGameObject = value;
        }
    }

    public bool Lock
    {
        get => m_Lock;
        set 
        {
            if (m_Lock == value) return;
            m_Lock = value;
            if (value)
                EventSystem.current.SetSelectedGameObject(null);
            else
                Invoke("SetDefaultSelected", 0.001f);
        }
    }

    void SetDefaultSelected()
    {
        if (!Lock)
        {
            EventSystem sys = EventSystem.current;
            if (m_CurrentGameObject != null && m_CurrentGameObject.activeInHierarchy)
                sys.SetSelectedGameObject(CurrentGameObject);
            else
            {
                foreach (var selectable in Selectables)
                {
                    if (selectable.gameObject.activeInHierarchy)
                    {
                        sys.SetSelectedGameObject(selectable.gameObject);
                        break;
                    }
                }
            }
        }
    }
}
