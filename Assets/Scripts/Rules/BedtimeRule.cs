using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu]
public class BedtimeRule : Rule
{
    [SerializeField] private int m_NbUncannyPortraits;

    private List<Portrait> m_UncannyPortraits;
    private List<Shelf> m_Shelves;
    private int soothedPortraits;
    private int interactedPortraits;

    public override void Init()
    {
        m_Shelves = RulesManager.Instance.Level.GetLevelShelves();
        List<int> booksToAssign = new List<int> { 1, 2, 3, 4, 5 }.OrderBy(x => Random.value).ToList();
        int currentBook = 0;
        foreach(Shelf shelf in m_Shelves) 
        { 
            shelf.SetBook((EBookTitle)booksToAssign[currentBook]);
            currentBook++;
        }
        m_UncannyPortraits = RulesManager.Instance.GetPortraits().OrderBy(x => Random.value).ToList();
        for(int i = 0; i< m_NbUncannyPortraits; i++)
        {
            m_UncannyPortraits[i].SetBadMood(true);
        }

        soothedPortraits = 0;
        interactedPortraits = 0;
    }
    public override void CheckCompletion()
    {
        if (interactedPortraits == m_NbUncannyPortraits)
        {
            End();
        }
    }

    public override void End()
    {
        List<Portrait> allPortraits = RulesManager.Instance.GetPortraits();
        if (soothedPortraits < m_NbUncannyPortraits)
        {
            
            foreach (Portrait portrait in allPortraits)
            {
                portrait.SetBadMood(true);
            }
        }
        else
        {
            foreach (Portrait portrait in allPortraits)
            {
                portrait.SetBadMood(false);
            }
        }

        foreach (Shelf shelf in m_Shelves)
        {
            shelf.ShutDown();
        }
        RulesManager.Instance.RuleCompleted();
    }

    public override void OnRuleObjectUsed(Portrait portrait, EBookTitle book)
   {
        interactedPortraits++;
        if(book == portrait.FavoriteBook)
        {
            portrait.SetBadMood(false);
            soothedPortraits++;
        }
        else
        {
            RulesManager.Instance.Strike();
        }
        RulesManager.Instance.ForwardTime(1, 0);
        CheckCompletion();
    }
}
