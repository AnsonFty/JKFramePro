using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class BaseToggle : Toggle
{
    [SerializeField] bool m_EnterSelect = true;
    [SerializeField] bool m_IsOnHideBackGround;
    [SerializeField] MaskableGraphic[] m_ChangeSelectColors;
    public Action OnSelectCallBack;
    public Action OnDeselectCallBack;

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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (m_EnterSelect)
            Select();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.selectedColor;
        OnSelectCallBack?.Invoke();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.normalColor;
        OnDeselectCallBack?.Invoke();
    }
}
