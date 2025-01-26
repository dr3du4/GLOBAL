using UnityEngine;

namespace Player {
    public class ChamsterSteering : MonoBehaviour, SteeringScheme {
        private Rigidbody _rigidbody;
        private Bazooka _bazooka;
        private Flamethrower _flamethrower;
        private Animator _animator;
        private Crosshair _crosshair;
        [SerializeField] private AudioSource _joiningTheBubble;
        [SerializeField] private AudioSource _chamsterWalking;
        
        private Camera _camera;

        public float moveForce = 5f;
        public float slopeForce = 10f;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            _crosshair = FindFirstObjectByType<Crosshair>();
            _bazooka = transform.parent.GetComponentInChildren<Bazooka>();
            _flamethrower = transform.parent.GetComponentInChildren<Flamethrower>();
            _animator = transform.GetComponentInChildren<Animator>();
            _camera = Camera.main;
            _crosshair.Hide();
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
            if (_rigidbody.linearVelocity.magnitude > 0.1f && !_chamsterWalking.isPlaying) {
                _chamsterWalking.Play();
            } else if (_rigidbody.linearVelocity.magnitude < 0.1f && _chamsterWalking.isPlaying) {
                _chamsterWalking.Stop();
            }
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
                var travelDirection = sideFactor + forwardFactor;
                travelDirection.y = 0;
                Debug.Log(travelDirection.normalized * moveForce);
                _rigidbody.AddForce(travelDirection.normalized * moveForce);
                _animator.transform.LookAt((transform.position + travelDirection));
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
                if (bubble.interactRange >= Vector3.Distance(bubble.transform.position, this.transform.position)) {
                    _joiningTheBubble.Play();
                    return true;
                }
            }
            return false;
        }

        public void Jump() {
            return;
        }

        public void StartAttack1() {
            _crosshair.Show();
            _animator.transform.LookAt(_camera.ScreenToWorldPoint( new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, _camera.nearClipPlane + 20))); //_camera.nearClipPlane + 10)));
            _animator.transform.rotation = Quaternion.Euler(0, _animator.transform.rotation.eulerAngles.y - 180, 0);
        }

        public void EndAttack1() {
            // Pass middle of the screen to screen to world point
            _crosshair.Hide();
            _bazooka.Shoot(_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, _camera.nearClipPlane + 20))); //_camera.nearClipPlane + 10)));
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
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 4f)) {
                return !hit.collider.CompareTag("NotForChamster");
            }
            return true;
        }
    }
}
