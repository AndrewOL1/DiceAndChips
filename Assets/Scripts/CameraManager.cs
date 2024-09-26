using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviourSingleton<CameraManager> 
{
    #region variables
    [Header("CameraManagerSettings")]
    [Space(5)]
    [Tooltip("List of Cameras.Order Aim TopDown Chase")]
    [SerializeField]List<CinemachineVirtualCamera> vCameras = new List<CinemachineVirtualCamera>();
    private CinemachineVirtualCamera currentCam;

    #endregion
    private void Start()
    {
        currentCam=vCameras[0];
    }
    public void SwitchToCamera(string s)
    {
        switch(s)
        {
            case "aim": ChangeCam(vCameras[0]); break;
            case "topDown": ChangeCam(vCameras[1]); break;
            case "chase": ChangeCam(vCameras[2]); break;
        }
    }
    private void ChangeCam(CinemachineVirtualCamera camera)
    {
        if (camera == null || camera !=currentCam)
        {
            if (camera.enabled == false)
            {
                camera.enabled = true;
                currentCam = camera;
            }
            else
            {
                currentCam.enabled = false;
            }
        }
    }
}
