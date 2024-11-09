using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private EThemeType m_Theme;


    //public void OnTriggerEnter(Collider other)
    //{
    //    PlayerControl player = other.GetComponent<PlayerControl>();

    //    if ( player != null && AudioManager.Instance.Turntable != null)
    //    {
    //        AudioManager.Instance.PlayTheme(m_Theme);
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    PlayerControl player = other.GetComponent<PlayerControl>();

    //    if (player && AudioManager.Instance.Turntable != null)
    //    {
    //        AudioManager.Instance.StopTheme();
    //    }
    //}
}
