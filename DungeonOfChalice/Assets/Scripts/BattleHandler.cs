using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private CharacterBattle playerCharacterBattle;
    public CharacterBattle enemyCharacterBattle;
    
    [SerializeField] private CharacterBattle activeCharacterBattle;

    private State state;
    private float attackTimer = 0f;
    [SerializeField] private float initialAttackTimer = 2.5f; 
    
    [SerializeField] private float turnSwitchDelay = 4f;
    [SerializeField] private Canvas battleCanvas;

    private GameObject[] enemiesArray;
    private GameObject[] playersArray; 
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> players = new List<GameObject>();
    private bool isDead = false; 
    private enum State
    {
        WaitingForPlayer,
        Busy, 
    }

    private void Start()
    {
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playersArray) 
        {
            players.Add(player); 
        }
        foreach (GameObject player in players)
        {
            Debug.Log(player);
        }
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
        }
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
        }
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

    public void UseRageButton()
    {
        if (playerCharacterBattle.currentCharge >= playerCharacterBattle.chargeRequired)
        {
            playerCharacterBattle.currentCharge = 0;
            battleCanvas.enabled = false;
            StartCoroutine(WaitToRage());
        }
        else
        {
            TooltipScreenSpaceUI.ShowTooltipWarning_Static("Not Enough Rage! Wait until rage meter fills up!");
        }
    }
    public void HealButton()
    {
        
        battleCanvas.enabled = false;
        StartCoroutine(WaitToHeal());
    }

    public void ShieldButton()
    {
        battleCanvas.enabled = false;
        StartCoroutine(WaitToShield());
    }
    private IEnumerator WaitToHeal()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        playerCharacterBattle.currentCharge++;
        playerCharacterBattle.HealOnTurn(playerCharacterBattle.healingAmount, () =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
        });
    }

    private IEnumerator WaitToShield()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        playerCharacterBattle.currentCharge++;
        playerCharacterBattle.ShieldOnTurn(playerCharacterBattle.shieldAmount, () =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
        });
    }
    private IEnumerator WaitToRage()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        playerCharacterBattle.UseRageAbility(enemyCharacterBattle, () =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            enemyCharacterBattle.TakeDamage(playerCharacterBattle.ragePower, true);
            StartCoroutine(EnemyAttack());
        });
    }

    private void Update()
    {
        Debug.Log(activeCharacterBattle);
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

    public void SetActiveCharacterBattle(CharacterBattle characterBattle)
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
            Debug.Log(activeCharacterBattle == playerCharacterBattle);
            if (activeCharacterBattle == playerCharacterBattle)
            {
                
                SetActiveCharacterBattle(enemyCharacterBattle);
                PlayerAttackLogic();
                for (int i = 0; i < players.Count; i++)
                {
                    //players[i].GetComponent<CharacterBattle>().ResetShieldToOne(); 
                }
                /*enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                state = State.Busy;
                StartCoroutine(EnemyAttack());*/
                /*enemyCharacterBattle.Attack(playerCharacterBattle, () =>
                {
                    ChooseNextActiveCharacter();
                });*/
            }
            else
            {

                EnemyAttackLogic();
                SetActiveCharacterBattle(playerCharacterBattle);
                Debug.Log("else");
                /*battleCanvas.enabled = true;
                playerCharacterBattle.TakeDamage(enemyCharacterBattle.attackPower, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));
                state = State.WaitingForPlayer;*/
            }

        }
    }

    private void PlayerAttackLogic()
    {
        
        
         enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
         state = State.Busy;
         StartCoroutine(EnemyAttack());
    }

    private void EnemyAttackLogic()
    {
        battleCanvas.enabled = true;
        playerCharacterBattle.TakeDamage(enemyCharacterBattle.attackPower, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));
        state = State.WaitingForPlayer;
        for (int i = 0; i < players.Count; i++)
        {
            //players[i].GetComponent<CharacterBattle>().ResetShieldToOne(); 
        }
    }

    private IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        Debug.Log(enemies.Count);
        /*foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<CharacterBattle>().Attack(playerCharacterBattle, () =>
            {
                Debug.Log(enemy + " attacked!");
                enemies.Remove(enemy);
                Debug.Log(enemies.Count);
                if (enemies.Count <= 0)
                {
                    SetActiveCharacterBattle(playerCharacterBattle);

                }
            });
        }*/
        for (int i = 0; i < enemies.Count; i++)
        {
            yield return new WaitForSeconds(0.75f); 
            Debug.Log(enemies[i]);
            enemies[i].GetComponent<CharacterBattle>().Attack(playerCharacterBattle, () =>
            {
                EnemyAttackLogic();
            });
            if (i >= enemies.Count - 1)
            {
                SetActiveCharacterBattle(playerCharacterBattle);
                for (int j = 0; j < players.Count; j++)
                {
                    players[j].GetComponent<CharacterBattle>().ResetShieldToOne();
                }
            }
                //EnemyAttackLogic();
            //SetActiveCharacterBattle(playerCharacterBattle);
        }
        enemies[0].GetComponent<CharacterBattle>().Attack(playerCharacterBattle, () =>
        {
            //EnemyAttackLogic();
            //SetActiveCharacterBattle(playerCharacterBattle);

            
        });
        
    }
    private bool TestBattleOver()
    {
        if (playerCharacterBattle.IsDead() && false)
        {
            // enemy wins
            BattleOverWindow.Show_Static("Enemy Wins!");
            return true;
        }
        if (enemyCharacterBattle.IsDead() && false)
        {
            // player wins
            BattleOverWindow.Show_Static("Player Wins!");
            return true;
        }
        return false; 
    }

    /*public CharacterBattle SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        return characterBattle;
    }*/
}
