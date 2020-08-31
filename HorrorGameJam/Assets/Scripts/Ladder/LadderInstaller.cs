using System;
using UnityEngine;

namespace Ladder
{
    public class LadderInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerControllerMaster _playerControllerMaster;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _cameraHolderTransform;
        [SerializeField] private Transform _cameraPivotTransform;
        [SerializeField] private Transform _finalDestinationTop;
        [SerializeField] private Transform _finalDestinatioBot;
        [SerializeField] private Transform _startingPointTop;
        [SerializeField] private Transform _startingPointBottom;
        [SerializeField] private Transform _ladderSound;
        [SerializeField] private Transform _startLadderSound;
        [SerializeField] private Transform _finishLadderSound;        
        [SerializeField] private Vector3 _playerOrientation;
        [SerializeField] private float _climbingSpeed;

        private LadderMediator _ladderMediator;

        private void Awake()
        {
            _ladderMediator = new LadderMediator(
                _playerControllerMaster,
                _playerTransform, 
                _cameraHolderTransform,
                _cameraPivotTransform,
                _finalDestinationTop, 
                _finalDestinatioBot,
                _startingPointTop,
                _startingPointBottom,
                _climbingSpeed,
                _playerOrientation,
                _ladderSound,
                _startLadderSound,
                _finishLadderSound);
        }

        public void StartClimbing(Action onFinishedClimbing)
        {
            _ladderMediator.Climb(onFinishedClimbing);
        }
    }
}