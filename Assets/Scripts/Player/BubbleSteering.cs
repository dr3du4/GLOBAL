using System;
using Player;
using UnityEngine;

public class BubbleSteering : MonoBehaviour, SteeringScheme
{
    private Rigidbody _rigidbody;
    private Camera _camera;
    private Animator _animator;
    
    public float moveForce = 5f;
    public float jumpForce = 5f;
    public float slopeForce = 10f;
    [SerializeField] private float touchingFloorLength = 3f;
    [SerializeField] private AudioSource _leavingTheBubble;
    [SerializeField] private AudioSource _chamsterWalking;
    [SerializeField] private AudioSource _jumpSound;
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _animator = transform.parent.GetComponentInChildren<Animator>();
    }

    private void Update() {
        // If player can not move it should slide down the slope
        UpdateAnimatorVars();
        if (!CanMove()) {
            _rigidbody.AddForce(Vector3.down * slopeForce, ForceMode.Acceleration);
        }
    }

    public void Move(Vector2 direction) {
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
            _rigidbody.AddForce(travelDirection.normalized * moveForce);
            _animator.transform.LookAt((transform.position + travelDirection));
            _animator.transform.rotation = Quaternion.Euler(0, _animator.transform.rotation.eulerAngles.y - 180, 0);
        }
    }
    private void UpdateAnimatorVars() {
        var velocity = _rigidbody.linearVelocity;
        velocity.y = 0;
        _animator.SetFloat("Speed", velocity.magnitude);
        if (_rigidbody.linearVelocity.magnitude > 0.1f && !_chamsterWalking.isPlaying) {
            _chamsterWalking.Play();
        } else if (_rigidbody.linearVelocity.magnitude < 0.1f && _chamsterWalking.isPlaying) {
            _chamsterWalking.Stop();
        }
    }

    public bool Interact() {
        _leavingTheBubble.Play();
        return true;
    }

    public void Jump() {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, touchingFloorLength)) {
            Debug.Log("Detected not touching the floor");
            return;
        }
        Debug.Log("Performing jump");
        _jumpSound.Play();
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void StartAttack1() {
        return;
    }

    public void EndAttack1() {
        return;
    }

    public void StartAttack2(Vector3 mousePosition) {
        return;
    }

    public void EndAttack2(Vector3 mousePosition) {
        return;
    }

    public void GotHit() {
        return;
    }

    private bool CanMove() {
        // Shoot raycast to the ground to check if chamster is on the floor tagged by "NotForChamster" tag
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 4f)) {
            return !hit.collider.CompareTag("NotForBubble");
        }
        return true;
    }
    
    // Draw gizmos for raycast
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * touchingFloorLength);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet")) {
            Destroy(other);
            return;
        }
    }
}
