using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private Transform m_Player;

    private float m_CameraVerticalRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * m_MouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * m_MouseSensitivity;

        m_CameraVerticalRotation -= inputY * Time.deltaTime;
        m_CameraVerticalRotation = Mathf.Clamp(m_CameraVerticalRotation, -45f, 45f);
        transform.localEulerAngles = Vector3.right * m_CameraVerticalRotation;

        m_Player.Rotate(Vector3.up * inputX * Time.deltaTime);
    }
}
