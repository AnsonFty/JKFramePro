using JKFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableManager : Singleton<SelectableManager>
{
    Stack<SelectableGroup> SelectableGroups = new Stack<SelectableGroup>();

    public void AddSelectable(SelectableGroup selectableGroup, int defaultIndex = 0) 
    {
        Lock = true;
        SelectableGroups.Push(selectableGroup);
        UnityEngine.EventSystems.EventSystem sys = UnityEngine.EventSystems.EventSystem.current;
        if (selectableGroup.Selectables.Count > 0)
        {
            GameObject go = selectableGroup.Selectables[defaultIndex].gameObject;
            sys.SetSelectedGameObject(go);
        }
    }

    public void Return()
    {
        SelectableGroups.Pop();
        SelectableGroup selectableGroup = SelectableGroups.Peek();
        selectableGroup.Lock = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(selectableGroup.CurrentGameObject);
    }

    public bool Lock
    {
        set
        {
            if (SelectableGroups.Count > 0)
                SelectableGroups.Peek().Lock = value;
        }
    }

    public void Clear()
    {
        SelectableGroups.Clear();
    }
}
