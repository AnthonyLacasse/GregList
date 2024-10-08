using UnityEngine;

public class Button : MonoBehaviour, Interactable
{
    [SerializeField] private int m_Time;


    Color m_InitialColor;

    private void Start()
    {
        m_InitialColor = GetComponent<Renderer>().material.color;
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
        if (m_Time < 10)
        {
            RulesManager.Instance.ForwardTime(m_Time, 0);
        }
        else
        {
            RulesManager.Instance.ForwardTime(0, m_Time);
        }

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
