using System;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Checkpoints {
    public class CheckpointManager : MonoBehaviour {
        private Vector3 _positionToSpawn;
        private Quaternion _rotationToSpawn;
        public UnityEvent endLevel;
        private PlayerManager _playerManager;
        public AudioSource audioSource; // Referencja do AudioSource
        public AudioClip checkSound; // Dźwięk strzału
        private void Start() {
            _playerManager = FindFirstObjectByType<PlayerManager>(FindObjectsInactive.Include);
        }

        public void CheckpointReached(Checkpoint lastCheckpoint) {
            if (lastCheckpoint.IsFinalCheckpoint) {
                endLevel.Invoke();
            }
            _positionToSpawn = lastCheckpoint.PositionToSpawn;
            _rotationToSpawn = lastCheckpoint.RotationToSpawn;
        }

        public void SpawnPlayerOnLastCheckpoint() {
            soundPlay();
            _playerManager.SpawnPlayer(_positionToSpawn, _rotationToSpawn);
        }

        public void soundPlay()
        {
            audioSource.PlayOneShot(checkSound); 
        }
}
}
