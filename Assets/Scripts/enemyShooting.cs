using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public Transform firePoint; // Punkt, z którego strzela przeciwnik
    public GameObject projectilePrefab; // Prefab pocisku
    public float fireForce = 20f; // Siła pocisku
    public float fireRate = 1f; // Czas pomiędzy strzałami (w sekundach)
    public string playerTag = "Player"; // Tag gracza

    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu?
    private float nextFireTime = 0f; // Czas do następnego strzału

    void OnTriggerEnter(Collider other)
    {
        // Sprawdź, czy obiekt, który wszedł w trigger, to gracz
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Sprawdź, czy obiekt, który opuścił trigger, to gracz
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        // Jeśli gracz jest w zasięgu i czas do następnego strzału minął, strzel
        if (isPlayerInRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Ustaw czas kolejnego strzału
        }
    }

    void Shoot()
    {
        // Stwórz pocisk w punkcie firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse); // Nadaj pociskowi siłę
        }
    }
}
