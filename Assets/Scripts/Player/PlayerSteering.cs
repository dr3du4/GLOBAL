using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerSteering : MonoBehaviour
    {
        SteeringScheme _currentSteeringScheme;
        SteeringModeManager _steeringModeManager;

        private bool _isAttack1InProgress = false;

        public SteeringScheme CurrentSteeringScheme => _currentSteeringScheme;

        PlayerInput _input;
        public void OnJumpKey(InputAction.CallbackContext obj) {
            Debug.Log("Jump key pressed!");
            _currentSteeringScheme.Jump();
        } 
        void Start() {
            _input = GetComponent<PlayerInput>();
            _steeringModeManager = GetComponent<SteeringModeManager>();
            RegisterPlayerInput();
            _isAttack1InProgress = false;
        }
        void FixedUpdate() {
            CheckIfPlayerMoved();
            CheckAttack1Conditions();
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
        public void OnInteraction(InputAction.CallbackContext obj) {
            if (_currentSteeringScheme.Interact()) {
                _steeringModeManager.SwitchSteeringMode();
            }
        }
        
        // public void OnAttack1(InputAction.CallbackContext obj) {
        //     _currentSteeringScheme.Attack1(_input.actions["Look"].ReadValue<Vector2>());
        // }
        
        public void OnAttack2() {
            // Debug.Log("Started fire!");
            _currentSteeringScheme.StartAttack2(_input.actions["Look"].ReadValue<Vector2>());
        }
        
        public void OnEndAttack2() {
            // Debug.Log("Finished fire!");
            _currentSteeringScheme.EndAttack2(_input.actions["Look"].ReadValue<Vector2>());
        }
    
        private void RegisterPlayerInput() {
            _input.actions["Jump"].performed += OnJumpKey;
            _input.actions["Interact"].performed += OnInteraction;
        }

        private void CheckAttack1Conditions() {
            if(_input.actions["Attack_1"].IsPressed()) {
                _isAttack1InProgress = true;
                _currentSteeringScheme.StartAttack1();
            }
            else {
                if (_isAttack1InProgress == true) {
                    _currentSteeringScheme.EndAttack1();
                    _isAttack1InProgress = false;
                }
            }
        }
    }
}
