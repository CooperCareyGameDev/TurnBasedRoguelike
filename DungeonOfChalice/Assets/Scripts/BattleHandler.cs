using System;
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
    private CharacterBattle[] characterBattles;
    public List<GameObject> enemies = new List<GameObject>();
    public List<CharacterBattle> enemyCharacterBattles = new List<CharacterBattle>();
    public List<GameObject> players = new List<GameObject>();
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
            //Debug.Log(player);
        }
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
        }
        foreach (GameObject enemy in enemies)
        {
            //Debug.Log(enemy);
        }
        SetActiveCharacterBattle(playerCharacterBattle);
        state = State.WaitingForPlayer;
    }

    public void AttackButton()
    {
        
        playerCharacterBattle.Attack(enemyCharacterBattle, () =>
        {

            if (activeCharacterBattle != null)
            {
                battleCanvas.enabled = false;
                attackTimer = 0;
                state = State.Busy;
                StartCoroutine(ChooseNextActiveCharacter());
                //state = State.WaitingForPlayer;
            }
            else
            {
                TooltipScreenSpaceUI.ShowTooltipWarning_Static("Select a target first");
                SetActiveCharacterBattle(enemies[0].GetComponent<CharacterBattle>());
                Debug.Log(enemies[0].GetComponent<CharacterBattle>());
            }
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
        Debug.Log("Added Charge");
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
        Debug.Log("Added Charge");
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
            if (playerCharacterBattle.currentClass == "Knight")
            {
                // Hits one enemy for high damage
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.ragePower, true);
                StartCoroutine(EnemyAttack());

            }

            else if (playerCharacterBattle.currentClass == "Barbarian")
            {
                // Gives damage buff to all party members, also does slight AOE damage
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().isBuffed = true;
                }
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 4), true);
                }
                StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Mage")
            {
                // Does damage to all enemies
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 2), true);
                }
                StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Archer")
            {
                // Damages all enemies, applies poison
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 3), true);
                    // Apply poison
                }
                StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Cleric")
            {
                // Heals all allies and cleanses status effects, poison, weakening etc.

                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().HealOnTurn(100, () =>
                    {
                        // Cleanse status effects
                    });
                }
                StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "King")
            {
                // Buffs all allies, also applies shield
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().isBuffed = true;
                    player.GetComponent<CharacterBattle>().ShieldOnTurn(10, () =>
                    {
                        //Shields
                    });
                }
                StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Trapper")
            {
                // applies traps to all allies
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    // Place traps
                    Debug.Log("Placing traps");
                }
                StartCoroutine(EnemyAttack());                
            }

            else if (playerCharacterBattle.currentClass == "Paladin")
            {
                // Heals entire party and adds shield
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().HealOnTurn(75, () =>
                    {
                        // Heal
                    });
                    player.GetComponent<CharacterBattle>().ShieldOnTurn(40, () =>
                    {
                        // Shield
                    }); 
                    StartCoroutine(EnemyAttack());
                }
            }
        });
    }

    private void Update()
    {
        //Debug.Log(activeCharacterBattle);
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
                /*battleCanvas.enabled = true;
                playerCharacterBattle.TakeDamage(enemyCharacterBattle.attackPower, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));
                state = State.WaitingForPlayer;*/
            }

        }
    }

    private void PlayerAttackLogic()
    {
        if (playerCharacterBattle.currentClass == "Knight")
        {
            // Basic attack
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
            }
        }

        else if (playerCharacterBattle.currentClass == "Barbarian")
        {
            // Basic attack
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
            }
        }

        else if (playerCharacterBattle.currentClass == "Mage")
        {
            // Attack does damage to one target, reduced damage to all targets
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 10, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.attackPower / 2) + 5, false);
                }
                playerCharacterBattle.isBuffed = false;
            }
            else
            {

                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage(playerCharacterBattle.attackPower / 2, false);
                }
            }
        }

        else if (playerCharacterBattle.currentClass == "Archer")
        {
            // Attack hits one target, inflicts status effect
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 20, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                // Apply status effect
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                // Apply status effect
            }
        }

        else if (playerCharacterBattle.currentClass == "Cleric")
        {
            // Weak attack with self heal
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage((playerCharacterBattle.attackPower / 2) + 10, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.HealOnTurn(20, () =>
                {
                    // Heal
                });
                playerCharacterBattle.isBuffed = false;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.HealOnTurn(10, () =>
                {
                    // Heal
                });
            }
        }

        else if (playerCharacterBattle.currentClass == "King")
        {
            // Weak attack that gives small shield
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage((playerCharacterBattle.attackPower / 2) + 5, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.ShieldOnTurn(15, () =>
                {
                    // Shield
                });
                playerCharacterBattle.isBuffed = false;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.ShieldOnTurn(10, () =>
                {
                    // Shield
                });
            }
        }

        else if (playerCharacterBattle.currentClass == "Trapper")
        {
            // Attack applies bleed
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 20, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                // apply bleed
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                // apply bleed
            }
        }

        else if (playerCharacterBattle.currentClass == "Paladin")
        {
            // Basic Attack
            if (playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
            }
        }
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
        //Debug.Log(enemies.Count);
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
            //Debug.Log(enemies[i]);
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
        if (enemies[0] != null)
        {
            enemies[0].GetComponent<CharacterBattle>().Attack(playerCharacterBattle, () =>
            {
                //EnemyAttackLogic();
                //SetActiveCharacterBattle(playerCharacterBattle);

            
            });
        }
        
    }
    private bool TestBattleOver()
    {
        if (playerCharacterBattle.IsDead())
        {
            // enemy wins
            BattleOverWindow.Show_Static("Enemy Wins!");
            return true;
        }
        if (enemies.Count <= 0)
        {
            // player wins
            BattleOverWindow.Show_Static("Player Wins!");
            return true;
        }
        return false; 
    }

    public void SetEnemyCharacterBattle()
    {
        if (enemies.Count > 0)
        {
            enemyCharacterBattle = enemies[0].GetComponent<CharacterBattle>(); 
        }
        else
        {
            enemyCharacterBattle = null;
            SetActiveCharacterBattle(playerCharacterBattle);
            if (Relics.healingBuff)
            {
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().HealOnTurn(30, () => {
                        Debug.Log("Healed on turn");
                    });
                   //DamagePopup.CreateHealPopup(transform.position, 200);
                }
            }
        }
    }

    /*public CharacterBattle SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        return characterBattle;
    }*/
}
