using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPortrait
{
    FRIENDLY,
    UNCANNY,

    COUNT
}



public class Portrait : MonoBehaviour
{
    [SerializeField] private Transform m_Eyes;
    [SerializeField] private GameObject m_UncannyVersion;
    [SerializeField] private EBookTitle m_FavoriteBook;
    

    private bool isGazing = false;

    private Coroutine m_PortraitRoutine;

    public EBookTitle FavoriteBook => m_FavoriteBook;

    private void Start()
    {
        m_UncannyVersion.SetActive(false);
    }

    public void SetBadMood(bool mood)
    {
        m_UncannyVersion.SetActive(mood);
    }

    public void SetInteractibility(bool interactibility)
    {
        //  canInteract = interactibility;
    }


    public IEnumerator PortraitGazeRoutine()
    {
        while (true)
        {
            Debug.DrawRay(m_Eyes.position, m_Eyes.forward * 2, Color.red);
            if (Physics.Raycast(m_Eyes.position, m_Eyes.forward, out RaycastHit hitInfo, 2.0f))
            {
                Debug.Log(hitInfo.collider.name);
                PlayerControl player = hitInfo.collider.GetComponent<PlayerControl>();

                if (player != null)
                {
                    Debug.Log("Player Detected");
                    m_UncannyVersion.SetActive(true);
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerControl player = other.GetComponent<PlayerControl>();

        if (player != null && isGazing)
        {
            m_PortraitRoutine = StartCoroutine(PortraitGazeRoutine());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();

        if (player != null && isGazing)
        {
            if (m_PortraitRoutine != null)
            {
                StopCoroutine(m_PortraitRoutine);
            }
        }
    }

    public InteractibleType GetType()
    {
        return InteractibleType.BOOK;

    }

}
