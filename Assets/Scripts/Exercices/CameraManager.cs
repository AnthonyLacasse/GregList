using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> m_Cameras;

    public void ActivateCamera(CinemachineVirtualCamera cameraToActivate)
    {
        foreach (CinemachineVirtualCamera camera in m_Cameras)
        {
            camera.enabled = camera == cameraToActivate;
        }
    }
}
