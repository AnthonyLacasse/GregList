using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class InputManager : MonoBehaviour
{
    private static InputManager _Instance;

    public static InputManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Instance = this;
        }

        playerMovement = new PlayerMovement();     
        
    }

    private void OnEnable()
    {
        playerMovement.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerMovement.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerMovement.Player.Look.ReadValue<Vector2>();
    }



}
