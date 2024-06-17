using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_InteractGroup;

    public void DisplayPromt()
    {
        m_InteractGroup.alpha = 1.0f;
    }

    public void HidePrompt()
    {
        m_InteractGroup.alpha = 0f;
    }


   
}
