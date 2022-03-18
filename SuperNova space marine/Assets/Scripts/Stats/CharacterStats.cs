using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int currentHealth { get; private set; }
    private int maxHealthValueCache;
    public Stat maxHealth;
    public Stat damage;
    public Stat armor;

    public bool isHit = false;

    public virtual void Awake()
    {
        currentHealth = maxHealth.GetValue();
        maxHealthValueCache = maxHealth.GetValue();
    }
    
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage,1, int.MaxValue);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        isHit = true;
    }

    public void Heal(int healAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealthValueCache)//doesn't allow the character to go above the max health
            {
                currentHealth = maxHealthValueCache;
            }
        }
    }

    public virtual void Die()
    {
        //this method is meant to be overwriten

    }
}
