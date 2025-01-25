using UnityEngine;

namespace Player {
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _bubble;
        [SerializeField] private GameObject _chamsterBubble;
        [SerializeField] private GameObject _chamster;
        
        public void SpawnPlayer(Vector3 position, Quaternion rotation) {
            SpawnObjectAtPosition(_bubble, position, rotation);
            SpawnObjectAtPosition(_chamsterBubble, position, rotation);
            SpawnObjectAtPosition(_chamster, position, rotation);
        }

        private void SpawnObjectAtPosition(GameObject obj, Vector3 position, Quaternion rotation) {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
    }
}
