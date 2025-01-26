using UnityEngine;

namespace Player {
    public interface SteeringScheme {
        void Move(Vector2 direction);
        bool Interact();
        void Jump();
        void StartAttack1();
        void EndAttack1();
        void StartAttack2(Vector3 mousePosition);
        void EndAttack2(Vector3 mousePosition);
        void GotHit();
    }
}