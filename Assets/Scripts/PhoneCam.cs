using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PhoneCam : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected :(");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            // 16/4/2024, Ren H - Removing access to front cam
            if (devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }
        
        if (backCam == null)
        {
            Debug.Log("Unable to find camera :(");
            return;
        }
        
        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }

        // 16/4/2024, Ren H - using float for more precision in the aspect ratio
        float ratio = (float)backCam.width / (float)backCam.height;

        float scaleY = backCam.videoVerticallyMirrored ? -1 : 1;
        background.rectTransform.localScale = new Vector3(1F, scaleY, 1F);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
