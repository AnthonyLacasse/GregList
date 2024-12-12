using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour, Interactable
{
    private EBookTitle m_Book;
    private PlayerControl m_Player;
    private Color initialColor;
    

    private bool m_IsReady = false;

    private void Start()
    {
         
        initialColor = GetComponent<Renderer>().material.color;
    }



    public void ShutDown()
    {
        m_IsReady = false;
    }
    public bool CanInteract()
    {
        return m_IsReady;
    }

    public void InRange(bool inRange)
    {
        if (inRange && m_IsReady)
        {
            GetComponent<Renderer>().material.color = Color.yellow;

        }
        else //off
        {
            GetComponent<Renderer>().material.color = initialColor;
        }
    }

    public void SetBook(EBookTitle book)
    {
        m_Book = book;
        m_Player = FindObjectOfType<PlayerControl>();
        m_IsReady = true;
    }

    public void Use()
    {
        if (m_Player != null && m_Player.HeldItem == EBookTitle.NONE)
        {
            m_Player.PickUpBook(m_Book);
            return;
        }
        else if (m_Player != null && m_Player.HeldItem != EBookTitle.NONE)
        {
            m_Player.StashBook();
        }

    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.BOOK;
    }
}
