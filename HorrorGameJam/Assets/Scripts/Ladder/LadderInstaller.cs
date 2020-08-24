using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ladder
{
    public class LadderInstaller : MonoBehaviour
    {
        [SerializeField] private LadderView _ladderView;
        [SerializeField] private PlayerControllerMaster _playerControllerMaster;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _cameraHolderTransform;
        [SerializeField] private Transform _highestPoint;
        [SerializeField] private Transform _lowestPoint;
        [SerializeField] private Transform _startingPointTop;
        [SerializeField] private Transform _startingPointBottom;
        [SerializeField] private Transform _cameraPivotTransform;


        
        private LadderMediator _ladderMediator;
        private LadderModel _ladderModel;

        private void Awake()
        {
            _ladderModel = new LadderModel();
            _ladderMediator = new LadderMediator(
                _ladderModel, 
                _ladderView, 
                _playerControllerMaster,
                _playerTransform, 
                _highestPoint, 
                _lowestPoint,
                _cameraHolderTransform,
                _startingPointTop,
                _startingPointBottom,
                _cameraHolderTransform);
        }

        public void StartClimbing(Action onFinishedClimbing)
        {
            _ladderMediator.Climb(onFinishedClimbing);
        }
    }
}