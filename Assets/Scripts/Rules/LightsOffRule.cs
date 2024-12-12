using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class LightsOffRule : Rule
{
    [SerializeField] private int m_LuckyNumber;

    private List<LightSwitch> m_Switches;
    private List<Portrait> m_Portraits;

    private List<LightSwitch> m_TurnedOffForGood;

    public override void Init()
    {
        m_TurnedOffForGood = new List<LightSwitch>();
        m_Portraits = RulesManager.Instance.GetPortraits();
        foreach (Portrait portrait in m_Portraits)
        {
            portrait.PortraitGaze();
            //Player must dodge the portraits vision when this rule is active
        }

        m_Switches = RulesManager.Instance.Level.GetLevelLightswitches();
        foreach (LightSwitch switches in m_Switches)
        {
            switches.TurnLightsOn();
        }

        if (RulesManager.Instance.GetPlayer() != null)
        {
            RulesManager.Instance.GetPlayer().m_VisitingRoom += VisitedRoom;
        }
    }
    public override void CheckCompletion()
    {
        if (m_TurnedOffForGood.Count == m_Switches.Count)
        {
            End();
        }
    }

    public override void End()
    {
        RulesManager.Instance.GetPlayer().m_VisitingRoom -= VisitedRoom;
        RulesManager.Instance.RuleCompleted();
    }

    public override void OnRuleObjectUsed(LightSwitch light)
    //Call this function when you use the specific object for this rule. 
    //Generate a random number to see if the light will turn back on again or not.
    {
        int chance = Random.Range(0, 100);

        if (chance % m_LuckyNumber != 0)
        {
            m_TurnedOffForGood.Add(light);
        }
    }

    private void VisitedRoom(GameObject room)
    //Turn back on all lights not turned off for good
    {
        bool anyLightTurnedOn = false;

        foreach (LightSwitch light in m_Switches)
        {
            if (!m_TurnedOffForGood.Contains(light))
            {
                light.TurnLightsOn();
                anyLightTurnedOn = true;
            }
        }

        if (anyLightTurnedOn)
        {
            AudioManager.Instance.PlaySound(EClipType.LIGHTSWITCH);
        }

        CheckCompletion();
    }
}