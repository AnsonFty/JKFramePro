using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class SelectableSystem
{
    static Stack<SelectableGroup> m_SelectableGroups = new Stack<SelectableGroup>();

    public static void AddSelectable(SelectableGroup selectableGroup, int defaultIndex = 0) 
    {
        Lock = true;
        m_SelectableGroups.Push(selectableGroup);
        if (selectableGroup.Selectables.Count > 0)
        {
            GameObject go = selectableGroup.Selectables[defaultIndex].gameObject;
            EventSystem.current.SetSelectedGameObject(go);
        }
    }

    public static void Return()
    {
        m_SelectableGroups.Pop();
        SelectableGroup selectableGroup = m_SelectableGroups.Peek();
        selectableGroup.ActionReturn?.Invoke();
        BaseToggle baseToggle = selectableGroup.CurrentGameObject.GetComponent<BaseToggle>();
        if (baseToggle != null)
            baseToggle.isOn = false;
        selectableGroup.Lock = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(selectableGroup.CurrentGameObject);
    }

    public static bool Lock
    {
        set
        {
            if (m_SelectableGroups.Count > 0)
                m_SelectableGroups.Peek().Lock = value;
        }
    }

    public static void Clear()
    {
        m_SelectableGroups.Clear();
    }
}
