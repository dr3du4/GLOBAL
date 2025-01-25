using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerSteering : MonoBehaviour
    {
        SteeringScheme _currentSteeringScheme;
        SteeringModeManager _steeringModeManager;

        public SteeringScheme CurrentSteeringScheme => _currentSteeringScheme;

        PlayerInput _input;
        void Start() {
            _input = GetComponent<PlayerInput>();
            _steeringModeManager = GetComponent<SteeringModeManager>();
            RegisterPlayerInput();
        }
        void FixedUpdate() {
            CheckIfPlayerMoved();
        }
    
        public void SwitchSteeringScheme(SteeringScheme newSteeringScheme) {
            _currentSteeringScheme = newSteeringScheme;
        }

        private void CheckIfPlayerMoved() {
            if (_input.actions["Move"].IsPressed()) {
                _currentSteeringScheme.Move(_input.actions["Move"].ReadValue<Vector2>());
            }
        }

        private void OnJump(InputAction.CallbackContext obj) {
            _currentSteeringScheme.Jump();
        } 

        public void OnInteraction(InputAction.CallbackContext obj) {
            if (_currentSteeringScheme.Interact()) {
                _steeringModeManager.SwitchSteeringMode();
            }
        }
    
        private void RegisterPlayerInput() {
            _input.actions["Jump"].performed += OnJump;
            _input.actions["Interact"].performed += OnInteraction;
        }
    }
}
