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
    [SerializeField] private List<Material> m_Textures; // Material pour le gym, code final doit être une texture
    [SerializeField] private Transform m_Eyes;

    private Renderer m_Renderer;

    private Coroutine m_PortraitRoutine;


    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();

        m_Renderer.material = m_Textures[(int)EPortrait.FRIENDLY];  // Material pour le Gym, code final doit être mainTexture

        
    }

    public IEnumerator PortraitGazeRoutine()
    {
        while (true)
        {
            Debug.DrawRay(m_Eyes.position, m_Eyes.forward, Color.red);
            if (Physics.Raycast(m_Eyes.position, m_Eyes.forward, out RaycastHit hitInfo, 2.0f))
            {
                PlayerControl player = hitInfo.collider.GetComponent<PlayerControl>();

                if (player != null)
                {
                    Debug.Log("Player Detected");
                    m_Renderer.material = m_Textures[(int)(EPortrait.UNCANNY)];
                }
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RulesManager.Instance.GetPortraitMode())
        {
            m_PortraitRoutine = StartCoroutine(PortraitGazeRoutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(m_PortraitRoutine);
    }


}
