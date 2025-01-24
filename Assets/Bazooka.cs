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

    void Update()
    {
        // Strzał z bazooki
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Tworzenie pocisku
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Nadanie pociskowi siły
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }

        // Rozpoczęcie czasu przeładowania
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}