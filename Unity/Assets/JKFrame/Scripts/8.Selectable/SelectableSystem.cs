using System.Collections.Generic;
using UnityEngine;

public static class SelectableSystem
{
    static Stack<SelectableGroup> m_SelectableGroups = new Stack<SelectableGroup>();

    public static void AddSelectable(SelectableGroup selectableGroup, int defaultIndex = 0) 
    {
        Lock = true;
        m_SelectableGroups.Push(selectableGroup);
        UnityEngine.EventSystems.EventSystem sys = UnityEngine.EventSystems.EventSystem.current;
        if (selectableGroup.Selectables.Count > 0)
        {
            GameObject go = selectableGroup.Selectables[defaultIndex].gameObject;
            sys.SetSelectedGameObject(go);
        }
    }

    public static void Return()
    {
        m_SelectableGroups.Pop();
        SelectableGroup selectableGroup = m_SelectableGroups.Peek();
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
