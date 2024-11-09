using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private EThemeType m_Theme;


    public void OnTriggerEnter(Collision other)
    {
        AudioManager.GetInstance().PlayTheme(m_Theme);
    }

    public void OnTriggerExit(Collision other)
    {
        AudioManager.GetInstance().StopTheme();
    }
}
