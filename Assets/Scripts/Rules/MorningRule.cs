
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu]
public class MorningRule : Rule
{
    public List<string> ruleDescription;

    public List<GameObject> plantsPrefabs;

    public int numberOfPlants;
    
    public override void Init()
    {
        SpawnPlants();
    }
    public override void CheckCompletion()
    {
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    private void SpawnPlants()
    {
        List<Transform> spawnPoints = RulesManager.Instance.GetSpawnPoints();
        List<Transform>  spawnLocations = new List<Transform>();

        for (int i = 0; i < numberOfPlants; i++)                                                    //Populate transform list with X random spawn points for the global list
        {
            int randomPoint = Random.Range(0, spawnPoints.Count);
            spawnLocations.Add(spawnPoints[randomPoint]);
            spawnPoints.Remove(spawnPoints[randomPoint]);
        }

        foreach (Transform spawn in spawnLocations)                                    //Spawn a different flower on each location
        {
            int flower = Random.Range(0, plantsPrefabs.Count);
            Instantiate(plantsPrefabs[flower], spawn);
        }      
       
    }





}
