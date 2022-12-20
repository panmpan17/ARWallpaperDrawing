using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawableTexture : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [field: SerializeField] public Vector2Int Size { get; protected set; }
    [SerializeField]
    private Color resetColor = new Color(1, 1, 1, 0);

    public Texture2D Texture { get; protected set; }

    private Color32[] _curColors;

    void Awake()
    {
        Texture = new Texture2D(Size.x, Size.y, TextureFormat.RGBA32, 1, true);

        // Set transparent color
        Color[] colors = new Color[Size.x * Size.y];
        Color transparent = new Color(1, 1, 1, 0);
        for (int i = 0; i < colors.Length; i++) colors[i] = resetColor;
        Texture.SetPixels(colors);
        Texture.Apply();

        if (meshRenderer)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetTexture("_MainTex", Texture);
            meshRenderer.SetPropertyBlock(block);
        }
    }

    public void UpdateCursorColor()
    {
        _curColors = Texture.GetPixels32();
    }


    public void ApplyMarkedPixelChanges()
    {
        Texture.SetPixels32(_curColors);
        Texture.Apply();
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
            if (x >= (int) Size.x || x < 0)
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
        int array_pos = y * (int) Size.x + x;

        // Check if this is a valid position
        if (array_pos > _curColors.Length || array_pos < 0)
            return;

        _curColors[array_pos] = color;
    }
}
