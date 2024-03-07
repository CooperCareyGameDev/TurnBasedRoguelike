using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterBattle : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject turnIndicator;
    private HealthSystem healthSystem;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int currentShield = 0; 
    [SerializeField] private Transform healthBarScaler;
    public int attackPower = 10;
    public int critPercentChance = 15;
    public int healingAmount = 15;
    public int chargeRequired = 5;
    public int currentCharge = 0;
    public int shieldAmount = 25; 
    [SerializeField] private TextMeshProUGUI rageChargeText;
    public int ragePower = 0;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText; 
    private int randomNumber;
    private bool isCrit;
    private bool isDead = false; 
    private void Awake()
    {
        if (Relics.critChanceBuff)
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
        Debug.Log(critPercentChance);
        randomNumber = UnityEngine.Random.Range(1, 100);
        Debug.Log(randomNumber.ToString());
        return randomNumber <= critPercentChance; 

    }

    private void Update()
    {
        Debug.Log(isDead);
        shieldText.text = currentShield + " shield";
        if (currentHealth <= 0)
        {
            isDead = true; 
        }
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
        Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
        Debug.Log("Attacked");
        spriteRenderer.color = Color.yellow;
        currentCharge++;
        //targetCharacterBattle.TakeDamage(attackPower);
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
    public void UseRageAbility(CharacterBattle targetCharacterBattle, Action onRageComplete) 
    {
        Debug.Log("Used Rage");
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
        Debug.Log(healthSystem.GetHealth());
    }

    public void TakeDamage(int damageAmount, bool isCrit)
    {
        Debug.Log("Taking damage");
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
        return currentHealth <= 0;
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
        Debug.Log("Reset shield to one");
        if (Relics.shieldBuff)
        {
            currentShield = 5;
        }
        else
        {
            currentShield = 0;
        }
    }

    public void IncreaseShield(int amount)
    {
        currentShield += amount;
    }
}
