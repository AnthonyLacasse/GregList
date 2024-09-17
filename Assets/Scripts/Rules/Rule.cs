using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class Rule : ScriptableObject 
{
    public abstract void Init();
    public abstract void CheckCompletion();
    public abstract void End();


}
