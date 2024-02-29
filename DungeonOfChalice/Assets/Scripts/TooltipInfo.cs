using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class TooltipInfo : MonoBehaviour
{
    [TextArea] [SerializeField] private string tooltipText;
    Button_UI buttonUI;
    private void Awake()
    {
        buttonUI = GetComponent<Button_UI>();
    }
    private void Start()
    {
        buttonUI.MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static(tooltipText);
        buttonUI.MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static(); 
    }



    private void OnMouseEnter()
    {
        TooltipScreenSpaceUI.ShowTooltip_Static(tooltipText);
    }

    private void OnMouseExit()
    {
        TooltipScreenSpaceUI.HideTooltip_Static();
    }
}
