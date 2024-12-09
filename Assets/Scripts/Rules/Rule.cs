using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class Rule : ScriptableObject 
{
    public List<string> ruleDescription;    

    public ERuleType ruleType;
    public abstract void Init();
    public abstract void CheckCompletion();
    public abstract void End();
    public virtual void Update() { }
    public virtual void OnRuleObjectUsed() { }
    public virtual void OnRuleObjectUsed(Disposable obj) { }
}
