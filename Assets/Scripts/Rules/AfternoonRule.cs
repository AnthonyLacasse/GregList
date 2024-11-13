using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfternoonRule : Rule
{
    
    private List<GameObject> m_ActivePaintings;

    public int m_AmountofActivePaintings;

    public override void Init()
    {
        RulesManager.Instance.SetReadTime(true);
    }
    void Start()
    {
        
    }
    public override void CheckCompletion()
    {
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        RulesManager.Instance.SetReadTime(false);
    }
        

   
}
