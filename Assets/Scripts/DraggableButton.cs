using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableButton : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Vector2 _offset;

    public event System.Action<PointerEventData> OnDragEvent;

    protected override void Awake()
    {
        base.Awake();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = eventData.position - (Vector2)_rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position - _offset;
        OnDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
