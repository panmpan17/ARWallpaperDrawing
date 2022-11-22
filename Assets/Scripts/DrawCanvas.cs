using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawCanvas : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private Color resetColor;
    [SerializeField]
    private Color penColor;

    private Texture2D _drawTexture;
    private RectTransform rectTransform;

    [SerializeField]
    private int brushSize = 10;
    private Color[] brushColors;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        float size = rectTransform.sizeDelta.x * rectTransform.localScale.x;
        Vector2Int position = new Vector2Int(
            Mathf.FloorToInt(delta.x),
            Mathf.FloorToInt(delta.y));

        _drawTexture.SetPixels(position.x, position.y, brushSize, brushSize, brushColors);
        _drawTexture.Apply();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - (Vector2)transform.position;
        float size = rectTransform.sizeDelta.x * rectTransform.localScale.x;
        Vector2Int position = new Vector2Int(
            Mathf.FloorToInt(delta.x),
            Mathf.FloorToInt(delta.y));

        _drawTexture.SetPixels(position.x, position.y, brushSize, brushSize, brushColors);
        _drawTexture.Apply();
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        Vector2 imageSize = rawImage.rectTransform.sizeDelta;
        Vector2Int imageSizeInt = new Vector2Int(Mathf.FloorToInt(imageSize.x), Mathf.FloorToInt(imageSize.y));
        // _drawTexture = new RenderTexture(imageSizeInt.x, imageSizeInt.y, 16, RenderTextureFormat.ARGB32);
        _drawTexture = new Texture2D(imageSizeInt.x, imageSizeInt.y, TextureFormat.RGBA32, 1, true);
        rawImage.texture = _drawTexture;


        // Debug.Log(imageSizeInt.x);
        // Debug.Log(imageSizeInt.y);
        // RenderTexture.active = _drawTexture;
        // Graphics.DrawTexture(new Rect(0, 0, imageSizeInt.x, imageSizeInt.y), Texture2D.whiteTexture);
        // Graphics.DrawTexture(new Rect(imageSizeInt.x / 2, imageSizeInt.y / 2, 10, 10), Texture2D.whiteTexture);


        // RenderTexture.active = null;
        Color[] colors = new Color[imageSizeInt.x * imageSizeInt.y];
        Color transparent = new Color(1, 1, 1, 0);
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = resetColor;
        }
        _drawTexture.SetPixels(colors);


        brushColors = new Color[brushSize * brushSize];
        for (int i = 0; i < brushColors.Length; i++)
        {
            brushColors[i] = penColor;
        }
        // _drawTexture.SetPixels(0, 0, 10, 10, colors);
        // _drawTexture.SetPixels(imageSizeInt.x - 10,imageSizeInt.y - 10, 10, 10, colors);
        _drawTexture.Apply();
    }
}
