using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class TooltipManager : MonoBehaviour
{
    private void Start()
    {
        TooltipScreenSpaceUI.ShowTooltipWarning_Static("Attack");
        //StartCoroutine(HideTooltip());
        //transform.Find("Attack").GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static("Attack");
        //transform.Find("Attack").GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
    }
    private void Update()
    {
        
    }

    private IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(3);
        TooltipScreenSpaceUI.HideTooltip_Static();
    }

    
}
