using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MPack;


public class StartMenu : MonoBehaviour
{
    // [SerializeField]
    // private RectTransform blackShield;
    // [SerializeField]
    // private Timer fadeTimer;

    [SerializeField]
    private string sceneName = "SampleScene";

    void Awake()
    {
        // fadeTimer.Running = false;
        // Debug.Log(blackShield.anchoredPosition);
    }

    // void Update()
    // {
    //     if (fadeTimer.Running)
    //     {
    //         blackShield.anchoredPosition = new Vector2(Mathf.Lerp(0, -blackShield.sizeDelta.x, fadeTimer.Progress), 0);

    //         if (fadeTimer.UpdateEnd)
    //         {
    //             fadeTimer.Running = false;
    //             SceneManager.LoadScene(sceneName);
    //         }
    //     }
    // }

    public void Trigger()
    {
        // fadeTimer.Reset();
        SceneManager.LoadScene(sceneName);
    }
}
