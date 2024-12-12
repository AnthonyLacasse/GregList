using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
    [SerializeField] private List<Light> m_Lights;

    private Color initialColor;
    private bool m_CanInteract = false;
    private bool m_LightsOn = true; // Track light state

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
        else // Reset to off color
        {
            GetComponent<Renderer>().material.color = initialColor;
        }
    }

    public void Use()
    {
        if (m_CanInteract)
        {
            AudioManager.Instance.PlaySound(EClipType.LIGHTSWITCH);

            // Toggle light state
            m_LightsOn = !m_LightsOn;

            foreach (Light light in m_Lights)
            {
                light.enabled = m_LightsOn;
            }

            // Allow interaction only if lights are off
            m_CanInteract = m_LightsOn;

            RulesManager.Instance.GetActiveRule().OnRuleObjectUsed(this);
        }
    }

    public void TurnLightsOn()
    {
        // Explicitly turn lights on
        m_LightsOn = true;
        foreach (Light light in m_Lights)
        {
            light.enabled = true;
        }
        m_CanInteract = true;
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
