using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, Interactable
{
    [SerializeField] private List<Light> m_Lights;

    private Color initialColor;

    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    public void InRange(bool inRange)
    {
        if (inRange)
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
        AudioManager.GetInstance().PlaySound(EClipType.LIGHTSWITCH);
        foreach (Light light in m_Lights)
        {
            light.enabled = !light.enabled;
        }
    }

    InteractibleType Interactable.GetType()
    {
       return InteractibleType.LIGHT;
    }

    public bool CanInteract()
    {
        return true;
    }
}
