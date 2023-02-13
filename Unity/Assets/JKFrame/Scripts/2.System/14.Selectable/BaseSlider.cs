using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseSlider : Slider
{
    [SerializeField] bool m_EnterSelect = true;
    [SerializeField] SelectableGroup m_SelectableGroup;
    [SerializeField] MaskableGraphic[] m_ChangeSelectColors;

    public SelectableGroup SelectableGroup => m_SelectableGroup;
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
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        foreach (var image in m_ChangeSelectColors)
            image.color = colors.normalColor;
    }
}
