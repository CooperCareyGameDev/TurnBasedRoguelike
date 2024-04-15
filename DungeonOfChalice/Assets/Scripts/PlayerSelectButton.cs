using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterBattle playerTarget;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private float hideDelay = 2.0f; 
    public void SetPlayerCharacterBattle()
    {
        if (!playerTarget.hasDoneTurn)
        {
            battleHandler.playerCharacterBattle = playerTarget;
            //Debug.LogError(battleHandler.playerCharacterBattle.name);
            //TooltipScreenSpaceUI.ShowTooltip_Static("Selected " + battleHandler.playerCharacterBattle.currentClass);
            //StartCoroutine(HideTooltip());

        }
        else
        {
            //Debug.LogError("Player has already used turn!");
            TooltipScreenSpaceUI.ShowTooltipWarning_Static("Party Member Has Already Used Turn!");
            
        }
    }

    
}
