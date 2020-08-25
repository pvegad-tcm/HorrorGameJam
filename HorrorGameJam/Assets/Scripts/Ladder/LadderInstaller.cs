using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ladder
{
    public class LadderInstaller : MonoBehaviour
    {
        [SerializeField] private float _climbingSpeed;
        [SerializeField] private PlayerControllerMaster _playerControllerMaster;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _cameraHolderTransform;
        [SerializeField] private Transform _cameraPivotTransform;
        [SerializeField] private Transform _finalDestinationTop;
        [SerializeField] private Transform _finalDestinatioBot;
        [SerializeField] private Transform _startingPointTop;
        [SerializeField] private Transform _startingPointBottom;
        [SerializeField] private Vector3 _playerOrientation;


        
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
                _playerOrientation);
        }

        public void StartClimbing(Action onFinishedClimbing)
        {
            _ladderMediator.Climb(onFinishedClimbing);
        }
    }
}