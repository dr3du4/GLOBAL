using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public Transform firePoint; // Punkt, z którego strzela przeciwnik
    public GameObject projectilePrefab; // Prefab pocisku
    public float fireForce = 20f; // Siła pocisku
    public float fireRate = 1f; // Czas pomiędzy strzałami (w sekundach)
    public string playerTag = "Player"; // Tag gracza
    public Transform playerTransform; // Referencja do gracza (ustawiona w inspektorze)

    private bool isPlayerInRange = false; // Czy gracz jest w zasięgu?
    private float nextFireTime = 0f; // Czas do następnego strzału
    private Vector3 targetPosition; // Pozycja gracza w momencie strzału

    void OnTriggerEnter(Collider other)
    {
        // Sprawdź, czy obiekt, który wszedł w trigger, to gracz
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = true;
            playerTransform = other.transform; // Przypisz transform gracza
            Debug.Log("Player In Range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Sprawdź, czy obiekt, który opuścił trigger, to gracz
        if (other.CompareTag(playerTag))
        {
            isPlayerInRange = false;
            playerTransform = null; // Usuń referencję do gracza
        }
    }

    void Update()
    {
        // Jeśli gracz jest w zasięgu, przeciwnik obraca się w jego kierunku
        if (isPlayerInRange && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            lookRotation *= Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Płynny obrót
        }

        // Jeśli gracz jest w zasięgu i czas do następnego strzału minął, strzel
        if (isPlayerInRange && Time.time >= nextFireTime)
        {
            targetPosition = playerTransform.position; // Zapamiętaj pozycję gracza
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
            // Wyznacz kierunek od firePoint do zapamiętanej pozycji gracza
            Vector3 shootDirection = (targetPosition - firePoint.position).normalized;
            rb.AddForce(shootDirection * fireForce, ForceMode.Impulse); // Nadaj pociskowi siłę
        }
    }
}