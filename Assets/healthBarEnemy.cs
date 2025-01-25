using System;
using UnityEngine;
using UnityEngine.UI;

public class healthBarEnemy : MonoBehaviour
{
    public float currentHealth;
    public Slider healthSlider;
    private Enemy enemyInstance;
    void Start()
    {
        enemyInstance = this.GetComponent<Enemy>();
        currentHealth = enemyInstance.maxHealth;
        healthSlider.maxValue = enemyInstance.maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        enemyInstance.health = (int)currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}