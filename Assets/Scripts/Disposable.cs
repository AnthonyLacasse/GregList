using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disposable : MonoBehaviour, Interactable
{
    [SerializeField] private string m_Color;
    [SerializeField] private GameObject m_Child;
    
    Color m_InitialDisposableColor;

    private bool m_CanInteract = true;

    private void Start()
    {


        m_InitialDisposableColor = m_Child.GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange && m_CanInteract)
        {
            m_Child.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else //off
        {
            m_Child.GetComponent<Renderer>().material.color = m_InitialDisposableColor;
        }
    }

    public void Use()
    {
        RulesManager.Instance.GetActiveRule().OnRuleObjectUsed(this);
        AudioManager.Instance.PlaySound(EClipType.TRASH);
    }

    public string GetColor() { return m_Color; }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.DISPOSABLE;
    }
   

    public bool CanInteract()
    {
        return m_CanInteract;
    }
}
