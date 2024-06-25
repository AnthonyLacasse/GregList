using UnityEngine;


public class InputManager : MonoBehaviour
{
    private static InputManager m_Instance;

    public static InputManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    
    
    private PlayerMovement m_playerMovement;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }

        m_playerMovement = new PlayerMovement();     
        
    }

    private void OnEnable()
    {
        m_playerMovement.Enable();
    }

    private void OnDisable()
    {
        m_playerMovement.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return m_playerMovement.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return m_playerMovement.Player.Look.ReadValue<Vector2>();
    }



}
