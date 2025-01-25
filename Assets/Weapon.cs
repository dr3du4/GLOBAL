using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform player; // Referencja do obiektu gracza

    void LateUpdate()
    {
        // Aktualizuj pozycję i rotację broni względem gracza
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
