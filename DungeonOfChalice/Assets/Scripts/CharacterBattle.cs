using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBattle : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    public string currentClass = "Knight";
    [SerializeField] private GameObject turnIndicator;
    private HealthSystem healthSystem;
    [Header("Health")]
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int currentShield = 0; 
    [SerializeField] private Transform healthBarScaler;
    [Header("Combat")]
    public int attackPower = 10;
    private int startingAttackPower = 0; 
    public int critPercentChance = 15;
    private int startingCritChance = 0; 
    public int healingAmount = 15;
    private int startingHealAmount = 0; 
    public int chargeRequired = 5;
    public int currentCharge = 0;
    public int shieldAmount = 25;
    private int startingShieldAmount = 0; 
    public int ragePower = 0;
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI rageChargeText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private bool isEnemy = false; 
    private int randomNumber;
    private bool isCrit;
    private bool isDead = false; 
    private BattleHandler battleHandler;
    [Header("Buffs")]
    [SerializeField] private int damageBuffStacks = 0;
    [SerializeField] private int critBuffStacks = 0;
    [SerializeField] private int healBuffStacks = 0;
    [SerializeField] private int shieldBuffStacks = 0;
    public bool isBuffed = false;
    public bool hasMagicSpike = false;
    [SerializeField] private int magicSpikeDamage = 10;
    public bool hasTrap = false;
    [SerializeField] private int trapDamage = 10;
    [Header("Debuffs")]
    public bool isPoisoned = false;
    [SerializeField] private int poisonDamage = 5;
    [SerializeField] private int currentBleed = 0;
    [SerializeField] private int bleedRequired = 2;
    [SerializeField] private int bleedDamage = 25;
    public bool isWeakened = false;
    [Header("Player Turn Management")]
    public static int partyMembersAlive = 1;
    public static int turnsLeft = 1;
    public bool hasDoneTurn = false;
    //private GameObject[] playerArray;
    //public List<GameObject> playerList = new List<GameObject>();
    [SerializeField] private int orderIndex; 
    private void Awake()
    {
        //playerArray = GameObject.FindGameObjectsWithTag("Player");
        
        /*foreach (GameObject player in playerArray)
        {
            playerList.Add(player);
            Debug.LogError("Added element to list");
        }*/
        partyMembersAlive = battleHandler.players.Count;
        //Debug.LogError(playerArray.Length);
        //Debug.LogError(playerList.Count);
        if (currentClass != "Knight" && currentClass != "Barbarian" && currentClass != "Mage" && currentClass != "Archer" && currentClass != "Cleric" && currentClass != "King" && currentClass != "Trapper" && currentClass != "Paladin" && !isEnemy)
        {
            Debug.LogError($"{currentClass} is not a valid character class name for " + gameObject.name);
        }
        if (!isEnemy)
        {
            switch (currentClass)
            {
                case "Knight":
                    orderIndex = 0; break;
                case "Barbarian":
                    orderIndex = 1; break;
                case "Mage":
                    orderIndex = 2; break;
                case "Archer":
                    orderIndex = 3; break;
                case "Cleric":
                    orderIndex = 4; break;
                case "King":
                    orderIndex = 5; break;
                case "Trapper":
                    orderIndex = 6; break;
                case "Paladin":
                    orderIndex = 7; break;
                default:
                    Debug.LogError("Incorrect class name"); break; 
            }

        }
        startingAttackPower = attackPower;
        startingCritChance = critPercentChance;
        startingHealAmount = healingAmount;
        startingShieldAmount = shieldAmount;
        battleHandler = FindFirstObjectByType<BattleHandler>();
        if (Relics.critChanceBuff && !isEnemy)
        {
            critPercentChance += 10; 
        }
        if (Relics.rageBuff)
        {
            chargeRequired--;
        }
        HideTurnIndicator();
        ragePower /= 2;
    }


    public bool isCriticalHit(int critPercentChance)
    {
        //Debug.Log(critPercentChance);
        randomNumber = UnityEngine.Random.Range(1, 100);
        //Debug.Log(randomNumber.ToString());
        return randomNumber <= critPercentChance; 

    }

    private void Update()
    {
        partyMembersAlive = battleHandler.players.Count;
        
        attackPower = startingAttackPower + (10 * damageBuffStacks);
        critPercentChance = startingCritChance + (10 * critBuffStacks);
        healingAmount = startingHealAmount + (10 * healBuffStacks);
        shieldAmount = startingShieldAmount + (10 * shieldBuffStacks);
        //Debug.Log(isDead);
        shieldText.text = currentShield + " shield";
        
        if (currentHealth <= 0 && Relics.hasSecondChance && !isEnemy)
        {
            currentHealth = startingHealth;
            Relics.hasSecondChance = false;
            isDead = false;
        }
        else if (currentHealth <= 0 && !Relics.hasSecondChance)
        {
            //isDead = true;
        }
        else
        {
            isDead = false;
        }

        if (currentHealth <= 0 && isEnemy)
        {
            battleHandler.enemies.Remove(gameObject);
            battleHandler.SetEnemyCharacterBattle(); 
            Destroy(gameObject);
        }
        if (currentHealth <= 0 && !isEnemy)
        {
            battleHandler.players.Remove(gameObject);
            Debug.LogError("Removed object");
            foreach (GameObject player in battleHandler.players)
            {
                //Debug.LogError(player.name);
            }
            Debug.LogError(battleHandler.players.Count);
            Destroy(gameObject);
        }
        Debug.LogError(battleHandler.players.Count);
        healthText.text = $"Health: {currentHealth} / {startingHealth}";
        rageChargeText.text = $"Rage: {currentCharge} / {chargeRequired}"; 
        if (currentCharge > chargeRequired)
        {
            currentCharge = chargeRequired;
        }
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        float healthScaleFactor = (float)currentHealth / startingHealth * 1500;
        if (healthScaleFactor <= 0)
        {
            healthScaleFactor = 0;
        }
        healthBarScaler.localScale = new Vector3(healthScaleFactor, 100);
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthSystem = new HealthSystem(startingHealth);
    }
    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        if (isDead) { return; }
        spriteRenderer.color = Color.yellow;
        currentCharge++;
        onAttackComplete();
    }

    public void HealOnTurn(int healAmount, Action onHealComplete)
    {
        currentHealth += healAmount;
        DamagePopup.CreateHealPopup(transform.position, healAmount);
        //DamagePopup.CreateHealPopup(healAmount);
        onHealComplete();
    } 

    public void ShieldOnTurn(int shieldAmount, Action onShieldComplete)
    {
        currentShield += shieldAmount;
        DamagePopup.CreateShieldPopup(transform.position, shieldAmount);
        onShieldComplete();
    }

    public void BuffOnTurn(Action onBuffComplete)
    {
        isBuffed = true;
        onBuffComplete();
    }
    public void UseRageAbility(CharacterBattle targetCharacterBattle, Action onRageComplete) 
    {
        spriteRenderer.color = Color.cyan;
        currentCharge = 0;
        onRageComplete();

    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void HideTurnIndicator()
    {
        turnIndicator.SetActive(false);
    }

    public void ShowTurnIndicator()
    {
        turnIndicator.SetActive(true);
    }


    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    public void TakeDamage(int damageAmount, bool isCrit)
    {
        //Debug.Log("Taking damage");
        if (isCrit)
        {
            if (currentShield > damageAmount * 2)
            {
                currentShield -= damageAmount * 2;
                DamagePopup.Create(transform.position, 0, true);
            }
            else if (currentShield <= damageAmount * 2)
            {
                currentHealth -= (damageAmount * 2 - currentShield);
                DamagePopup.Create(transform.position, damageAmount * 2 - currentShield, true);
                currentShield = 0;

            }
            //currentHealth -= damageAmount * 2;
            //DamagePopup.Create(transform.position, damageAmount * 2, true);
        }
        else
        {
            if (currentShield > damageAmount)
            {
                currentShield -= damageAmount;
                DamagePopup.Create(transform.position, 0, false);
            }
            else if (currentShield <= damageAmount)
            {
                currentHealth -= (damageAmount - currentShield);
                DamagePopup.Create(transform.position, damageAmount - currentShield, false);
                currentShield = 0;
            }
            //currentHealth -= damageAmount;
            //DamagePopup.Create(transform.position, damageAmount, false);

        }

    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
    } 

    public bool IsDead()
    {
        if (currentHealth <= 0 && Relics.hasSecondChance)
        {
            currentHealth = startingHealth;
            Relics.hasSecondChance = false; 
            return false;
        }
        else if (currentHealth <= 0 && !Relics.secondChanceBuff)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetStartingHealth()
    {
        return startingHealth;
    }

    public void ResetShieldToOne()
    {
        //Debug.Log("Reset shield to one");
        if (Relics.shieldBuff)
        {
            currentShield = 5;
        }
        else
        {
            currentShield = 0;
        }
    }

    public void TakePoisonDamage()
    {
        if (isPoisoned)
        {
            TakeDamage(poisonDamage, false);
            isPoisoned = false;
        }
    }

    public void InflictPoison()
    {
        isPoisoned = true;
    }

    public void TakeMagicSpikeDamage(bool hasSpike)
    {
        if (hasSpike)
        {
            TakeDamage(magicSpikeDamage, false);
            hasMagicSpike = false;
        }
    }
    
    public void TakeTrapDamage(bool hasTrap)
    {
        if (hasTrap)
        {
            TakeDamage(trapDamage, false);
            hasTrap = false;
        }
    }

    public void GiveMagicSpikes()
    {
        hasMagicSpike = true; 
    }

    public void RemoveMagicSpikes()
    {
        hasMagicSpike = false;
    }

    public void CleanseStatusEffects()
    {
        isPoisoned = false;
        currentBleed = 0;
    }

    public void IncreaseShield(int amount)
    {
        currentShield += amount;
    }

    public void AddBleed(int amount)
    {
        currentBleed += amount;
        if (currentBleed >= bleedRequired)
        {
            TakeBleedDamage();
            currentBleed = 0;
        }
    }

    public void TakeBleedDamage()
    {
        TakeDamage(bleedDamage, false);
    }

    public void InflictWeaken()
    {
        isWeakened = true;
    }

    public void RemoveWeaken()
    {
        isWeakened = false;
    }

    public void SetTrap()
    {
        hasTrap = true;
    }

    public void RemoveTrap()
    {
        hasTrap = false;
    }
}
