using System.Collections;
using UnityEngine;

// Skrypt obsługujący bazookę
public class Bazooka : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab pocisku
    public Transform firePoint; // Punkt, z którego wystrzeliwany jest pocisk
    public float fireForce = 20f; // Siła wystrzału
    public float reloadTime = 1.5f; // Czas przeładowania

    private bool canShoot = true;

    public void Shoot(Vector3 WorldPointTarget)
    {
        Debug.Log("Attempt to shoot");
        if (canShoot == false) {
            return;
        }
        Debug.Log("Shots should be fired");
        // Tworzenie pocisku
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Nadanie pociskowi siły w podanego punktu w świecie
            rb.linearVelocity = (WorldPointTarget - firePoint.position).normalized * fireForce;
        }

        // Rozpoczęcie czasu przeładowania
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        canShoot = false;
        Debug.Log("Realod started");
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        Debug.Log("Reload Done");
    }
}