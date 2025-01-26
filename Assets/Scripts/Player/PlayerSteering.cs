using Checkpoints;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerSteering : MonoBehaviour
    {
        SteeringScheme _currentSteeringScheme;
        SteeringModeManager _steeringModeManager;
        CheckpointManager _checkpointManager;
        public GameObject _pauseMenu;
        private int _numberOfHits = 0;
        [SerializeField] private int _maxNumberOfHits = 3;

        private bool _isAttack1InProgress = false;

        public SteeringScheme CurrentSteeringScheme => _currentSteeringScheme;

        PlayerInput _input;
        public void OnJumpKey(InputAction.CallbackContext obj) {
            Debug.Log("Jump key pressed!");
            _currentSteeringScheme.Jump();
        } 
        public void OnRestartDemand(InputAction.CallbackContext obj) {
            _checkpointManager.SpawnPlayerOnLastCheckpoint();
        }
        
        public void OnPauseDemand(InputAction.CallbackContext obj) {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }

        public void GotHit() {
            _numberOfHits++;
            if (_numberOfHits >= _maxNumberOfHits) {
                _checkpointManager.SpawnPlayerOnLastCheckpoint();
                _numberOfHits = 0;
            }
        }
        void Start() {
            _input = GetComponent<PlayerInput>();
            _steeringModeManager = GetComponent<SteeringModeManager>();
            _checkpointManager = FindFirstObjectByType<CheckpointManager>();
            RegisterPlayerInput();
            _isAttack1InProgress = false;
            // Set up CursorLockMode.Locked
            Cursor.lockState = CursorLockMode.Locked;
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
            // _input.actions["Pause"].performed += OnPauseDemand;
            _input.actions["Restart"].performed += OnRestartDemand;
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
