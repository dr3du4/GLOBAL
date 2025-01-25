using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifetime = 5f; // Czas życia pocisku
    public int damage = 10; // Ilość obrażeń, które zadaje pocisk

    void Start()
    {
        // Zniszcz pocisk po określonym czasie
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdź, czy trafiony obiekt ma gracza lub inny cel do zniszczenia
        if (collision.gameObject.CompareTag("Player"))
        {
            // Przykład: zmniejszenie życia gracza
            playerHealth playerHealth = collision.gameObject.GetComponent<playerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Zniszcz pocisk po kolizji
        Destroy(gameObject);
    }
}
