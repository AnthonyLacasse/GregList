using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_WalkSpeed = 5f;
    [SerializeField] private float m_JogSpeed = 8f;
    [SerializeField] private float m_CrouchedSpeed = 3.5f;
    [SerializeField] private float m_MouseSensitivity = 2f;

    private Rigidbody m_Rigidbody;

    private float m_CameraHorizontalRotation = 0;

    private float m_MovementSpeed;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float forwardMovement = Input.GetAxis("Vertical");      

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MovementSpeed = m_JogSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            m_MovementSpeed = m_CrouchedSpeed;
        }
        else
        {
            m_MovementSpeed = m_WalkSpeed;
        }

        Vector3 movement = Vector3.zero;

        movement = m_Rigidbody.velocity;
        movement += transform.forward * forwardMovement * m_MovementSpeed;
        movement += transform.right * horizontalMovement * m_MovementSpeed;
        m_Rigidbody.velocity = movement.normalized;

        m_Rigidbody.AddForce(movement, ForceMode.VelocityChange);



        float xRotation = Input.GetAxisRaw("Mouse X") * m_MouseSensitivity;

        if (xRotation <= 0)
        {
            xRotation += 360;
        }

        m_CameraHorizontalRotation += xRotation;

        transform.rotation = Quaternion.Euler(0, m_CameraHorizontalRotation, 0);
    }


}
