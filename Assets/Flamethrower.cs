using System;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public ParticleSystem fireEffect; // Referencja do systemu cząsteczek
    public float damage = 10f;       // Obrażenia
    public float range = 5f;         // Zasięg miotacza ognia
    public LayerMask damageLayer;    // Warstwa obiektów, które mogą być trafione

    private bool isFiring = false;
    public Transform target; // Obiekt, który system cząsteczek ma śledzić (np. broń)
    

    void LateUpdate()
    {
        if (target != null)
        {
            // Dopasowanie pozycji i rotacji systemu cząsteczek do broni/postaci
            //fireEffect.transform.position = target.position;
            var vector=new Vector3(180f, 0f, 0f);
            fireEffect.transform.rotation = target.rotation* Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        // Włącz/wyłącz ogień
        if (Input.GetMouseButton(0)) // LPM do aktywacji
        {
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

    public void StartFiring()
    {
        if (!isFiring)
        {
            fireEffect.Play(); // Uruchom cząsteczki
            isFiring = true;
        }
    }

    public void StopFiring()
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

            // Zadaj obrażenia, jeśli obiekt ma komponent "Enemy"
            var enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage * Time.deltaTime * 10); // Obrażenia w czasie
            }
        }
    }
}