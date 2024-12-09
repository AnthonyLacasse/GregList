using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private EThemeType m_Theme;

    public EThemeType Theme => m_Theme;
    
}
