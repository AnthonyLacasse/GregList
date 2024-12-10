using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;
using UnityEngine.SceneManagement;

public enum EMomentOfDay
{
    MORNING,
    NOON,
    AFTERNOON,
    TWILIGHT,
    NIGHT,
    DAYBREAK,

    COUNT
}

[System.Serializable]
public struct RulesByMoment
{
    public ERuleType Type;
    public List<Rule> AvailableRules;
}


public class RulesManager : MonoBehaviour
{
    private static RulesManager m_Instance;

    [SerializeField] private AzureTimeController m_Sky;
    [SerializeField] private List<RulesByMoment> m_LevelRules;
    [SerializeField] private HUD m_Hud;
    [SerializeField] private Level m_Level;

    [SerializeField] private GameObject m_Exit;

    [SerializeField] private PlayerControl m_Player;
    [SerializeField] private int m_MaxStrikes;

    private bool ListCollected = false;
    private bool PortraitsActive = false;
    private bool TimeToRead = false;
    private bool Escape = true;

    private int m_CurrentRule;
    private int m_Strike;
   


    private int m_Hours;
    private int m_Minutes;

    private List<List<Transform>> RulesSpawnPositions = new();
    private List<Rule> m_CurrentSessionRules = new();


    public static RulesManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }

        m_Hours = 8;
        m_Minutes = 0;
        float timeLine = m_Hours + (m_Minutes / 60);

        m_Sky.SetTimeline(timeLine);

        SetSessionRules();
        m_CurrentRule = 0;
    }
    

    public void ForwardTime(int hour, int minutes)
    {

        m_Hours += hour;
        m_Minutes += minutes;


        if (m_Hours >= 24)
        {
            m_Hours = 0;
        }

        if (m_Minutes >= 60)
        {
            m_Minutes -= 60;
            m_Hours++;
        }

        float timeLine = m_Hours + (m_Minutes / 60);
        Debug.Log($"The time is : {timeLine}");
        m_Sky.SetTimeline(timeLine);
        
    }

    private void SetSessionRules()
    {
        foreach (RulesByMoment rule in m_LevelRules)
        {
            int selection = Random.Range(0, rule.AvailableRules.Count);
            m_CurrentSessionRules.Add(rule.AvailableRules[selection]);
        }
    }

    
    public List<Transform> GetSpawnPoints(ERuleType type) { return m_Level.GetLevelTransforms(type); }
       
    public bool GetPortraitMode()   { return PortraitsActive;  }
    public void SetPortaitMode(bool portraitBehaviour) {PortraitsActive = portraitBehaviour; }
    public bool GetListCollected() { return ListCollected; }
    public void SetListCollected() { ListCollected = true; }
    public void SetReadTime(bool isTime) { TimeToRead = isTime; }
    public bool GetReadTime() { return TimeToRead; }
    public PlayerControl GetPlayer() { return m_Player; }

    public bool PlayerCanEscape() { return Escape; }

    public List<Transform> GetRuleSpawnPoints() { return RulesSpawnPositions[m_CurrentRule]; }

    public void Strike()
    {
        m_Strike++;
        AudioManager.Instance.PlaySound(EClipType.STRIKE);
        m_Hud.WriteInRed(m_CurrentRule);
        Debug.Log("Strike");
        if(m_Strike >= m_MaxStrikes)
        {
            LoseGame();
        }
    }


    public void RuleCompleted()
    {
        m_Hud.CheckOff(m_CurrentRule);
        m_Hud.OnRuleCompleted();
        AudioManager.Instance.PlaySound(EClipType.COMPLETE_TASK);
        m_CurrentRule++;
        Debug.Log("RuleCompleted");

        if (m_CurrentRule == m_CurrentSessionRules.Count)
        {
            if (m_Strike > m_MaxStrikes)
            {
                LoseGame();
                return;
            }
        m_Exit.SetActive(false);
            return;
        }
        m_CurrentSessionRules[m_CurrentRule].Init();
    }

    public List<Rule> GetRules() { return m_CurrentSessionRules; }
    public string GetRulesDescriptions()
    {
        string descriptionList = "";

        foreach (Rule rule in m_CurrentSessionRules)
        {
            for (int i = 0; i < rule.ruleDescription.Count; i++)
            {
                descriptionList += rule.ruleDescription[i];
            }
        }
        return descriptionList;
    }

    public Rule GetActiveRule() {  return m_CurrentSessionRules[m_CurrentRule]; }

    public void LoseGame()
    {
        m_Player.LoseGame();
    }


}
