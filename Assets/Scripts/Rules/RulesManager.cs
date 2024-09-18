using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;


public class RulesManager : MonoBehaviour
{
    private static RulesManager m_Instance;

    [SerializeField] private AzureTimeController m_Sky;
    [SerializeField] private List<Rule> m_Rules;

    [SerializeField] private GameObject m_Exit;

    [SerializeField] private PlayerControl m_Player;
    [SerializeField] private List<Transform> m_AllSpawnPoints;
    [SerializeField] private List<Transform> FailureSpots;   

    private bool ListCollected = false;
    private bool PortraitsActive = false;
    private bool Escape = true;

    private int m_CurrentRule;


    private int m_Hours;
    private int m_Minutes;

    private bool[] m_RulesSuccess = new bool[] { false, false, false, false, false, false };
    private List<List<Transform>> RulesSpawnPositions = new();


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
        ForwardTime(m_Hours, m_Minutes);

        RulesSpawnPositions.Add(m_AllSpawnPoints);
        

        m_CurrentRule = 0;
        m_Rules[m_CurrentRule].Init();


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

        m_Sky.SetTimeline(timeLine);
    }

    public List<Transform> GetSpawnPoints() { return m_AllSpawnPoints; }


    public bool GetPortraitMode()
    {
        return PortraitsActive;
    }

    public bool GetListCollected()
    {
        return ListCollected;
    }

    public void SetListCollected() { ListCollected = true; }


    public void SetPortaitMode(bool portraitBehaviour)
    {
        PortraitsActive = portraitBehaviour;
    }
    public PlayerControl GetPlayer() { return m_Player; }

    public bool PlayerCanEscape() { return Escape;  }

    public List<Transform> GetRuleSpawnPoints() { return RulesSpawnPositions[m_CurrentRule]; }




    //public void RuleCompleted(bool ruleSuccess)
    //{
    //    m_CurrentRule++;
    //    m_RulesSuccess.Add(ruleSuccess);


    //    if (m_CurrentRule == m_Rules.Count)
    //    {
    //        for (int i = 0; i < m_Rules.Count; i++)
    //        {
    //            if (m_RulesSuccess[i] == false)
    //            {
    //                foreach (Transform spawnPoint in FailureSpots)
    //                {
    //                    Instantiate(m_Rules[i].FailureAvatar, spawnPoint.transform);
    //                }

    //                Escape = false;
    //                return;
    //            }


    //        }

    //        m_Exit.SetActive(false);
    //    }
    //}

    public string GetRulesDescriptions()
    {
        string descriptionList = "";

        foreach (Rule rule in m_Rules)
        {
            for (int i = 0; i < m_Rules.Count; i++)
            {
                for (int j = 0; j < m_Rules[i].RuleDescription.Count; j++)
                {
                    descriptionList += m_Rules[i].RuleDescription[j];
                }
            }
        }

        return descriptionList;
    }

    //public Rule GetActiveRule()
    //{
    //    return m_Rules[m_CurrentRule];
    //}

}
