using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HUD : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_InteractGroup;
    [SerializeField] private List<Sprite> m_Sprites;
    [SerializeField] private Image m_PromptImage;
    [SerializeField] private Image m_RulePanel;
    [SerializeField] private Vector3 m_FinalRulePosition;

    private Vector3 m_RulesHiddenPosition;

    private float m_Elapsed;
    private Coroutine m_ShowRulesRoutine;
    private Coroutine m_HideRulesRoutine;

    private void Start()
    {
        m_RulesHiddenPosition = m_RulePanel.transform.localPosition;
        m_HideRulesRoutine = StartCoroutine(HideRulesRoutine());
        m_ShowRulesRoutine = StartCoroutine(ShowRulesRoutine());
    }

    public void DisplayPrompt(InteractibleType type)
    {
        m_PromptImage.sprite = m_Sprites[(int)type]; 
        m_InteractGroup.alpha = 1.0f;
    }

    public void HidePrompt()
    {
        m_InteractGroup.alpha = 0f;
    }

    public void DisplayNote()
    {
        
        StartCoroutine(ShowRulesRoutine());
    }

    public void HideNote()
    {
        
        StartCoroutine(HideRulesRoutine());
    }


    private IEnumerator ShowRulesRoutine()
    {
        m_Elapsed = 0;

        while (m_RulePanel.transform.localPosition != m_FinalRulePosition)
        {
            m_Elapsed += Time.deltaTime;
            m_RulePanel.transform.localPosition = Vector3.Lerp(m_RulesHiddenPosition, m_FinalRulePosition, m_Elapsed);
            yield return null;
        }

    }

    private IEnumerator HideRulesRoutine()
    {
        m_Elapsed = 0;

        while (m_RulePanel.transform.localPosition != m_RulesHiddenPosition)
        {
            m_Elapsed += Time.deltaTime;
            m_RulePanel.transform.localPosition = Vector3.Lerp(m_FinalRulePosition, m_RulesHiddenPosition, m_Elapsed);
            yield return null;
        }

    }


}
