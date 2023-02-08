using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseButton : Button
{
    [SerializeField] bool m_EnterSelect = true;
    [SerializeField] bool m_UseClickScale = true;
    [SerializeField] SelectableGroup m_SelectableGroup;
    [SerializeField] MaskableGraphic[] m_ChangeSelectColors;

    [Serializable]
    public class SelectEvent : UnityEvent { }

    [SerializeField]
    private SelectEvent m_OnSelect = new SelectEvent();

    [SerializeField]
    private SelectEvent m_OnDeSelect = new SelectEvent();

    public SelectableGroup SelectableGroup => m_SelectableGroup;
    public override void OnPointerEnter(PointerEventData eventData) 
    {
        base.OnPointerEnter(eventData);
        if (m_EnterSelect)
            Select();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (m_UseClickScale)
            transform.localScale = new Vector2(0.9f, 0.9f);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (m_UseClickScale)
            transform.localScale = Vector2.one;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (m_SelectableGroup != null)
            m_SelectableGroup.CurrentGameObject = gameObject;
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.selectedColor;
        m_OnSelect?.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.normalColor;
        m_OnDeSelect?.Invoke();
    }
}