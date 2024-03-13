using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class TooltipInfo : MonoBehaviour
{
    [TextArea] [SerializeField] private string tooltipText;
    [SerializeField] private bool isEnemy = false;
    private string healthText;
    private int currentHealth;
    private string newText;
    Button_UI buttonUI;
    private void Awake()
    {
        buttonUI = GetComponent<Button_UI>();
        if (isEnemy )
        {
            healthText = $"\n{currentHealth} / {GetComponentInParent<CharacterBattle>().GetStartingHealth()} Health\n";
            //Debug.Log(GetComponentInParent<CharacterBattle>().GetCurrentHealth());
        }
    }
    private void Start()
    {
        if (isEnemy)
        {
            System.Func<string> getTooltipTextFunc = () =>
            {
                return $"Enemy \n{GetComponentInParent<CharacterBattle>().GetCurrentHealth()} / {GetComponentInParent<CharacterBattle>().GetStartingHealth()} Health";
            }; 
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static(getTooltipTextFunc);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static(tooltipText);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static(); 

        }
    }

    private void Update()
    {
        /*if (isEnemy)
        {
            currentHealth = GetComponentInParent<CharacterBattle>().GetCurrentHealth();
            healthText = $"\n{currentHealth} / {GetComponentInParent<CharacterBattle>().GetStartingHealth()} Health\n";
            newText = tooltipText + healthText;
            
        }*/
    }

    /*private void OnMouseEnter()
    {
        TooltipScreenSpaceUI.ShowTooltip_Static(tooltipText);
    }

    private void OnMouseExit()
    {
        TooltipScreenSpaceUI.HideTooltip_Static();
    }*/
}
