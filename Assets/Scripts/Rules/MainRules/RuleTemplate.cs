using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class RuleTemplate : Rule
{
    public RulesAddonTemplate[] MinorModifiers;
         
    public int TimeValue;

    public int RoomsToVisit;

    public List<GameObject> VisitedRooms = new List<GameObject>();

    public PlayerControl Player;


    // À retirer
    public GameObject m_RuleTool; //Gym Item, à supprimer dans la scene principale


    private void Awake()
    {
        Player.m_VisitingRoom += EnterRoom;
    }

    private void OnDestroy()
    {
        Player.m_VisitingRoom -= EnterRoom;
        VisitedRooms.Clear();
    }


    public void EnterRoom(GameObject room)
    {
        if (!VisitedRooms.Contains(room))
        {
            VisitedRooms.Add(room);
            RulesManager.Instance.ForwardTime(0, TimeValue);
            Debug.Log("You visited a room");
            CheckRuleCompletion();
        }
    }

    private void CheckRuleCompletion()
    {
        if (VisitedRooms.Count == RoomsToVisit)
        {
            m_RuleTool.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}

