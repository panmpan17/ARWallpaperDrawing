using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DrawSceneCanvas : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Vector2Int canvasSize;
    [SerializeField]
    private Color resetColor = new Color(1, 1, 1, 0);

    [SerializeField]
    private Transform bottomLeft;
    [SerializeField]
    private Transform topRight;

    [SerializeField]
    private DrawModeControl drawModeControl;

    private Texture2D _drawTexture;
    private Vector3 _size;
    private Color32[] _curColors;
    private Vector2 _previousPosition;

    void Awake()
    {
        _drawTexture = new Texture2D(canvasSize.x, canvasSize.y, TextureFormat.RGBA32, 1, true);

        // Set transparent color
        Color[] colors = new Color[canvasSize.x * canvasSize.y];
        Color transparent = new Color(1, 1, 1, 0);
        for (int i = 0; i < colors.Length; i++) colors[i] = resetColor;
        _drawTexture.SetPixels(colors);
        _drawTexture.Apply();

        if (meshRenderer)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetTexture("_MainTex", _drawTexture);
            meshRenderer.SetPropertyBlock(block);
        }

        _size = topRight.position - bottomLeft.position;
    }

    Vector2 ConvertRaycastPointToCanvasPosition(Vector3 position)
    {
        // Vector3 delta = position - bottomLeft.position;
        // Vector2 uvPosition = new Vector2(delta.x / _size.x, delta.y / _size.y);
        // return new Vector2(uvPosition.x * canvasSize.x, uvPosition.y * canvasSize.y);

        Vector3 localPoint = transform.InverseTransformPoint(position);
        Vector3 delta = localPoint - bottomLeft.localPosition;
        // Vector2 uvPosition = new Vector2(delta.x / _size.x, delta.y / _size.y);
        return new Vector2(delta.x * canvasSize.x, delta.y * canvasSize.y);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Vector3 position = eventData.pointerPressRaycast.worldPosition;

        // Vector3 delta = position - bottomLeft.position;
        // Vector2 uvPosition = new Vector2(delta.x /  _size.x, delta.y / _size.y);

        // Vector2 canvasPosition = new Vector2(uvPosition.x * canvasSize.x, uvPosition.y * canvasSize.y);
        // Debug.Log(canvasPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPosition = ConvertRaycastPointToCanvasPosition(eventData.pointerPressRaycast.worldPosition);;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 nextPosition = ConvertRaycastPointToCanvasPosition(eventData.pointerCurrentRaycast.worldPosition);

        _curColors = _drawTexture.GetPixels32();

        ColourBetween(_previousPosition, nextPosition, 10, drawModeControl.PenColor);
        ApplyMarkedPixelChanges();

        _previousPosition = nextPosition;
    }




    public void ApplyMarkedPixelChanges()
    {
        _drawTexture.SetPixels32(_curColors);
        _drawTexture.Apply();
    }

    public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
    {
        // Get the distance from start to finish
        float distance = Vector2.Distance(start_point, end_point);
        Vector2 direction = (start_point - end_point).normalized;

        Vector2 cur_position = start_point;

        // Calculate how many times we should interpolate between start_point and end_point based on the amount of time that has passed since the last update
        float lerp_steps = 1 / distance;

        for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
        {
            cur_position = Vector2.Lerp(start_point, end_point, lerp);
            MarkPixelsToColour(cur_position, width, color);
        }
    }

    public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
    {
        // Figure out how many pixels we need to colour in each direction (x and y)
        int center_x = (int)center_pixel.x;
        int center_y = (int)center_pixel.y;
        //int extra_radius = Mathf.Min(0, pen_thickness - 2);

        for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
        {
            // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
            if (x >= (int)canvasSize.x || x < 0)
                continue;

            for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
            {
                MarkPixelToChange(x, y, color_of_pen);
            }
        }
    }

    public void MarkPixelToChange(int x, int y, Color color)
    {
        // Need to transform x and y coordinates to flat coordinates of array
        int array_pos = y * (int)canvasSize.x + x;

        // Check if this is a valid position
        if (array_pos > _curColors.Length || array_pos < 0)
            return;

        _curColors[array_pos] = color;
    }
}
