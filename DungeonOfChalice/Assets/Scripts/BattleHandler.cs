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
    [SerializeField] private Canvas battleCanvas; 
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

    public void AttackButton()
    {
        battleCanvas.enabled = false; 
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


    public void HealButton()
    {
        /*attackTimer = 0;
        state = State.Busy;
        playerCharacterBattle.HealOnTurn(playerCharacterBattle.healingAmount, () =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
        });*/
        battleCanvas.enabled = false;
        StartCoroutine(WaitToHeal());
    }
    private IEnumerator WaitToHeal()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        playerCharacterBattle.HealOnTurn(playerCharacterBattle.healingAmount, () =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
        });
    }
    private void Update()
    {
        attackTimer += Time.deltaTime; 
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && attackTimer >= initialAttackTimer)
            {
                AttackButton();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                HealButton();
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
        if (!TestBattleOver())
        {
            yield return new WaitForSeconds(turnSwitchDelay);
            if (activeCharacterBattle == playerCharacterBattle)
            {
                
                SetActiveCharacterBattle(enemyCharacterBattle);
                enemyCharacterBattle.TakeDamage(10, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
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
                battleCanvas.enabled = true;
                playerCharacterBattle.TakeDamage(10, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));
                state = State.WaitingForPlayer;
            }

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
    private bool TestBattleOver()
    {
        if (playerCharacterBattle.IsDead())
        {
            // enemy wins
            BattleOverWindow.Show_Static("Enemy Wins!");
            return true;
        }
        if (enemyCharacterBattle.IsDead())
        {
            // player wins
            BattleOverWindow.Show_Static("Player Wins!");
            return true;
        }
        return false; 
    }
}
