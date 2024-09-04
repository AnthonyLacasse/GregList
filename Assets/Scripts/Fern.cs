
using UnityEngine;

public class Fern : MonoBehaviour, Interactable
{
    Color initialColor;

    private bool m_CanInteract = true;

    private void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange && m_CanInteract)
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
        if (m_CanInteract)
        {
            //Implement watering plant counter
            m_CanInteract = false;
            //RulesManager.Instance.GetActiveRule().RuleFollowed = false;
        }
    }

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.PLANT;
    }
}
