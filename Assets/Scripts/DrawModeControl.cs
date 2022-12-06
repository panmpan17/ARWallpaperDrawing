using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawModeControl : MonoBehaviour
{
    [SerializeField]
    private Color[] colorPalettes;
    [SerializeField]
    private RectTransform selectIndicate;
    [SerializeField]
    private Image[] paletteButtons;

    public Color PenColor { get; protected set; }

    void Awake()
    {
        ChangeToPalette(0);

        for (int i = 0; i < colorPalettes.Length && i < paletteButtons.Length; i++)
        {
            paletteButtons[i].color = colorPalettes[i];
        }
    }

    void OnValidate()
    {
        for (int i = 0; i < colorPalettes.Length && i < paletteButtons.Length; i++)
        {
            paletteButtons[i].color = colorPalettes[i];
        }
    }

    public void ChangeToPalette(int index)
    {
        PenColor = colorPalettes[index];
        selectIndicate.anchoredPosition = paletteButtons[index].rectTransform.anchoredPosition;
    }

    public void ChangeToEraser()
    {
        PenColor = new Color(1, 1, 1, 0);
    }
}
