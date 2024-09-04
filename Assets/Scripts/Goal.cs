using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private HUD m_HUD;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();

        if (player != null )
        {
            m_HUD.WinGame();
        }
    }
}
