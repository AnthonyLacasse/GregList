using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;


public class RulesManager : MonoBehaviour
{
    private static RulesManager m_Instance;

    [SerializeField] private AzureTimeController m_Sky;
    [SerializeField] private List<AbstractRule> m_Rules;



   // private int m_CurrentRule = 0;



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

        //La documentation est vraiment vaste. La skybox poss�de son propre eventsystem. L'incr�mentation des minutes ne fonctionne pas avec un code basique
        //Je sais que je peux forcer la skybox � afficher une heure enti�re, je vais continuer de consulter la documentation 
    }

    
}
