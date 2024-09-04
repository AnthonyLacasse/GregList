using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image m_Hand;
    [SerializeField] private Vector3 m_FinalHandPosition;

    private Vector3 m_StartingHandPosition;
    private float m_Elapsed;

    private Coroutine m_HandRoutine;

    private void Start()
    {
        
        m_StartingHandPosition = m_Hand.transform.localPosition;
        m_Elapsed = 0;
        m_HandRoutine = StartCoroutine(RaisePhoneRoutine());
    }

    public void OnStartGamePress()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }



    private IEnumerator RaisePhoneRoutine()
    {
        while(m_Hand.transform.localPosition != m_FinalHandPosition)
        {
            m_Elapsed += Time.deltaTime;
            m_Hand.transform.localPosition = Vector3.Lerp(m_StartingHandPosition, m_FinalHandPosition, m_Elapsed/2);
            Debug.Log(m_Hand.transform.localPosition);
            yield return null;
        }

        StopCoroutine(m_HandRoutine);
    }


}
