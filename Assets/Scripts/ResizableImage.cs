using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizableImage : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;
    [SerializeField]
    private RectTransform resizeButton;

    public void ChangeImage(Sprite sprite)
    {
        mainImage.sprite = sprite;

        Vector2 size = new Vector2(sprite.rect.width, sprite.rect.height);
        mainImage.rectTransform.sizeDelta = size;
        resizeButton.anchoredPosition = size / 2;
    }
}
