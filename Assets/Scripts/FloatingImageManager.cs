using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingImageManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform imageRectTransform;
    [SerializeField]
    private DrawingPresent drawingPresent;
    [SerializeField]
    private GameObject resizableImage;
    
    private List<ResizableImage> _resizableImages;

    void Awake()
    {
        _resizableImages = new List<ResizableImage>();
    }

    public void AddImage(Sprite sprite)
    {
        GameObject newObj = Instantiate(resizableImage, transform);
        var image = newObj.GetComponent<ResizableImage>();
        image.ChangeImage(sprite);

        _resizableImages.Add(image);
    }

    public void Save()
    {
        drawingPresent.PresentFloatingImage(imageRectTransform, _resizableImages);
    }
}
