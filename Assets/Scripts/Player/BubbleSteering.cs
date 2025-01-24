using Player;
using UnityEngine;

public class BubbleSteering : MonoBehaviour, SteeringScheme
{
    private Rigidbody _rigidbody;
    private PlayerSteering _playerSteering;
    public float forwardForce = 5f;
    public float sideForce = 5f;
    public float jumpForce = 5f;
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _playerSteering = transform.parent.GetComponent<PlayerSteering>();
        _playerSteering.SwitchSteeringScheme(this);
    }
    public void Move(Vector2 direction) {
        if(direction.y != 0) {
            MoveForward(direction.y);
        }
        if(direction.x != 0) {
            MoveAside(direction.x);
        }
    }

    private void MoveAside(float sideDirection) {
        _rigidbody.AddForce(Vector3.right * (sideForce * sideDirection));
        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, sideDirection * sideForce, 0));
    }

    private void MoveForward(float forward) {
        _rigidbody.AddForce(Vector3.forward * (forwardForce * forward));
    }

    public bool Interact() {
        throw new System.NotImplementedException();
    }

    public void Jump() {
        _rigidbody.AddForce(transform.up);
    }
}
