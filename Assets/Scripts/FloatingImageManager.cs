using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingImageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject resizableImage;
    // [SerializeField]
    // private Sprite[] avalibleImages;

    // public void AddImage(int index)
    // {}

    public void AddImage(Sprite sprite)
    {
        GameObject newObj = Instantiate(resizableImage, transform);
        newObj.GetComponent<ResizableImage>().ChangeImage(sprite);
    }
}
