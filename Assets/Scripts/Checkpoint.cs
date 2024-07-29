using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private HUD m_HUD;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();

        if (player != null && !RulesManager.Instance.PlayerCanEscape())
        {
            m_HUD.LoseGame();
        }

    }
}
