using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterBattle playerTarget;
    [SerializeField] private BattleHandler battleHandler;
    [SerializeField] private float hideDelay = 2.0f;
    [SerializeField] private float actionDelay = 1.5f;

    private void Start()
    {
        battleHandler = FindFirstObjectByType<BattleHandler>();
    }
    public void SetPlayerCharacterBattle()
    {
        if (BattleHandler.isSelecting) { return; }
        if (!playerTarget.hasDoneTurn)
        {
            Debug.LogError("Selecting Target");
            battleHandler.playerCharacterBattle = playerTarget;
            battleHandler.activeCharacterBattle = playerTarget;
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
        
        if (BattleHandler.isSelecting)
        {
            if (battleHandler.playerCharacterBattle.currentClass == "Knight")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.ShieldOnTurn(battleHandler.playerCharacterBattle.shieldAmount, () => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Barbarian")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                Debug.LogError("Buffing");
                battleHandler.targetedCharacterBattle.BuffOnTurn( () => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Mage")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.GiveSpikeOnTurn(() => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Archer")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.GiveEvasiveOnTurn(() => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Cleric")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.HealOnTurn(battleHandler.playerCharacterBattle.healingAmount, () => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "King")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.ShieldOnTurn(battleHandler.playerCharacterBattle.shieldAmount, () => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Trapper")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.TrapOnTurn(() => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
            else if (battleHandler.playerCharacterBattle.currentClass == "Paladin")
            {
                yield return new WaitForSeconds(actionDelay);
                battleHandler.targetedCharacterBattle = playerTarget;
                battleHandler.targetedCharacterBattle.HealOnTurn(battleHandler.playerCharacterBattle.healingAmount / 2, () => { });
                battleHandler.targetedCharacterBattle.ShieldOnTurn(battleHandler.playerCharacterBattle.shieldAmount / 2, () => { });
                BattleHandler.isSelecting = false;
                CharacterBattle.currentCharge++;
                battleHandler.SetPlayerCharacterBattle();
            }
        }
    }

    public void SelectSecondaryCoroutine()
    {
        StartCoroutine(SelectSecondaryTarget());
    }
}
