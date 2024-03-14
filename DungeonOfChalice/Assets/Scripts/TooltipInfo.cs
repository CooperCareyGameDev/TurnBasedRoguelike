using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TooltipInfo : MonoBehaviour
{
    [TextArea] [SerializeField] private string[] tooltipText;
    [SerializeField] private int initialTooltipIndex = 0;
    public static int tooltipIndex = 0; 
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
            //buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[0]);
            //buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static(); 

        }
    }

    private void Update()
    {
        if (tooltipIndex == 0)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[0]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 1)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[1]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 2)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[2]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 3)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[3]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 4)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[4]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 5)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[5]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 6)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[6]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
        else if (tooltipIndex == 7)
        {
            buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static((string)tooltipText[7]);
            buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
        }
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
