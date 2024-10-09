
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu]
public class MorningRule : Rule
{
    public List<GameObject> plantsPrefabs;

    public int m_NumberOfPlants;

    public int m_RoomsToVisit;

    public int m_TimeValue;

    private int m_WateredPlants;

    private int m_NotAFern;

    private List<GameObject> m_VisitedRooms;



    public override void Init()
    {
        m_WateredPlants = 0;
        m_NotAFern = 0;
        m_VisitedRooms = new List<GameObject>();

        SpawnPlants();

        RulesManager.Instance.GetPlayer().m_VisitingRoom += VisitedRoom;

    }

    public override void CheckCompletion()
    {
        if (m_VisitedRooms.Count == m_RoomsToVisit)
        {
            if (m_WateredPlants < m_NotAFern)
            {
                RulesManager.Instance.Striked();
            }
            End();
        }
    }

    public override void End()
    {
        RulesManager.Instance.GetPlayer().m_VisitingRoom -= VisitedRoom;
        RulesManager.Instance.RuleCompleted();
    }

    private void SpawnPlants()
    {
        List<Transform> spawnPoints = RulesManager.Instance.GetPlantsSpawnPoints();
        List<Transform> spawnLocations = new List<Transform>();
        m_NotAFern = 0;

        for (int i = 0; i < m_NumberOfPlants; i++)                                     //Populate transform list with X random spawn points from the global list
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            spawnLocations.Add(spawnPoints[randomPoint]);
            spawnPoints.Remove(spawnPoints[randomPoint]);
        }

        foreach (Transform spawn in spawnLocations)                                    //Spawn a different flower on each location
        {
            int flower = Random.Range(0, plantsPrefabs.Count);
            Instantiate(plantsPrefabs[flower], spawn);
            if (flower != (plantsPrefabs.Count - 1))
            {
                m_NotAFern++;
            }
        }

    }

    public override void OnRuleObjectUsed()
    {
        m_WateredPlants++;
    }


    private void VisitedRoom(GameObject room)
    {
        if (!m_VisitedRooms.Contains(room))
        {
            m_VisitedRooms.Add(room);
            Debug.Log("New room visited");
            RulesManager.Instance.ForwardTime(0, m_TimeValue);
        }
        CheckCompletion();
    }




}
