using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [TextArea]
    [SerializeField] string attackingText;
    [TextArea]
    [SerializeField] string rageText;
    [TextArea]
    [SerializeField] string statusEffectsText; 
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetAttackingText()
    {
        tutorialText.text = attackingText;
    }

    public void SetRageText()
    {
        tutorialText.text = rageText;
    }

    public void SetStatusEffectsText()
    {
        tutorialText.text = statusEffectsText;
    }
}
