using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disposable : MonoBehaviour, Interactable
{
    Color m_InitialDisposableColor;

    private bool m_CanInteract = true;

    private void Start()
    {
        m_InitialDisposableColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange && m_CanInteract)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else //off
        {
            GetComponent<Renderer>().material.color = m_InitialDisposableColor;
        }
    }

    public void Use()
    {
        if (m_CanInteract)
        {
            m_CanInteract = false;
        }
    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.PLANT;
    }
}
