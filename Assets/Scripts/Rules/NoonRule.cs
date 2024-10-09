
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class NoonRule : Rule
{      

    public List<GameObject> foodPrefabs;

    

    
        
    
    public override void Init()
    {       
        SpawnFood();

        

    }

    public override void CheckCompletion()
    {
    }

    public override void End()
    {
       
    }

    private void SpawnFood()
    {
        List<Transform> spawnPoints = RulesManager.Instance.GetFoodSpawnPoints();
        List<GameObject> foodToInstantiate = foodPrefabs.OrderBy( x => Random.value).ToList();

        for (int i = 0; i < foodToInstantiate.Count; i++)
        {
            Instantiate(foodToInstantiate[i], spawnPoints[i].transform);
        }  
    }





}
