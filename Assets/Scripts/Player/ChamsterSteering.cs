using UnityEngine;

namespace Player {
    public class ChamsterSteering : MonoBehaviour, SteeringScheme
    {
        private Rigidbody _rigidbody;
        private Camera _camera;
    
        public float forwardForce = 5f;
        public float sideForce = 5f;
        public float jumpForce = 5f;
        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        public void Move(Vector2 direction) {
            // If player is moving on the floor tagged by "NotForChamster" tag, chamster will not move
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
            foreach (var hit in hitColliders) {
                if (hit.CompareTag("NotForChamster")) {
                    return;
                }
            }
            
            if(direction.y != 0) {
                MoveForward(direction.y);
            }
            if(direction.x != 0) {
                MoveAside(direction.x);
            }
        }

        private void MoveAside(float sideDirection) {
            Vector3 cameraRight = _camera.transform.right;
            cameraRight.y = 0;
            _rigidbody.AddForce(cameraRight * (sideForce * sideDirection));
            // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, sideDirection * sideForce, 0));
        }

        private void MoveForward(float forward) {
            Vector3 cameraForward = _camera.transform.forward;
            cameraForward.y = 0;
            _rigidbody.AddForce(cameraForward * (forwardForce * forward));
        }

        public bool Interact() {
            // Find the bubble component in 20 sphere
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
            Bubble bubble = null;
            foreach (var hit in hitColliders) {
                if (hit.CompareTag("Bubble")) {
                    bubble = hit.GetComponent<Bubble>();
                }
            }
            if(bubble != null) {
                return bubble.interactRange >= Vector3.Distance(bubble.transform.position, this.transform.position);
            }
            return false;
        }

        public void Jump() {
            throw new System.NotImplementedException();
        }
    }
}
