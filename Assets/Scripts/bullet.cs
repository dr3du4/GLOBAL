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
}
