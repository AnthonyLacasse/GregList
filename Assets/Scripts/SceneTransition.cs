using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneTransition : MonoBehaviour
{
  
    [SerializeField] private float m_FadeDuration = 1f;
    private CanvasGroup m_FadePanel;

    private void Start()
    {
        m_FadePanel = GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < m_FadeDuration)
        {
            elapsedTime += Time.deltaTime;
            m_FadePanel.alpha = 1f - (elapsedTime / m_FadeDuration);
            
            yield return null;
        }

        m_FadePanel.alpha = 0f; // Make sure panel is at full transparency
        
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < m_FadeDuration)
        {
            elapsedTime += Time.deltaTime;
            m_FadePanel.alpha = elapsedTime / m_FadeDuration;
            
            yield return null;
        }

        m_FadePanel.alpha = 1f; // Make sure panel is at full opacity
        

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}
