
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu]
public class NoonRule : Rule
{      

    public List<GameObject> plantsPrefabs;

    public int m_NumberOfPlants;

    public int m_RoomsToVisit;

    private int m_WateredPlants;

    private int m_NotAFern;
        
    
    public override void Init()
    {
        
        
    }

    public override void CheckCompletion()
    {
    }

    public override void End()
    {
       
    }

    private void SpawnFood()
    {
        List<Transform> spawnPoints = RulesManager.Instance.GetSpawnPoints();
        List<Transform>  spawnLocations = new List<Transform>();
       

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





}
