using Unity.Cinemachine;
using UnityEngine;

namespace Player {
    public class SteeringModeManager : MonoBehaviour {
        public Vector3 chamsterSpawnOffset;
        private PlayerSteering _playerSteering;
        private SteeringScheme _bubbleSteering;
        private SteeringScheme _chamsterSteering;
        [SerializeField] GameObject _bubble;
        [SerializeField] GameObject _chamsterBubble;
        [SerializeField] GameObject _chamsterBubbleRig;
        [SerializeField] GameObject _chamster;
    
        private void Start() {
            _playerSteering = GetComponent<PlayerSteering>();
            _chamsterSteering = GetComponentInChildren<ChamsterSteering>();
            _bubbleSteering = GetComponentInChildren<BubbleSteering>();
            SwitchSteeringToBubble(); ;
        }
    
        public void SwitchSteeringMode() {
            if(_playerSteering.CurrentSteeringScheme == _bubbleSteering) {
                SwitchSteeringToChamster();
            } else {
                SwitchSteeringToBubble();
            }
        }

        private void SwitchSteeringToBubble() {
            _chamsterBubble.SetActive(true);
            _chamsterBubbleRig.SetActive(true);
            _chamsterBubble.transform.position = _bubble.transform.position;
            _bubble.SetActive(false);
            _chamster.SetActive(false);
            _playerSteering.SwitchSteeringScheme(_bubbleSteering);
            Camera.main.GetComponent<CinemachineCamera>().Follow = _chamsterBubbleRig.transform;
        } 
    
        private void SwitchSteeringToChamster() {
            _bubble.transform.position = _chamsterBubble.transform.position;
            _chamster.transform.position = _chamsterBubble.transform.position + chamsterSpawnOffset;
            _bubble.SetActive(true);
            _chamster.SetActive(true);
            _chamsterBubble.SetActive( false);
            _chamsterBubbleRig.SetActive( false);
            _playerSteering.SwitchSteeringScheme(_chamsterSteering);
            Camera.main.GetComponent<CinemachineCamera>().Follow = _chamster.transform;
        }
    }
}
