
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MorningRule : Rule
{
    public List<string> ruleDescription;
    public List<Transform> spawnLocations;
    
    public override void Init()
    {
        
    }
    public override void CheckCompletion()
    {
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    private List<Transform> GetSpawnPoints()
    {
        spawnLocations = new List<Transform>();

        for (int i = 0; i < 10; i++)
        {

        }


        
        return spawnLocations;
    }





}
