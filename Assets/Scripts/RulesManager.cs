using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;
using UnityEngine.UIElements;

public class RulesManager : MonoBehaviour
{
    private static RulesManager m_Instance;

    [SerializeField] private AzureTimeController m_Sky;

    private int m_Hours = 8;
    private int m_Minutes = 0;

    public static RulesManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private RulesManager m_RulesManager;

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


        m_RulesManager = new RulesManager();
        
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

        float timeLine = m_Hours + m_Minutes / 60;

        m_Sky.SetTimeline(timeLine);
    }

    

        





}
