using System;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public ParticleSystem fireEffect; // Referencja do systemu cząsteczek
    public float damage = 10f;       // Obrażenia
    public float range = 5f;         // Zasięg miotacza ognia
    public LayerMask damageLayer;    // Warstwa obiektów, które mogą być trafione

    private bool isFiring = false;

    void Update()
    {
        // Włącz/wyłącz ogień
        if (Input.GetMouseButton(0)) // LPM do aktywacji
        {
            Debug.Log("Fire");
            StartFiring();
        }
        else
        {
            StopFiring();
        }

        // Zadaj obrażenia w trakcie strzelania
        if (isFiring)
        {
            ApplyDamage();
        }
    }

    void StartFiring()
    {
        if (!isFiring)
        {
            fireEffect.Play(); // Uruchom cząsteczki
            isFiring = true;
        }
    }

    void StopFiring()
    {
        if (isFiring)
        {
            fireEffect.Stop(); // Zatrzymaj cząsteczki
            isFiring = false;
        }
    }

    void ApplyDamage()
    {
        // Znajdź wszystkie obiekty w zasięgu
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position + transform.forward * range / 2, range, damageLayer);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Trafiono obiekt: " + hitCollider.name); // Wyświetla nazwę trafionego obiektu

            // Zadaj obrażenia, jeśli obiekt ma komponent "Health"
            var enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage * Time.deltaTime*10); // Obrażenia w czasie
                Debug.Log("Zadano obrażenia: " +damage * Time.deltaTime*10);
            }
            else
            {
                Debug.Log("Obiekt " + hitCollider.name + " nie ma komponentu Enemy!");
            }
        }
    }
}