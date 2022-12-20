using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using easyar;

public class ImageTrackerResponse : MonoBehaviour
{
    [SerializeField]
    private ImageTargetController targetController;

    public UnityEvent OnTrackStarted;
    public UnityEvent OnTrackLost;

    void Awake()
    {
        targetController.TargetFound += TrackStarted;
        targetController.TargetLost += TrackLost;
    }

    void TrackStarted()
    {
        OnTrackStarted.Invoke();
    }

    void TrackLost()
    {
        OnTrackLost.Invoke();
    }
}
