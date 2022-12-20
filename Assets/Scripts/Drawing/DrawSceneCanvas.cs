using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DrawSceneCanvas : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private DrawableTexture drawableTexture;
    [SerializeField]
    private Transform bottomLeft;
    [SerializeField]
    private Transform topRight;

    [SerializeField]
    private DrawModeControl drawModeControl;

    private Vector3 _size;
    private Vector2 _previousPosition;

    void Awake()
    {
        _size = topRight.position - bottomLeft.position;
    }

    Vector2 ConvertRaycastPointToCanvasPosition(Vector3 position)
    {
        Vector3 localPoint = transform.InverseTransformPoint(position);
        Vector3 delta = localPoint - bottomLeft.localPosition;

        return new Vector2(delta.x * drawableTexture.Size.x, delta.y * drawableTexture.Size.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPosition = ConvertRaycastPointToCanvasPosition(eventData.pointerPressRaycast.worldPosition);;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 nextPosition = ConvertRaycastPointToCanvasPosition(eventData.pointerCurrentRaycast.worldPosition);

        drawableTexture.UpdateCursorColor();
        drawableTexture.ColourBetween(_previousPosition, nextPosition, 10, drawModeControl.PenColor);
        drawableTexture.ApplyMarkedPixelChanges();

        _previousPosition = nextPosition;
    }
}
