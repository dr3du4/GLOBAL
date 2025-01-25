using System;
using Player;
using UnityEngine;

public class BubbleSteering : MonoBehaviour, SteeringScheme
{
    private Rigidbody _rigidbody;
    private Camera _camera;
    
    public float forwardForce = 5f;
    public float sideForce = 5f;
    public float jumpForce = 5f;
    public float slopeForce = 10f;
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update() {
        // If player can not move it should slide down the slope
        if (!CanMove()) {
            _rigidbody.AddForce(Vector3.down * slopeForce);
        }
    }

    public void Move(Vector2 direction) {
        if (!CanMove()) {
            return;
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
        return true;
    }

    public void Jump() {
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool CanMove() {
        // Return false if player walks over the floor tagged by "NotForBubble" tag. It should maximaly accurate
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1);
        foreach (var hit in hitColliders) {
            if (hit.CompareTag("NotForBubble")) {
                return false;
            }
        }
        return true;
    }
}
