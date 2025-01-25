using UnityEngine;

public class Enemy : MonoBehaviour
{
   public int maxHealth = 100;
   public float health = 100;
   
   public void TakeDamage(float damage)
   {
      health -= damage;
      if (health <= 0)
      {
         Die();
      }
   }
   void Die()
   {
      Destroy(gameObject);
   }
}
