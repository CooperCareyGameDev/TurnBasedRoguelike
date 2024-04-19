using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [Header("CharacterBattles")]
    public CharacterBattle playerCharacterBattle;
    public CharacterBattle enemyCharacterBattle;
    public CharacterBattle targetedCharacterBattle;
    [SerializeField] private CharacterBattle activeCharacterBattle;

    private State state;
    private float attackTimer = 0f;
    [Header("Delays")]
    [SerializeField] private float initialAttackTimer = 2.5f; 
    
    [SerializeField] private float turnSwitchDelay = 4f;
    [SerializeField] private Canvas battleCanvas;

    private GameObject[] enemiesArray;
    private GameObject[] playersArray; 
    private CharacterBattle[] characterBattles;
    [Header("Lists")]
    public List<GameObject> enemies = new List<GameObject>();
    public List<CharacterBattle> enemyCharacterBattles = new List<CharacterBattle>();
    public List<GameObject> players = new List<GameObject>();
    private bool isDead = false;
    public static bool isSelecting = false; 
    private enum State
    {
        WaitingForPlayer,
        Busy, 
    }

    private void Start()
    {
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        playersArray = GameObject.FindGameObjectsWithTag("Player");
        playerCharacterBattle = playersArray[0].GetComponent<CharacterBattle>();
        enemyCharacterBattle = enemiesArray[0].GetComponent<CharacterBattle>();
        foreach (GameObject player in playersArray) 
        {
            players.Add(player); 
        }
        foreach (GameObject player in players)
        {
        }
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
        }
        foreach (GameObject enemy in enemies)
        {
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
                //battleCanvas.enabled = false;
                attackTimer = 0;
                state = State.Busy;
                StartCoroutine(ChooseNextActiveCharacter());
                //state = State.WaitingForPlayer;
            }
            else
            {
                TooltipScreenSpaceUI.ShowTooltipWarning_Static("Select a target first");
                SetActiveCharacterBattle(enemies[0].GetComponent<CharacterBattle>());
                //Debug.Log(enemies[0].GetComponent<CharacterBattle>());
            }
        });
    }

    public void UseRageButton()
    {
        if (CharacterBattle.currentCharge >= playerCharacterBattle.chargeRequired)
        {
            CharacterBattle.currentCharge = 0;
            //battleCanvas.enabled = false;
            StartCoroutine(WaitToRage());
        }
        else
        {
            TooltipScreenSpaceUI.ShowTooltipWarning_Static("Not Enough Rage! Wait until rage meter fills up!");
        }
    }
    public void SecondaryAction()
    {
        if (playerCharacterBattle.currentClass == "Knight")
        {
            // Shield ally
            BattleHandler.isSelecting = true;
            
            //StartCoroutine(WaitToShield(playerCharacterBattle.shieldAmount));


        }
        else if (playerCharacterBattle.currentClass == "Barbarian")
        {
            // Buff Ally
            BattleHandler.isSelecting = true;
        }
        else if (playerCharacterBattle.currentClass == "Mage")
        {
            // Give ally retaliatory damage
            BattleHandler.isSelecting = true; 
            //playerCharacterBattle.hasMagicSpike = true; 
            //StartCoroutine(WaitToHeal(0));
        }
        else if (playerCharacterBattle.currentClass == "Archer")
        {
            // Apply evasive, 50% chancee to dodge attack
            BattleHandler.isSelecting = true;
            //StartCoroutine(WaitToHeal(0));
            
        }
        else if (playerCharacterBattle.currentClass == "Cleric")
        {
            // Heal
            BattleHandler.isSelecting = true;
        }
        else if (playerCharacterBattle.currentClass == "King")
        {
            // Apply shield to ally
            BattleHandler.isSelecting = true;
        }
        else if (playerCharacterBattle.currentClass == "Trapper")
        {
            // Apply trap to ally
            BattleHandler.isSelecting = true;
        }
        else if (playerCharacterBattle.currentClass == "Paladin")
        {
            // Heals and applies shield
            BattleHandler.isSelecting = true; 
            
        }
        playerCharacterBattle.hasDoneTurn = true;
        CharacterBattle.turnsLeft--;
        if (CharacterBattle.turnsLeft <= 0)
        {
            StartCoroutine(EnemyAttack());
            CharacterBattle.turnsLeft = CharacterBattle.partyMembersAlive;
            foreach (GameObject player in players)
            {
                player.GetComponent<CharacterBattle>().hasDoneTurn = false;
                Debug.LogError("reset has done turn");
            }
        }
        else
        {
            
            Debug.LogError("Setting Character battle");
        }
        //battleCanvas.enabled = false;
        //StartCoroutine(WaitToHeal());
    }

    public void ShieldButton()
    {
        battleCanvas.enabled = false;
        StartCoroutine(WaitToShield(playerCharacterBattle.shieldAmount));
    }
    private IEnumerator WaitToHeal(int healAmount)
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        CharacterBattle.currentCharge++;
        playerCharacterBattle.HealOnTurn(healAmount, () =>
        {
            //SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            //StartCoroutine(EnemyAttack());
        });
    }

    private IEnumerator WaitToBuff()
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        CharacterBattle.currentCharge++;
        playerCharacterBattle.BuffOnTurn(() =>
        {
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            StartCoroutine(EnemyAttack());
        });
    }

    private IEnumerator WaitToShield(int shieldAmount)
    {
        yield return new WaitForSeconds(turnSwitchDelay);
        state = State.Busy;
        attackTimer = 0;
        CharacterBattle.currentCharge++;
        targetedCharacterBattle.ShieldOnTurn(shieldAmount, () =>
        {
            //SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
            //StartCoroutine(EnemyAttack());
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
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.ragePower, true);
                //StartCoroutine(EnemyAttack());

            }

            else if (playerCharacterBattle.currentClass == "Barbarian")
            {
                // Gives damage buff to all party members, also does slight AOE damage
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().isBuffed = true;
                }
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 4), true);
                }
                //StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Mage")
            {
                // Does damage to all enemies
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 2), true);
                }
                //StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Archer")
            {
                // Damages all enemies, applies poison
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.ragePower / 3), true);
                    // Apply poison
                    enemy.GetComponent<CharacterBattle>().InflictPoison(); 
                }
                //StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Cleric")
            {
                // Heals all allies and cleanses status effects, poison, weakening etc.

                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().HealOnTurn(100, () =>
                    {
                        // Cleanse status effects
                        player.GetComponent<CharacterBattle>().CleanseStatusEffects();
                    });
                }
                //StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "King")
            {
                // Buffs all allies, also applies shield
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().isBuffed = true;
                    player.GetComponent<CharacterBattle>().ShieldOnTurn(10, () =>
                    {
                        //Shields
                    });
                }
                //StartCoroutine(EnemyAttack());
            }

            else if (playerCharacterBattle.currentClass == "Trapper")
            {
                // applies traps to all allies
                //SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                foreach (GameObject player in players)
                {
                    // Place traps
                    player.GetComponent<CharacterBattle>().hasTrap = true;
                    Debug.Log("Placing traps");
                }
                //StartCoroutine(EnemyAttack());                
            }

            else if (playerCharacterBattle.currentClass == "Paladin")
            {
                // Heals entire party and adds shield
                //SetActiveCharacterBattle(enemyCharacterBattle);
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
                    //StartCoroutine(EnemyAttack());
                }
            }
            playerCharacterBattle.hasDoneTurn = true;
            CharacterBattle.turnsLeft--;
            if (CharacterBattle.turnsLeft <= 0)
            {
                StartCoroutine(EnemyAttack());
                CharacterBattle.turnsLeft = CharacterBattle.partyMembersAlive;
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().hasDoneTurn = false;
                    Debug.LogError("reset has done turn");
                }
            }
            else
            {
                SetPlayerCharacterBattle();
                Debug.LogError("Setting Character battle");
            }
        });
    }

    private void Update()
    {
        
        attackTimer += Time.deltaTime; 
        if (state == State.WaitingForPlayer)
        {
            /*if (Input.GetKeyDown(KeyCode.Space) && attackTimer >= initialAttackTimer)
            {
                AttackButton();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                SecondaryAction();
            }*/

        }
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
            }
        }
        //TestBattleOver();
        switch (playerCharacterBattle.currentClass)
        {
            case "Knight": 
                TooltipInfo.tooltipIndex = 0;
                break;
            case "Barbarian":
                TooltipInfo.tooltipIndex = 1;
                break;
            case "Mage": 
                TooltipInfo.tooltipIndex = 2;
                break;
            case "Archer":
                TooltipInfo.tooltipIndex = 3;
                break;
            case "Cleric": 
                TooltipInfo.tooltipIndex = 4;
                break;
            case "King":
                TooltipInfo.tooltipIndex = 5;
                break;
            case "Trapper":
                TooltipInfo.tooltipIndex = 6;
                break;
            case "Paladin": 
                TooltipInfo.tooltipIndex = 7;
                break;
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
            if (activeCharacterBattle == playerCharacterBattle)
            {
                
                //SetActiveCharacterBattle(enemyCharacterBattle);
                PlayerAttackLogic();
                Debug.LogError("Running player attack logic");
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
            playerCharacterBattle.animator.SetTrigger("Attack");
            // Basic attack
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                // Not weak and buffed
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                // weak and not buffed
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Barbarian")
        {
            playerCharacterBattle.animator.SetTrigger("Attack");
            // Basic attack
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Mage")
        {
            // Attack does damage to one target, reduced damage to all targets
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 10, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage((playerCharacterBattle.attackPower / 2) + 5, false);
                }
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage(playerCharacterBattle.attackPower / 3, false);
                }
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {

                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<CharacterBattle>().TakeDamage(playerCharacterBattle.attackPower / 2, false);
                }
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Archer")
        {
            // Attack hits one target, inflicts status effect
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 20, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                // Apply status effect
                enemyCharacterBattle.InflictPoison();
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                // Apply status effect
                enemyCharacterBattle.InflictPoison();
                CharacterBattle.currentCharge++;

            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                // Apply status effect
                enemyCharacterBattle.InflictPoison();
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Cleric")
        {
            // Weak attack with self heal
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage((playerCharacterBattle.attackPower / 2) + 10, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.HealOnTurn(20, () =>
                {
                    // Heal
                });
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.HealOnTurn(10, () =>
                {
                    // Heal
                });
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "King")
        {
            // Weak attack that gives small shield
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage((playerCharacterBattle.attackPower / 2) + 5, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.ShieldOnTurn(15, () =>
                {
                    // Shield
                });
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.ShieldOnTurn(10, () =>
                {
                    // Shield
                });
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Trapper")
        {
            // Attack applies bleed
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 20, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                // apply bleed
                enemyCharacterBattle.AddBleed(1);
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;

            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                // apply bleed
                enemyCharacterBattle.AddBleed(1);
                playerCharacterBattle.isWeakened = false;
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
        }

        else if (playerCharacterBattle.currentClass == "Paladin")
        {
            // Basic Attack
            playerCharacterBattle.animator.SetTrigger("Attack");
            if (playerCharacterBattle.isBuffed && !playerCharacterBattle.isWeakened)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower + 25, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                CharacterBattle.currentCharge++;
            }
            else if (playerCharacterBattle.isWeakened && !playerCharacterBattle.isBuffed)
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower / 2, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
            else
            {
                enemyCharacterBattle.TakeDamage(playerCharacterBattle.attackPower, enemyCharacterBattle.isCriticalHit(playerCharacterBattle.critPercentChance));
                playerCharacterBattle.isBuffed = false;
                playerCharacterBattle.isWeakened = false;
                CharacterBattle.currentCharge++;
            }
        }
        state = State.Busy;
        playerCharacterBattle.hasDoneTurn = true;
        CharacterBattle.turnsLeft--;
        if (CharacterBattle.turnsLeft <= 0)
        {
            StartCoroutine(EnemyAttack());
            CharacterBattle.turnsLeft = CharacterBattle.partyMembersAlive;
            foreach (GameObject player in players)
            {
                player.GetComponent<CharacterBattle>().hasDoneTurn = false;
                Debug.LogError("reset has done turn");
            }
        }
        else
        {
            SetPlayerCharacterBattle();
            Debug.LogError("Setting Character battle");
        }
    }

    private void EnemyAttackLogic()
    {
        battleCanvas.enabled = true;
        if (enemyCharacterBattle.isWeakened)
        {
            playerCharacterBattle.TakeDamage(enemyCharacterBattle.attackPower / 2, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));
            enemyCharacterBattle.isWeakened = false;
        }
        else
        {
            playerCharacterBattle.TakeDamage(enemyCharacterBattle.attackPower, playerCharacterBattle.isCriticalHit(enemyCharacterBattle.critPercentChance));

        }
        if (playerCharacterBattle.hasMagicSpike)
        {
            enemyCharacterBattle.TakeMagicSpikeDamage(playerCharacterBattle.GetComponent<CharacterBattle>().hasMagicSpike);
            playerCharacterBattle.GetComponent<CharacterBattle>().hasMagicSpike = false;
        }
        if (playerCharacterBattle.hasTrap)
        {
            enemyCharacterBattle.isWeakened = true;
            enemyCharacterBattle.TakeTrapDamage(playerCharacterBattle.GetComponent<CharacterBattle>().hasTrap);
            playerCharacterBattle.GetComponent<CharacterBattle>().hasTrap = false;
        }
        enemyCharacterBattle.hasDoneTurn = true;
        SetEnemyCharacterBattle();
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
                enemies[i].GetComponent<CharacterBattle>().TakePoisonDamage();
                EnemyAttackLogic();
            });
            if (i >= enemies.Count - 1)
            {
                SetActiveCharacterBattle(playerCharacterBattle);
                for (int j = 0; j < players.Count; j++)
                {
                    players[j].GetComponent<CharacterBattle>().ResetShieldToOne();
                    if (players[j].GetComponent<CharacterBattle>().isPoisoned)
                    {
                        players[j].GetComponent<CharacterBattle>().TakePoisonDamage();
                    }
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
            // next wave
            return true;
        }
        return false; 
    }


    public void SetPlayerCharacterBattle()
    {
        if (players.Count > 0)
        {
            if (!players[0].GetComponent<CharacterBattle>().hasDoneTurn)
            {
                playerCharacterBattle = players[0].GetComponent<CharacterBattle>();
                SetActiveCharacterBattle(playerCharacterBattle);
            }
            else if (!players[1].GetComponent<CharacterBattle>().hasDoneTurn)
            {
                playerCharacterBattle = players[1].GetComponent<CharacterBattle>();
                SetActiveCharacterBattle(playerCharacterBattle);
            }
            else if (!players[2].GetComponent<CharacterBattle>().hasDoneTurn)
            {
                playerCharacterBattle = players[2].GetComponent<CharacterBattle>();
                SetActiveCharacterBattle(playerCharacterBattle);
            }
            else if (!players[3].GetComponent<CharacterBattle>().hasDoneTurn)
            {
                playerCharacterBattle = players[3].GetComponent<CharacterBattle>();
                SetActiveCharacterBattle(playerCharacterBattle);
            }
            else
            {
                // Let enemy attack
                
                foreach (GameObject player in players)
                {
                    player.GetComponent<CharacterBattle>().hasDoneTurn = false;
                    
                }
            }
        }
    }

    private int DetermineEnemyCharacterBattleIndex(int input)
    {
        if (input > (enemies.Count - 1))
        {
            return (enemies.Count - 1); 
        }
        else
        {
            return input;
        }
    }
    public void SetEnemyCharacterBattle()
    {
        if (enemies.Count > 0)
        {
            if (!enemies[DetermineEnemyCharacterBattleIndex(0)].GetComponent<CharacterBattle>().hasDoneTurn && enemies[DetermineEnemyCharacterBattleIndex(0)] != null)
            {
                enemyCharacterBattle = enemies[0].GetComponent<CharacterBattle>();
                Debug.LogError("setenemycharacterbattle");
            }
            else if (!enemies[DetermineEnemyCharacterBattleIndex(1)].GetComponent<CharacterBattle>().hasDoneTurn && enemies[DetermineEnemyCharacterBattleIndex(1)] != null)
            {
                enemyCharacterBattle = enemies[1].GetComponent<CharacterBattle>();
                Debug.LogError("setenemycharacterbattle");
            }
            else if (enemies[DetermineEnemyCharacterBattleIndex(2)] != null && !enemies[DetermineEnemyCharacterBattleIndex(2)].GetComponent<CharacterBattle>().hasDoneTurn)
            {
                enemyCharacterBattle = enemies[2].GetComponent<CharacterBattle>();
            }
            else if (!enemies[DetermineEnemyCharacterBattleIndex(3)].GetComponent<CharacterBattle>().hasDoneTurn && enemies[DetermineEnemyCharacterBattleIndex(3)] != null)
            {
                enemyCharacterBattle = enemies[3].GetComponent<CharacterBattle>();
            }  
            else if (!enemies[DetermineEnemyCharacterBattleIndex(4)].GetComponent<CharacterBattle>().hasDoneTurn && enemies[DetermineEnemyCharacterBattleIndex(4)] != null)
            {
                enemyCharacterBattle = enemies[4].GetComponent<CharacterBattle>();
            }
            else
            {
                foreach (GameObject enemy in enemies)
                {
                    Debug.LogError("Foreach");
                    enemy.GetComponent<CharacterBattle>().hasDoneTurn = false;
                }
            }
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

    public void WaitToHealCoroutine(int amount)
    {
        StartCoroutine(WaitToHeal(amount));
    }

    public void WaitToShieldCoroutine(int amount)
    {
        StartCoroutine(WaitToShield(amount));
    }
}
