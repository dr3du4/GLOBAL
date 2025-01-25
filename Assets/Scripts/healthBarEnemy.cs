using System;
using UnityEngine;
using UnityEngine.UI;

public class healthBarEnemy : MonoBehaviour
{
    public Slider healthSlider;
    private Enemy enemyInstance;
    void Start()
    {
        enemyInstance = this.GetComponent<Enemy>();
        healthSlider.maxValue = enemyInstance.maxHealth;
        healthSlider.value = enemyInstance.health;
    }

    void Update()
    {
        healthSlider.value = enemyInstance.health;
    }
   

    void Die()
    {
        Destroy(gameObject);
    }
    
}