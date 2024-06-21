using UnityEngine;

public class DisposableObject : MonoBehaviour, Interactable
{
    Color m_InitialDisposableColor;

    private void Start()
    {
        m_InitialDisposableColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else //off
        {
            GetComponent<Renderer>().material.color = m_InitialDisposableColor;
        }
    }

    public void Use()
    {
        //Implement trash counter
        Destroy(gameObject);
    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.DISPOSABLE;
    }
}
