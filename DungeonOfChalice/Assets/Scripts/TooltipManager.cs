using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class TooltipManager : MonoBehaviour
{
    [SerializeField] private Button_UI attackButton; 
    private void Start()
    {
        //StartCoroutine(HideTooltip());
        //Debug.Log(transform.Find("Attack")); 
        //attackButton.GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => TooltipScreenSpaceUI.ShowTooltip_Static("Attack");
        //attackButton.GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => TooltipScreenSpaceUI.HideTooltip_Static();
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
