using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class ClassTooltips : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private Button_UI buttonUI;
    [TextArea] [SerializeField] private string[] descriptions;
    private void Start()
    {
        textMeshProUGUI = GetComponentInParent<TextMeshProUGUI>();
        buttonUI = GetComponent<Button_UI>();
        buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static(SwitchStatement(textMeshProUGUI.text));
        buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();


    }
    private void Update()
    {
        string currentHeroText = textMeshProUGUI.text;

        /*switch (currentHeroText)
        {
            case "Knight":
                TooltipScreenSpaceUI.ShowTooltip_Static("Knight: uses basic melee attacks, and can give shield to party members");
                break;
            case "Barbarian":
                TooltipScreenSpaceUI.ShowTooltip_Static("Barbarian: high damage and can buff others, but no defensive abilites");
                break;
            case "Mage":
                TooltipScreenSpaceUI.ShowTooltip_Static("Mage: powerful offensive spells with elemental attacks, but lo health");
                break;
            case "Archer":
                TooltipScreenSpaceUI.ShowTooltip_Static("Archer: can hide under cover, and apply status effects from arrows");
                break;

                    
        }*/
    }

    public string SwitchStatement(string currentHeroText)
    {
        Debug.Log("running switch");
        switch (currentHeroText)
        {
            case "Knight":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[0]);
                return descriptions[0];
                //return "Knight: uses basic melee attacks, and can give shield to party members";
            case "Barbarian":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[1]);
                return descriptions[1];
                //return "Barbarian: high damage and can buff others, but no defensive abilites";
            case "Mage":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[2]);
                return descriptions[2];
                //return "Mage: powerful offensive spells with elemental attacks, but low health";
            case "Archer":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[3]);
                return descriptions[3];
                //return "Archer: can hide under cover, and apply status effects from arrows";
            case "Cleric":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[4]);
                return descriptions[4];
                //return "Cleric: Heals and cleanses status effects, but has low damage";
            case "King":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[5]);
                return descriptions[5];
               // return "King: Gives powerful buffs to party members, but has terrible attacks";
            case "Trapper":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[6]);
                return descriptions[6];
                //return "Trapper: Can set traps on allies that damage enemies that attack that ally";
            case "Paladin":
                TooltipScreenSpaceUI.ShowTooltip_Static(descriptions[7]);
                return descriptions[7];
                //return "Can heal or shield the entire party as well as having decent damage";
            default:
                return "Invalid Text";


        }
    }
}
