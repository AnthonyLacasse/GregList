using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour, Interactable
{
    [SerializeField] private HUD m_HUD;
    private Color m_InitialNoteColor;

    private void Start()
    {
        m_InitialNoteColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else //off
        {
            GetComponent<Renderer>().material.color = m_InitialNoteColor;
        }
    }

    public void Use()
    {
        RulesManager.Instance.SetListCollected();
        RulesManager.Instance.GetActiveRule().Init();
        m_HUD.DisplayNote();
        Destroy(gameObject);
    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.DISPOSABLE;
    }

    public bool CanInteract()
    {
        return true;
    }
}
