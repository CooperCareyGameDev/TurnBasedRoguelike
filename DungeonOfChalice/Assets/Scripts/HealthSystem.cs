using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class HealthSystem 
{
    public event EventHandler OnHealthChanged; 
    private int health;
    private int maxHealth; 
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;

    }

    public int GetHealth() { return health; }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) { health = 0; }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) { health = maxHealth; }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public float GetHealthPercent()
    {
        return (float)health / maxHealth * 1500;
    }
}
