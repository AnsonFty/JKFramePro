using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;

public class BaseToggle : Toggle
{
    [SerializeField] bool m_EnterSelect = true;
    [SerializeField] bool m_UseClickScale = true;
    [SerializeField] bool m_IsOnHideBackGround;
    [SerializeField] SelectableGroup m_SelectableGroup;
    public SelectableGroup SelectableGroup => m_SelectableGroup;
    [SerializeField] MaskableGraphic[] m_ChangeSelectColors;
    [SerializeField] GameObject[] m_SelectObjs;

    [Serializable]
    public class SelectEvent : UnityEvent { }

    [SerializeField]
    private SelectEvent m_OnSelect = new SelectEvent();

    [SerializeField]
    private SelectEvent m_OnDeSelect = new SelectEvent();

    protected override void Awake()
    {
        base.Awake();
        if (m_IsOnHideBackGround)
            onValueChanged.AddListener(SetBackGround);
    }

    void SetBackGround(bool isOn)
    {
        if (isOn)
        {
            targetGraphic.color = Color.clear;
            return;
        }
        EventSystem sys = EventSystem.current;
        if (sys != null && sys.gameObject == this)
        {
            targetGraphic.color = colors.selectedColor;
            return;
        }
        targetGraphic.color = colors.normalColor;
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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (m_EnterSelect)
            Select();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (m_SelectableGroup != null)
            m_SelectableGroup.CurrentGameObject = gameObject;
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.selectedColor;
        foreach (var obj in m_SelectObjs)
            obj.SetActive(true);
        m_OnSelect?.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.normalColor;
        foreach (var obj in m_SelectObjs)
            obj.SetActive(false);
        m_OnDeSelect?.Invoke();
    }
}
