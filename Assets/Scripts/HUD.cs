using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class HUD : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_InteractGroup;
    [SerializeField] private List<Sprite> m_Sprites;
    [SerializeField] private Image m_PromptImage;


    public void DisplayPrompt(InteractibleType type)
    {
        m_PromptImage.sprite = m_Sprites[(int)type]; 
        m_InteractGroup.alpha = 1.0f;
    }

    public void HidePrompt()
    {
        m_InteractGroup.alpha = 0f;
    }



}
