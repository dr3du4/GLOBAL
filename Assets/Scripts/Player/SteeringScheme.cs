using UnityEngine;

namespace Player {
    public interface SteeringScheme {
        void Move(Vector2 direction);
        void Interact();
        void Jump();
    }
}