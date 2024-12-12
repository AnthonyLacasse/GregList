using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPortrait
{
    FRIENDLY,
    UNCANNY,

    COUNT
}



public class Portrait : MonoBehaviour, Interactable
{
    [SerializeField] private Transform m_Eyes;
    [SerializeField] private GameObject m_UncannyVersion;
    [SerializeField] private EBookTitle m_FavoriteBook;
    
    private BoxCollider m_BoxCollider;
    private bool isGazing = false;
    private bool m_CanInteract = false;
    private PlayerControl m_Player;

    private Coroutine m_PortraitRoutine;

    public EBookTitle FavoriteBook => m_FavoriteBook;

    private void Start()
    {
        m_UncannyVersion.SetActive(false);
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    public void SetBadMood(bool mood)
    {
        m_UncannyVersion.SetActive(mood);
        m_CanInteract = mood;  
        m_BoxCollider.size = new Vector3(0.2f, 0.25f, 0.09f);
        m_BoxCollider.center = new Vector3(0.002f, 0.1f, 0.07f);
        m_Player = FindObjectOfType<PlayerControl>();
    }



    public IEnumerator PortraitGazeRoutine()
    {
        while (isGazing)
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
                    RulesManager.Instance.Strike();
                    isGazing = false; // End gaze behavior
                }
            }
            yield return null;
        }
        m_PortraitRoutine = null; // Reset coroutine reference
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

        if (player != null)
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

    public void InRange(bool inRange)
    {
        
    }

    public bool CanInteract()
    {
        return m_CanInteract;
    }

    public void Use()
    {
        if (m_Player != null && m_Player.HeldItem != EBookTitle.NONE)
        {
            RulesManager.Instance.GetActiveRule().OnRuleObjectUsed(this, m_Player.HeldItem);
            m_Player.StashBook();
        }
        else
        {
            Debug.LogWarning("Player or HeldItem is null in Portrait.Use()");
        }
    }

    public void PortraitGaze()
    {
        isGazing = true;
        m_UncannyVersion.SetActive(false);        
    }
}
