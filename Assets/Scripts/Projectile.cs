using System;
using UnityEngine;

// Skrypt obsługujący pocisk
public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; // Czas życia pocisku
    public GameObject explosionEffect; // Prefab efektu eksplozji
    public float damage = 10f;
    void Start()
    {
        // Zniszczenie pocisku po określonym czasie
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            return;
        }
        // Sprawdzenie kolizji i uruchomienie efektu eksplozji
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        // Zniszczenie pocisku
        Destroy(gameObject);

        // Możesz tu dodać logikę obrażeń, np. dla przeciwników
    }
    
}