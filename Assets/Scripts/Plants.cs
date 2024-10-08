using UnityEngine;

public class Plant : MonoBehaviour, Interactable
{
    Color initialColor;

    public bool m_CanInteract = true;

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
            RulesManager.Instance.GetActiveRule().UseRuleObject();

            m_CanInteract = false;
        }
    }
     

    InteractibleType Interactable.GetType()
    {
        return InteractibleType.PLANT;
    }

    public bool CanInteract() { return m_CanInteract; }
}
