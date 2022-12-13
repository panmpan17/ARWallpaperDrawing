using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPresent : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRendererPrefab;

    private List<GameObject> _spriteRenderers = new List<GameObject>();

    public void ClearSpriteRenderers()
    {
        for (int i = 0; i < _spriteRenderers.Count; i++)
        {
            Destroy(_spriteRenderers[i].gameObject);
        }
        _spriteRenderers.Clear();
    }

    public void PresentFloatingImage(RectTransform rectTransform, List<ResizableImage> resizableImages)
    {
        ClearSpriteRenderers();

        for (int i = 0; i < resizableImages.Count; i++)
        {
            ResizableImage resizableImage = resizableImages[i];

            Vector3 scaleToImage = resizableImage.RectTransform.sizeDelta / rectTransform.sizeDelta;

            SpriteRenderer spriteRenderer = Instantiate<SpriteRenderer>(spriteRendererPrefab, transform);
            spriteRenderer.sprite = resizableImage.Sprite;

            Transform _transform = spriteRenderer.transform;

            Vector3 localPosition = resizableImage.RectTransform.anchoredPosition / rectTransform.sizeDelta;
            localPosition.z = -0.1f;
            _transform.localPosition = localPosition;
            _transform.localScale = scaleToImage;
            spriteRenderer.gameObject.SetActive(true);

            _spriteRenderers.Add(spriteRenderer.gameObject);
        }
    }
}
