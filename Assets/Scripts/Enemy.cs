using UnityEngine;

public class Enemy : MonoBehaviour
{
   public int maxHealth = 100;
   public int health = 100;
   
   public void TakeDamage(float damage)
   {
      health -= (int)damage;
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
