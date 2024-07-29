using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Rule : ScriptableObject
{    
    public List<string> RuleDescription;

    public int TimeValue;

    public int RoomsToVisit;

    public bool PortraitsActive;

    public List<GameObject> VisitedRooms = new List<GameObject>();

    public List<GameObject> Prefabs;

    public GameObject FailureAvatar;
                
    public bool RuleFollowed = true;  

    private void OnDestroy()
    {
        RulesManager.Instance.GetPlayer().m_VisitingRoom -= EnterRoom;
        VisitedRooms.Clear();
        
    }

    public void Init()
    {
        RulesManager.Instance.GetPlayer().m_VisitingRoom += EnterRoom;

        RulesManager.Instance.SetPortaitMode(PortraitsActive);

        foreach (Transform spawnPoint in RulesManager.Instance.GetRuleSpawnPoints())
        {
            if (spawnPoint == null) return;

            if (Prefabs != null)
            {
                Instantiate(Prefabs[Random.Range(0, Prefabs.Count)], spawnPoint);
            }
        }
    }
    public void EnterRoom(GameObject room)
    {
        if (!VisitedRooms.Contains(room))
        {
            VisitedRooms.Add(room);
            RulesManager.Instance.ForwardTime(0, TimeValue);
            CheckRuleCompletion();
        }
    }
    private void CheckRuleCompletion()
    {
        if (VisitedRooms.Count == RoomsToVisit)
        {            
            RulesManager.Instance.RuleCompleted(RuleFollowed);
        }
    }

}
