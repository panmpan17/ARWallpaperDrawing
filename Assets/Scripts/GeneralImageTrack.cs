using easyar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using ImageTracking_ImageTarget;

public class GeneralImageTrack : MonoBehaviour
{
    public ARSession Session;

    public ImageTargetController[] images;

    private Dictionary<ImageTargetController, bool> imageTargetControllers = new Dictionary<ImageTargetController, bool>();
    private ImageTargetController controllerNamecard;
    private ImageTargetController controllerIdback;
    private ImageTrackerFrameFilter imageTracker;
    private CameraDeviceFrameSource cameraDevice;


    void Awake()
    {
        imageTracker = Session.GetComponentInChildren<ImageTrackerFrameFilter>();
        cameraDevice = Session.GetComponentInChildren<CameraDeviceFrameSource>();

        // for (int i = 0; i < images.Length; i++)
        // {
        //     imageTargetControllers
        // }
    }
}
