using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HUD : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_InteractGroup;
    [SerializeField] private List<Sprite> m_Sprites;
    [SerializeField] private Image m_PromptImage;
    [SerializeField] private Image m_RulePanel;
    [SerializeField] private Vector3 m_FinalRulePosition;
    [SerializeField] private TextMeshProUGUI m_RulesDescription;
    [SerializeField] private TextMeshProUGUI m_FinalText;
    [SerializeField] private CanvasGroup m_FinalTextGroup;
    

    private Vector3 m_RulesHiddenPosition;

    private float m_Elapsed;
    private Coroutine m_ShowRulesRoutine;
    private Coroutine m_HideRulesRoutine;
    private Coroutine m_EndGameCoroutine;
   

    private void Start()
    {
        m_RulesHiddenPosition = m_RulePanel.transform.localPosition;

        m_RulesDescription.text = RulesManager.Instance.GetRulesDescriptions();

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

        m_ShowRulesRoutine = StartCoroutine(ShowRulesRoutine());
    }

    public void HideNote()
    {

        m_HideRulesRoutine = StartCoroutine(HideRulesRoutine());
    }

    public void WinGame()
    {
        m_FinalText.color = Color.blue;
        m_FinalText.text = "You're free";
        m_EndGameCoroutine = StartCoroutine(EndGameRoutine());
    }

    public void LoseGame()
    {
        m_FinalText.color = Color.red;
        m_FinalText.text = "You will stay here forever";
        m_EndGameCoroutine = StartCoroutine(EndGameRoutine());
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


    private IEnumerator EndGameRoutine()
    {
        float elapsed = 0;
        
        while(true)
        {
            elapsed += Time.deltaTime;

            m_FinalTextGroup.alpha = Mathf.Lerp(0, 1, elapsed);

            if (elapsed > 1) 
            {
                yield return new WaitForSeconds(5f);


                SceneManager.LoadScene("TitleScene");

            }



        }
    }


}
