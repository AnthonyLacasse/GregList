using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GarbageRule : Rule
{
    public List<Disposable> garbagePrefabs;

    public int m_NumberOfTrash;

    public int m_RoomsToVisit;

    public int m_TimeValue;

    private int m_CollectedTrash;

    private int m_NotForCollection;

    private string m_DoNotCollect;

    List<Disposable> m_Collection;

    private List<GameObject> m_VisitedRooms;



    public override void Init()
    {
        m_CollectedTrash = 0;
        m_Collection = new List<Disposable>();
        m_VisitedRooms = new List<GameObject>();



        GenerateForbiddenColor();

        ruleDescription[0] = "I'm very sorry, but we had to leave in a bit of a hurry, and the house isn't tidy enough. ";
        ruleDescription[1] = "Would you kindly tour the house and dispose of trash that way be left behind ? ";
        ruleDescription[2] = $"Don't bother to pick up anything that is {m_DoNotCollect}. ";
        ruleDescription[3] = "Gertrude loves this color and she will pick hem up herself. ";
        ruleDescription[4] = "Be warry that once you have visited each room, you won't be able to pick anything left behind, and Gertrude won't like it if you missed something. ";

        SpawnGarbage();

        RulesManager.Instance.GetPlayer().m_VisitingRoom += VisitedRoom;

    }

    public override void CheckCompletion()
    {
        if (m_VisitedRooms.Count == m_RoomsToVisit)
        {
            int completionValue = m_NumberOfTrash - m_NotForCollection;

            if (m_CollectedTrash < completionValue)
            {
                RulesManager.Instance.Strike();
            }
            End();
        }
    }

    public override void End()
    {
        for (int i = m_Collection.Count - 1; i >= 0 ; i--)
        {
            Disposable garbage = m_Collection[i];
            m_Collection.Remove(garbage);
            Destroy(garbage.gameObject);
        }

        RulesManager.Instance.GetPlayer().m_VisitingRoom -= VisitedRoom;
        RulesManager.Instance.RuleCompleted();
    }

    private void GenerateForbiddenColor()
    {
        int random = Random.Range(0, 100) % 3;

        if (random == 0)
        {
            m_DoNotCollect = "red";
        }
        else if (random == 1)
        {
            m_DoNotCollect = "blue";
        } 
        else
        {
            m_DoNotCollect = "green";
        }
    }


    private void SpawnGarbage()
    {
        List<Transform> spawnPoints = RulesManager.Instance.GetSpawnPoints(ruleType);
        List<Transform> spawnLocations = new List<Transform>();
        m_NotForCollection = 0;

        for (int i = 0; i < m_NumberOfTrash; i++)                                     //Populate transform list with X random spawn points from the global list
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            spawnLocations.Add(spawnPoints[randomPoint]);
            spawnPoints.Remove(spawnPoints[randomPoint]);
        }

        foreach (Transform spawn in spawnLocations)                                    //Spawn a different garbage item on each location
        {
            int trash = Random.Range(0, garbagePrefabs.Count);
            Disposable garbage = Instantiate(garbagePrefabs[trash], spawn);
            if (garbage.GetColor() == m_DoNotCollect)
            {
                m_NotForCollection++;
            }
            m_Collection.Add(garbage);
        }

    }

    public override void OnRuleObjectUsed(Disposable obj)
    {
        m_CollectedTrash++;

        if (obj.GetColor() == m_DoNotCollect)
        {
            RulesManager.Instance.Strike();
        }
        m_Collection.Remove(obj);
        if (obj != null)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            Debug.LogWarning("The object to be destroyed is null.");
        }

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


