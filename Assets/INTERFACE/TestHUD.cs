using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHUD : MonoBehaviour
{
    [SerializeField] private Animator m_InteractAnimation;

    public void ChangeInteractPrompt(bool visibility)
    {
        m_InteractAnimation.SetBool("Visibility", visibility);
    }
}
