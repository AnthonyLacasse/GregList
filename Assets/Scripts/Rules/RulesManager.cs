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
    [SerializeField] private List<Portrait> m_Portraits;
    [SerializeField] private HUD m_Hud;
    [SerializeField] private Level m_Level;

    [SerializeField] private GameObject m_Exit;

    [SerializeField] private PlayerControl m_Player;
    [SerializeField] private int m_MaxStrikes;

    private bool ListCollected = false;
    private bool Escape = true;

    private int m_CurrentRule;
    private int m_Strike;



    private int m_Hours;
    private int m_Minutes;

    private List<List<Transform>> RulesSpawnPositions = new();
    private List<Rule> m_CurrentSessionRules = new();
    private List<Portrait> runPortraits = new();

    public Level Level => m_Level;
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
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
        //DontDestroyOnLoad(gameObject); Ensure persistence across scenes if needed
        Initialize();
    }

    private void Initialize()
    {
        m_Hours = 8;
        m_Minutes = 0;
        float timeLine = m_Hours + (m_Minutes / 60);
        m_Sky.SetTimeline(timeLine);

        SetSessionRules();
        m_CurrentRule = 0;
        m_Strike = 0;
    }


    public void ForwardTime(int hours, int minutes)
    {
        m_Minutes += minutes;
        if (m_Minutes < 0)
        {
            m_Minutes += 60;
            m_Hours--;
        }
        if (m_Minutes >= 60)
        {
            m_Minutes -= 60;
            m_Hours++;
        }

        m_Hours += hours;
        if (m_Hours < 0) m_Hours += 24;
        if (m_Hours >= 24) m_Hours -= 24;

        float timeLine = m_Hours + (m_Minutes / 60f);
        Debug.Log($"The time is: {timeLine}");
        m_Sky.SetTimeline(timeLine);
    }

    private void SetSessionRules()
    {
        foreach (RulesByMoment rule in m_LevelRules)
        {
            if (rule.AvailableRules == null || rule.AvailableRules.Count == 0) continue;

            int selection = Random.Range(0, rule.AvailableRules.Count);
            m_CurrentSessionRules.Add(rule.AvailableRules[selection]);
        }
    }

    public void PlacePortraits()
    {
        List<Transform> spawnPoints = GetSpawnPoints(ERuleType.PORTRAITS);

        if (spawnPoints.Count < m_Portraits.Count)
        {
            Debug.LogWarning("Not enough spawn points for all portraits!");
            return;
        }

        List<Transform> spawnLocations = new List<Transform>();
        for (int i = 0; i < m_Portraits.Count; i++)
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            spawnLocations.Add(spawnPoints[randomPoint]);
            spawnPoints.RemoveAt(randomPoint);
        }

        int portrait = 0;
        foreach (Transform spawn in spawnLocations)
        {
            Portrait painting = Instantiate(m_Portraits[portrait], spawn);
            runPortraits.Add(painting);
            portrait++;
        }
    }

    public List<Transform> GetSpawnPoints(ERuleType type) { return m_Level.GetLevelTransforms(type); }

    public List<Portrait> GetPortraits() { return runPortraits; }
    
    public bool GetListCollected() { return ListCollected; }
    public void SetListCollected() { ListCollected = true; }
    public PlayerControl GetPlayer() { return m_Player; }

    public bool PlayerCanEscape() { return Escape; }
       

    public void Strike()
    {
        m_Strike++;
        /*if(m_Strike == 1)
        {
        afficher le m_HUD.TutoStrike();
         
         */
        AudioManager.Instance.PlaySound(EClipType.STRIKE);
        m_Hud.WriteInRed(m_CurrentRule);
        Debug.Log("Strike");
        if (m_Strike >= m_MaxStrikes)
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

    public Rule GetActiveRule() { return m_CurrentSessionRules[m_CurrentRule]; }

    public void LoseGame()
    {
        m_Player.LoseGame();
    }


}
