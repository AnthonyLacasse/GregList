using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour, Interactable
{
    Color initialColor;

    private bool m_CanBeWatered = true;

    private void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange && m_CanBeWatered)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else //off
        {
            GetComponent<Renderer>().material.color = initialColor;
        }
    }

    public void Use()
    {
        if (m_CanBeWatered)
        {
            m_CanBeWatered = false;
        }
    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.PLANT;
    }
}
