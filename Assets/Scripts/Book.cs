using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EBookTitle
{
    NONE,
    WAR_AND_PEACE,
    TWINS,
    DIVINE_COMEDY,
    THREE_WISE_MONKEYS,
    DR_JEKYLL_MR_HYDE,


    COUNT
}

public class Book : MonoBehaviour
{
    [SerializeField] EBookTitle m_Title;
    
    public EBookTitle Title => m_Title;

    
}
