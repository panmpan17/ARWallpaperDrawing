using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(DraggableButton))]
public class ResizeImageButton : MonoBehaviour
{
    [SerializeField]
    private RectTransform resizeRect;

    private DraggableButton _draggableButton;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _draggableButton = GetComponent<DraggableButton>();
        _draggableButton.OnDragEvent += OnDrag;
    }

    void OnDestory()
    {
        _draggableButton.OnDragEvent -= OnDrag;
    }

    void OnDrag(PointerEventData eventData)
    {
        Vector2 position = _rectTransform.anchoredPosition;
        resizeRect.sizeDelta = new Vector2(position.x * 2, position.y * 2);
    }
}
