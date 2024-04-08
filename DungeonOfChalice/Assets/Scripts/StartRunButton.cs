using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartRunButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI classText1;
    [SerializeField] TextMeshProUGUI classText2;
    [SerializeField] TextMeshProUGUI classText3;
    [SerializeField] TextMeshProUGUI classText4;



    public void StartGame()
    {
        if (classText1.text != classText2.text && classText1.text != classText3.text && classText1.text != classText4.text && classText2.text != classText3.text && classText2.text != classText4.text && classText3.text != classText4.text)
        {
            // Start game
            Debug.Log("Game Started");
            TooltipScreenSpaceUI.ShowTooltip_Static("Success!");
        }
        else
        {
            Debug.LogError("Invalid Party");
            TooltipScreenSpaceUI.ShowTooltipWarning_Static("Only one of each class!");
        }
    }
}
