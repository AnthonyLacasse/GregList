using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a shell script designed to hold transforms locations from the scene

public enum ERuleType
{
    FIND_ITEMS,
    PORTRAITS,
    MEAL,


    MAX_COUNT
}

[System.Serializable]
public struct TransformByType
{
    public ERuleType type;
    public List<Transform> transforms;
}




public class Level : MonoBehaviour
{
    [SerializeField] private List<TransformByType> m_Transforms;
    private Dictionary<ERuleType, List<Transform>> m_Positions = new();

    private void Start()
    {
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        foreach (TransformByType e in m_Transforms)
        {
            m_Positions.Add(e.type, e.transforms);
        }
    }

    public List<Transform> GetLevelTransforms(ERuleType type)
    {
        return m_Positions[type];
    }

}
