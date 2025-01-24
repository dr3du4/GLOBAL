using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSteering : MonoBehaviour
{
    SteeringScheme _currentSteeringScheme;
    PlayerInput _input;
    void Start() {
        _input = GetComponent<PlayerInput>();
        RegisterPlayerInput();
    }
    void Update() {
        CheckIfPlayerMoved();
    }
    
    public void SwitchSteeringScheme(SteeringScheme newSteeringScheme) {
        _currentSteeringScheme = newSteeringScheme;
    }
    private void RegisterPlayerInput() {
        _input.actions["Jump"].performed += OnJump;
        _input.actions["Interact"].performed += OnInteract;
    }

    private void CheckIfPlayerMoved() {
        if (_input.actions["Move"].IsPressed()) {
            _currentSteeringScheme.Move(_input.actions["Move"].ReadValue<Vector2>());
        }
    }

    private void OnJump(InputAction.CallbackContext obj) {
        _currentSteeringScheme.Jump();
    } 

    private void OnInteract(InputAction.CallbackContext obj) {
        _currentSteeringScheme.Interact();
    }
    

}
