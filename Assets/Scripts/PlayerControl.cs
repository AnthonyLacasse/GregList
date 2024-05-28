using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float m_Speed = 6.0F;
    [SerializeField] public float m_CrounchedSpeed = 3.5f;
    [SerializeField] public float m_JogSpeed = 9f;


    private Vector3 m_MoveDirection = Vector3.zero;
    private CharacterController m_Controller;
    private InputManager m_InputManager;
    private Transform m_PlayerView;

    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_InputManager = InputManager.Instance;
        m_PlayerView = Camera.main.transform;
    }

    void Update()
    {
        float speed = m_Speed;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = m_CrounchedSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = m_JogSpeed;
        }


        Vector2 movement = m_InputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = m_PlayerView.forward * move.z + m_PlayerView.right * move.x;
        move.y = 0;
        m_Controller.Move(move * speed * Time.deltaTime);
    }
}
