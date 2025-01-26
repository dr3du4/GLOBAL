using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Checkpoints {
    public class Checkpoint : MonoBehaviour {
        [SerializeField] private Vector3 positionToSpawn;
        public Vector3 PositionToSpawn => positionToSpawn;
        [SerializeField] private Quaternion rotationToSpawn;
        [SerializeField] private bool isFinalCheckpoint;
        public bool IsFinalCheckpoint => isFinalCheckpoint;
        public Quaternion RotationToSpawn => rotationToSpawn;
        private CheckpointManager _checkpointManager;
       
        
        private void Start() {
            _checkpointManager = transform.parent.GetComponent<CheckpointManager>();
            if(positionToSpawn == Vector3.zero) {
                positionToSpawn = transform.position;
            }
            if(rotationToSpawn == Quaternion.identity) {
                rotationToSpawn = transform.rotation;
            }
        }
        
        private void DeleteCheckpoint()
        {
           Destroy(gameObject); // Zniszcz obiekt
           _checkpointManager.soundPlay();
        }
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) {
                return;
            }
            
            _checkpointManager.CheckpointReached(this);
            DeleteCheckpoint();
        }
    }
}
