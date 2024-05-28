using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_AssociatedCamera;

    private void OnTriggerEnter(Collider other)
    {
        CameraManager manager = FindObjectOfType<CameraManager>();
        manager?.ActivateCamera(m_AssociatedCamera);
    }

}
