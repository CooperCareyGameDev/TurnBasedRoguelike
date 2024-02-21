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

    private void Awake()
    {
        HideTurnIndicator();
    }

    private void Update()
    {
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

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
    } 

    public bool IsDead()
    {
        return currentHealth < 0;
    }
}
