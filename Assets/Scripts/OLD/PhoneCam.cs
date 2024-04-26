using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCam : MonoBehaviour
{


   
    private WebCamTexture backCam;
    private Texture defaultBackground;
    public Quaternion baseRotation;
    public RawImage background;
    public Button cameraButton;

    // Start is called before the first frame update
    void Start()
    {
        cameraButton.onClick.AddListener(OpenCamera);
        baseRotation = transform.rotation;
    }

    public void OpenCamera()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            //camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                transform.rotation = baseRotation * Quaternion.AngleAxis(backCam.videoRotationAngle, Vector3.up);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back Camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

       
    }
}