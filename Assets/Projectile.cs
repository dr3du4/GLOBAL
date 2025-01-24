using UnityEngine;

// Skrypt obsługujący pocisk
public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; // Czas życia pocisku
    public GameObject explosionEffect; // Prefab efektu eksplozji

    void Start()
    {
        // Zniszczenie pocisku po określonym czasie
        Destroy(gameObject, lifetime);
    }

   void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie kolizji i uruchomienie efektu eksplozji
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Zniszczenie pocisku
        Destroy(gameObject);

        // Możesz tu dodać logikę obrażeń, np. dla przeciwników
    }
    
}