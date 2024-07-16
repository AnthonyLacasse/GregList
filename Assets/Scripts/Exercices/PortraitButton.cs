using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortraitButton : MonoBehaviour, Interactable
{
    [SerializeField] private TextMeshPro m_Text;
    
    Color m_InitialColor;


    private void Start()
    {
        m_InitialColor = GetComponent<Renderer>().material.color;

        m_Text.text = "Off";
    }

    public void InRange(bool inRange)
    {
        if (inRange)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else //off
        {
            GetComponent<Renderer>().material.color = m_InitialColor;
        }
    }

    public void Use()
    {
        RulesManager.Instance.SetPortaitMode(!RulesManager.Instance.GetPortraitMode());
        if(RulesManager.Instance.GetPortraitMode())
        {
            m_Text.text = "On";
        }
        else
        {
            m_Text.text = "Off";
        }

    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.DISPOSABLE;
    }
}
