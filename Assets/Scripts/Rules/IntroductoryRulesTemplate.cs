using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class IntroductoryRuleTemplate : AbstractRule

{
    public List<string> RuleDescription;

    public MinorRulesTemplate[] MinorModifiers;

    public Transform[] Spawnpoints;

    public GameObject[] prefabModels;

    public int Count;

    public bool Completed = false;

}
