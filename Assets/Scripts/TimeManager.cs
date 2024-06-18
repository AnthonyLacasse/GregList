using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager m_Instance;

    public static TimeManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private TimeManager m_TimeManager;

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


        m_TimeManager = new TimeManager();
        
    }

    public void GetTime()
    {

    }

        





}
