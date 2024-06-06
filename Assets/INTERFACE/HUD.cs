using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Animator m_InteractAnimation;

    public void ChangeInteractPrompt(bool visibility)
    {
        m_InteractAnimation.SetBool("Visibility", visibility);
    }
}
