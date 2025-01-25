using UnityEngine;

namespace Player {
    public class ChamsterSteering : MonoBehaviour, SteeringScheme {
        private Rigidbody _rigidbody;
        private Bazooka _bazooka;
        private Flamethrower _flamethrower;
        private Animator _animator;
        
        private Camera _camera;

        public float moveForce = 5f;
        public float jumpForce = 5f;
        public float slopeForce = 10f;
        
        

        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            _bazooka = transform.parent.GetComponentInChildren<Bazooka>();
            _flamethrower = transform.parent.GetComponentInChildren<Flamethrower>();
            _animator = transform.GetComponentInChildren<Animator>();
            _camera = Camera.main;
        }


        private void Update() {
            // If player can not move it should slide down the slope
            if (!CanMove()) {
                _rigidbody.AddForce(Vector3.down * slopeForce, ForceMode.Acceleration);
            }
            UpdateAnimatorVars();
        }
        
        private void UpdateAnimatorVars() {
            _animator.SetFloat("Speed", _rigidbody.linearVelocity.magnitude);
        }

        public void Move(Vector2 direction) {
            // If player is moving on the floor tagged by "NotForChamster" tag, chamster will not move
            if (!CanMove()) {
                return;
            }
            if(direction.y != 0 || direction.x != 0) {
                Vector3 cameraRight = _camera.transform.right;
                cameraRight.y = 0;
                var sideFactor = cameraRight * direction.x;
            
                Vector3 cameraForward = _camera.transform.forward;
                cameraForward.y = 0;
                var forwardFactor = cameraForward * direction.y;
                _rigidbody.AddForce((sideFactor + forwardFactor) * moveForce);
                _animator.transform.LookAt((transform.position + sideFactor + forwardFactor));
                _animator.transform.rotation = Quaternion.Euler(0, _animator.transform.rotation.eulerAngles.y - 180, 0);
            }
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

            if (bubble != null) {
                return bubble.interactRange >= Vector3.Distance(bubble.transform.position, this.transform.position);
            }

            return false;
        }

        public void Jump() {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void Attack1(Vector3 mousePosition) {
            _bazooka.Shoot(_camera.ScreenToWorldPoint(mousePosition));
        }

        public void StartAttack2(Vector3 mousePosition) {
            _flamethrower.StartFiring();
        }

        public void EndAttack2(Vector3 mousePosition) {
            _flamethrower.StopFiring();
        }

        private bool CanMove() {
            // Shoot raycast to the ground to check if chamster is on the floor tagged by "NotForChamster" tag
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f)) {
                return !hit.collider.CompareTag("NotForChamster");
            }
            return true;
        }
    }
}
