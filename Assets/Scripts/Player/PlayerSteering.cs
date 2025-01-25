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
            CheckIfPlayerAttack2();
        }

        private void CheckIfPlayerAttack2() {
            if (_input.actions["Attack_2"].IsPressed()) {
                OnAttack2();
            }
            else {
                OnEndAttack2();
            }
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
        
        public void OnAttack1(InputAction.CallbackContext obj) {
            _currentSteeringScheme.Attack1(_input.actions["Look"].ReadValue<Vector2>());
        }
        
        public void OnAttack2() {
            // Debug.Log("Started fire!");
            _currentSteeringScheme.StartAttack2(_input.actions["Look"].ReadValue<Vector2>());
        }
        
        public void OnEndAttack2() {
            // Debug.Log("Finished fire!");
            _currentSteeringScheme.EndAttack2(_input.actions["Look"].ReadValue<Vector2>());
        }
    
        private void RegisterPlayerInput() {
            _input.actions["Jump"].performed += OnJump;
            _input.actions["Interact"].performed += OnInteraction;
            _input.actions["Attack_1"].performed += OnAttack1;
        }
    }
}
