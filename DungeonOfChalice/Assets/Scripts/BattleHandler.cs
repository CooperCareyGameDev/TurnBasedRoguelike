using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private CharacterBattle playerCharacterBattle;
    [SerializeField] private CharacterBattle enemyCharacterBattle; 
    private CharacterBattle activeCharacterBattle;
    private State state;
    private float attackTimer = 0f;
    [SerializeField] private float initialAttackTimer = 2.5f; 
    
    [SerializeField] private float turnSwitchDelay = 4f; 

    private enum State
    {
        WaitingForPlayer,
        Busy, 
    }

    private void Start()
    {
        SetActiveCharacterBattle(playerCharacterBattle);
        state = State.WaitingForPlayer;
    }
    private void Update()
    {
        attackTimer += Time.deltaTime; 
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && attackTimer >= initialAttackTimer)
            {
                attackTimer = 0;
                state = State.Busy;
                playerCharacterBattle.Attack(enemyCharacterBattle, () =>
                {
                    Debug.Log("Next Character");
                    //ChooseNextActiveCharacter();
                    StartCoroutine(ChooseNextActiveCharacter());
                    //state = State.WaitingForPlayer;
                });
            }

        }
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        if (activeCharacterBattle != null)
        {
            activeCharacterBattle.HideTurnIndicator();
        }
        activeCharacterBattle = characterBattle; 
        activeCharacterBattle.ShowTurnIndicator();
    }

    private IEnumerator ChooseNextActiveCharacter()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        if (activeCharacterBattle == playerCharacterBattle)
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
            /*enemyCharacterBattle.Attack(playerCharacterBattle, () =>
            {
                ChooseNextActiveCharacter();
            });*/
        }
        else
        {
            SetActiveCharacterBattle(playerCharacterBattle);
            state = State.WaitingForPlayer;
        }
    }

    private IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        enemyCharacterBattle.Attack(playerCharacterBattle, () =>
        {
            
            StartCoroutine(ChooseNextActiveCharacter());
        });
    }
}
