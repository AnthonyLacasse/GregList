using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType
{
    EVENT_START,
    EVENT_END,

    COUNT
}


public class Observer
{
    #region Singleton
    private static Observer m_Instance;

    private Dictionary<EEventType, Action<Dictionary<string, object>>> m_Events;

    private Observer()
    {
        m_Events = new Dictionary<EEventType, Action<Dictionary<string, object>>>();
    }

    public static Observer GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new Observer();
        }

        return m_Instance;
    }
    #endregion


    public void SubscribeTo(EEventType eventId, Action<Dictionary<string, object>> listener)
    {
        if (m_Events[eventId] != null)
        {
            m_Events[eventId] += listener;
        }
        else
        {
            m_Events.Add(eventId, listener);
        }
    }

    public void UnsubscribeTo(EEventType eventId, Action<Dictionary<string, object>> listener)
    {
        if (m_Events[eventId] != null)
        {
            m_Events[eventId] -= listener;
        }
    }

    public void TriggerEvent(EEventType eventId, Dictionary<string, object> parameter)
    {
        if (m_Events[eventId] != null)
        {
            m_Events[eventId].Invoke(parameter);
        }
    }

}
