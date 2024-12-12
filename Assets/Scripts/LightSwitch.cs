using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
    [SerializeField] private List<Light> m_Lights;

    private Color initialColor;
    private bool m_CanInteract = false;

    void Start()
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
        AudioManager.Instance.PlaySound(EClipType.LIGHTSWITCH);
        foreach (Light light in m_Lights)
        {
            light.enabled = false;
            m_CanInteract = false;
        }
        RulesManager.Instance.GetActiveRule().OnRuleObjectUsed(this);
    }

    public void TurnLightsOn()
    {
        foreach (Light light in m_Lights)
        {
            light.enabled = true;
            m_CanInteract = true;
        }
    }


    InteractibleType Interactable.GetType()
    {
        return InteractibleType.LIGHT;
    }

    public bool CanInteract()
    {
        return m_CanInteract;
    }
}
