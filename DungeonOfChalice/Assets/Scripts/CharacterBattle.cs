using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class CharacterBattle : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject turnIndicator;
    private HealthSystem healthSystem;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private Transform healthBarScaler;
    [SerializeField] private int attackPower = 10;
    public int critPercentChance = 15;
    public int healingAmount = 15; 
    private int randomNumber;
    private bool isCrit; 

    private void Awake()
    {
        HideTurnIndicator();
    }

    public bool isCriticalHit(int critPercentChance)
    {
        randomNumber = UnityEngine.Random.Range(1, 100);
        Debug.Log(randomNumber.ToString());
        return randomNumber <= critPercentChance; 

    }

    private void Update()
    {
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
        Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
        Debug.Log("Attacked");
        spriteRenderer.color = Color.yellow;
        
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
        
        if (isCrit)
        {
            currentHealth -= damageAmount * 2;
            DamagePopup.Create(transform.position, damageAmount * 2, true);
        }
        else
        {
            currentHealth -= damageAmount;
            DamagePopup.Create(transform.position, damageAmount, false);

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
}
