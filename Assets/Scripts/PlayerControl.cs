using System;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float m_Speed = 6.0F;
    [SerializeField] private float m_CrounchedSpeed = 3.5f;
    [SerializeField] private float m_JogSpeed = 9f;
    [SerializeField] private float m_RayLenght = 1.5f;
    [SerializeField] private Transform m_Head;
    [SerializeField] private HUD m_HUD;
    [SerializeField] LayerMask PlayerLayer;

    private Interactable m_UsableObject;
      
    private CharacterController m_Controller;

    private InputManager m_InputManager;
    private Transform m_PlayerView;
    private Vector3 m_InitialSize;
    private bool m_SeeNote = true;

    public Action<GameObject> m_VisitingRoom;


    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();

        m_InputManager = InputManager.Instance;
        m_PlayerView = Camera.main.transform;
        m_InitialSize = transform.localScale;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        UseObjects();
        ConsultNote();
    }

    private void Move()
    {
        float speed = m_Speed;
        Vector3 crouchedSize = m_InitialSize * 0.66f;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = m_CrounchedSpeed;
            transform.localScale = crouchedSize;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = m_InitialSize;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = m_JogSpeed;
        }

        Vector2 movement = m_InputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = m_PlayerView.forward * move.z + m_PlayerView.right * move.x;
        m_Controller.SimpleMove(move * speed);
    }



    private void UseObjects()
    {
        if (m_UsableObject != null)
        {
            m_UsableObject.InRange(false);
        }
        if (Physics.Raycast(m_Head.position, Camera.main.transform.forward, out RaycastHit info, m_RayLenght, ~PlayerLayer))
        {
            Interactable interactableHit = info.collider.GetComponent<Interactable>();

            if (interactableHit == null)
            {
                m_UsableObject = null;
            }
            else
            {
                m_UsableObject = interactableHit;
                if (interactableHit.CanInteract())
                m_UsableObject.InRange(true);
                m_HUD.DisplayPrompt(m_UsableObject.GetType());
            }
        }
        if (m_UsableObject == null)
        {
            m_HUD.HidePrompt();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            m_UsableObject.Use();
            m_UsableObject = null;
            m_HUD.HidePrompt();
        }
    }

    private void ConsultNote()
    {
        if (RulesManager.Instance.GetListCollected() && Input.GetKeyDown(KeyCode.Tab))
        {
            if (m_SeeNote)
            {
                m_SeeNote = false;
                m_HUD.HideNote();
            }
            else
            {
                m_SeeNote = true;
                m_HUD.DisplayNote();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Room"))
        {
            m_VisitingRoom?.Invoke(other.gameObject);
        }
    }

    public void LoseGame()
    {
        m_HUD.LoseGame();
    }

}
