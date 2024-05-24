using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float m_MouseSensitivity = 2f;

    private float m_CameraVerticalRotation = 0f;
   

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        float yRotation = Input.GetAxisRaw("Mouse Y") * m_MouseSensitivity;

        m_CameraVerticalRotation -= yRotation;
        m_CameraVerticalRotation = Mathf.Clamp(m_CameraVerticalRotation, -90f, 90f);      

        transform.rotation = Quaternion.Euler(m_CameraVerticalRotation, 0, 0);
    }
}
