using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float m_Speed = 6.0F;
    [SerializeField] public float m_CrounchedSpeed = 3.5f;
    [SerializeField] public float m_JogSpeed = 9f;

    private Interactable m_UsableObject;

    private Vector3 m_MoveDirection = Vector3.zero;
    private CharacterController m_Controller;
    private Rigidbody m_Rigidbody;
    private InputManager m_InputManager;
    private Transform m_PlayerView;

    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_InputManager = InputManager.Instance;
        m_PlayerView = Camera.main.transform;
    }

    void Update()
    {
        Move();
        UseObjects();
    }

    private void Move()
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
        m_Controller.Move(move * speed * Time.deltaTime);

    }

    private void UseObjects()
    {
        if (m_UsableObject != null)
        {
            m_UsableObject.InRange(false);
        }
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit info, 0.65f))
        {
            Interactable interactableHit = info.collider.GetComponent<Interactable>();

            if (interactableHit == null)
            {
               m_UsableObject = null;
            }
            else
            {
                m_UsableObject = interactableHit;
                m_UsableObject.InRange(true);
            }
        }
        if (m_UsableObject == null)
        {
            //Remove HUD prompt
        }
        else if (Input.GetMouseButtonDown(0))
        {
            m_UsableObject.Use();
            m_UsableObject = null;
            //Remove HUD prompt
        }
    }
}
