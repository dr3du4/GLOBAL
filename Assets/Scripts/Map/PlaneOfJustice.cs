using UnityEngine;
using UnityEngine.Events;

namespace Map {
    public class PlaneOfJustice : MonoBehaviour
    {
        public UnityEvent onPlayerEnter;

        private void OnTriggerEnter(Collider other) {
            Debug.Log("Something touched Plane Of Justice");
            if (other.CompareTag("Player")) {
                Debug.Log("Player touched Plane Of Justice");
                onPlayerEnter.Invoke();
            }
        }
    }
}
