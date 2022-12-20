using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainFooterControl : MonoBehaviour
{
    [SerializeField]
    private FooterButton[] footerButtons;
    private FooterButton _currentFooterButton;

    void Awake()
    {
        _currentFooterButton = footerButtons[0];
        foreach (FooterButton footerButton in footerButtons)
        {
            footerButton.Button.onClick.AddListener(delegate { OnFooterButtonClicked(footerButton); });
        }
    }

    void OnFooterButtonClicked(FooterButton footerButton)
    {
        _currentFooterButton.Panel.SetActive(false);
        _currentFooterButton.Button.interactable = true;

        _currentFooterButton = footerButton;
        _currentFooterButton.Panel.SetActive(true);
        _currentFooterButton.Button.interactable = false;
    }

    [System.Serializable]
    public class FooterButton
    {
        public GameObject Panel;
        public Button Button;
    }
}
