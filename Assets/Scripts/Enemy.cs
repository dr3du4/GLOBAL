using UnityEngine;

public class Enemy : MonoBehaviour
{
   public int maxHealth = 100;
   public float health = 100;
   public AudioSource audioSource; // Referencja do AudioSource
   public AudioClip damageSound;
   
   public void TakeDamage(float damage)
   {
      health -= damage;
      if (audioSource != null && damageSound != null)
      {
         audioSource.PlayOneShot(damageSound); // Graj dźwięk strzału
      }
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
