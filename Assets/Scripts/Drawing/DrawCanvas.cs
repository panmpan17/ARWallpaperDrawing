using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawCanvas : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField]
    private DrawModeControl drawModeControl;

    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private DrawableTexture drawableTexture;

    private RectTransform rectTransform;

    [SerializeField]
    private int brushSize = 10;
    // private Color[] brushColors;
    
    private Vector2Int _imageSizeInt;
    private Vector2 _previousPosition;



    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        _previousPosition = new Vector2(delta.x, delta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        Vector2 nextPosition = new Vector2(delta.x, delta.y);

        drawableTexture.UpdateCursorColor();
        drawableTexture.ColourBetween(_previousPosition, nextPosition, 10, drawModeControl.PenColor);
        drawableTexture.ApplyMarkedPixelChanges();

        _previousPosition = nextPosition;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        rawImage.texture = drawableTexture.Texture;
    }
}
