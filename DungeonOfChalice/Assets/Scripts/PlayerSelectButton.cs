using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterBattle playerTarget;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private float hideDelay = 2.0f;
    [SerializeField] private float actionDelay = 1.5f; 
    public void SetPlayerCharacterBattle()
    {
        if (BattleHandler.isSelecting) { return; }
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

    private IEnumerator SelectSecondaryTarget()
    {
        Debug.LogError("Selecting Target");
        if (BattleHandler.isSelecting)
        {
            if (battleHandler.playerCharacterBattle.currentClass == "Knight" || true)
            {
                BattleHandler.isSelecting = false;
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.ShieldOnTurn(battleHandler.targetedCharacterBattle.shieldAmount, () => { });

            }

        }
    }

    public void SelectSecondaryCoroutine()
    {
        StartCoroutine(SelectSecondaryTarget());
    }
}
