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
    private Color resetColor;
    [SerializeField]
    private Renderer meshRenderer;

    private Texture2D _drawTexture;
    private RectTransform rectTransform;

    [SerializeField]
    private int brushSize = 10;
    // private Color[] brushColors;
    private Color32[] curColors;
    
    private Vector2Int _imageSizeInt;
    private Vector2 _previousPosition;



    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        float size = rectTransform.sizeDelta.x * rectTransform.localScale.x;
        Vector2Int position = new Vector2Int(
            Mathf.FloorToInt(delta.x),
            Mathf.FloorToInt(delta.y));

        _previousPosition = new Vector2(delta.x, delta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        float size = rectTransform.sizeDelta.x * rectTransform.localScale.x;
        Vector2Int position = new Vector2Int(
            Mathf.FloorToInt(delta.x),
            Mathf.FloorToInt(delta.y));

        curColors = _drawTexture.GetPixels32();

        Vector2 nextPosition = new Vector2(delta.x, delta.y);
        ColourBetween(_previousPosition, nextPosition, 10, drawModeControl.PenColor);
        ApplyMarkedPixelChanges();

        _previousPosition = nextPosition;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        Vector2 imageSize = rawImage.rectTransform.sizeDelta;
        _imageSizeInt = new Vector2Int(Mathf.FloorToInt(imageSize.x), Mathf.FloorToInt(imageSize.y));
        _drawTexture = new Texture2D(_imageSizeInt.x, _imageSizeInt.y, TextureFormat.RGBA32, 1, true);
        rawImage.texture = _drawTexture;

        // Set transparent color
        Color[] colors = new Color[_imageSizeInt.x * _imageSizeInt.y];
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
    }


    public void ApplyMarkedPixelChanges()
    {
        _drawTexture.SetPixels32(curColors);
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
            if (x >= (int)_imageSizeInt.x || x < 0)
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
        int array_pos = y * (int)_imageSizeInt.x + x;

        // Check if this is a valid position
        if (array_pos > curColors.Length || array_pos < 0)
            return;

        curColors[array_pos] = color;
    }
}
