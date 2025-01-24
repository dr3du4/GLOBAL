using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius = 5f; // Promień eksplozji
    public float explosionForce = 500f; // Siła eksplozji
    public float damage = 50f; // Obrażenia od eksplozji

    void Start()
    {
        // Znalezienie wszystkich obiektów w promieniu eksplozji
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Dodanie siły odrzutu
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Możesz tu dodać logikę obrażeń, np. zmniejszanie zdrowia przeciwników
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        // Zniszczenie efektu eksplozji po jego zakończeniu
        Destroy(gameObject, 2f);
    }
}