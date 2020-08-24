using System;
using System.Collections;
using UnityEngine;

namespace Ladder
{
    public class LadderMediator
    {
        private readonly LadderView _ladderView;
        private readonly LadderModel _ladderModel;
        private readonly PlayerControllerMaster _playerControllerMaster;
        private readonly Transform _playerTransform;
        private readonly Transform _topOfLadder;
        private readonly Transform _bottomOfLadder;
        private readonly Transform _cameraHolderTransform;
        private readonly Transform _startingPointTop;
        private readonly Transform _startingPointBottom;
        private readonly Transform _cameraPivotTransform;
        private Action _onFinishedClimbing;


        public LadderMediator(
            LadderModel ladderModel,
            LadderView ladderView,
            PlayerControllerMaster playerControllerMaster,
            Transform playerTransform,
            Transform topOfLadder,
            Transform bottomOfLadder,
            Transform cameraHolderTransform,
            Transform startingPointTop,
            Transform startingPointBottom,
            Transform cameraPivotTransform)
        {
            _ladderModel = ladderModel;
            _ladderView = ladderView;
            _playerControllerMaster = playerControllerMaster;
            _playerTransform = playerTransform;
            _topOfLadder = topOfLadder;
            _bottomOfLadder = bottomOfLadder;
            _cameraHolderTransform = cameraHolderTransform;
            _startingPointTop = startingPointTop;
            _startingPointBottom = startingPointBottom;
            _cameraPivotTransform = cameraPivotTransform;
        }

        public void Climb(Action onFinishedClimbing)
        {
            _onFinishedClimbing = onFinishedClimbing;
            _playerControllerMaster.Disable();

            var distanceToTopOfLadder = _topOfLadder.position - _playerTransform.position;
            var distanceToBottomOfLadder = _bottomOfLadder.position - _playerTransform.position;

            if (distanceToTopOfLadder.sqrMagnitude > distanceToBottomOfLadder.sqrMagnitude)
            {
                _ladderModel.Target = _topOfLadder.position;
                _ladderModel.Origin = _startingPointBottom.position;
            }
            else
            {
                _ladderModel.Target = _bottomOfLadder.position;
                _ladderModel.Origin = _startingPointTop.position;
            }

            CoroutineMaker.Instance.StartCoroutine(StartMovement());
        }

        private IEnumerator StartMovement()
        {
            
            var distanceToTopOfLadder = Vector3.Distance(_topOfLadder.position, _playerTransform.position);
            var distanceToBottomOfLadder = Vector3.Distance(_bottomOfLadder.position, _playerTransform.position);
            _playerTransform.rotation = _topOfLadder.rotation;//Quaternion.identity;
            _cameraHolderTransform.rotation = Quaternion.identity;
            _cameraPivotTransform.rotation = Quaternion.identity;
            
            _playerTransform.position = _ladderModel.Origin;
            while (distanceToTopOfLadder > 0.5f && distanceToBottomOfLadder > 0.5f)
            {
                distanceToTopOfLadder = Vector3.Distance(_topOfLadder.position, _playerTransform.position);
                distanceToBottomOfLadder = Vector3.Distance(_bottomOfLadder.position, _playerTransform.position);
                
                if (Input.GetKey(KeyCode.W))
                {
                    _playerTransform.position =
                        Vector3.MoveTowards(_playerTransform.position, _topOfLadder.position, 2 * Time.deltaTime);                }
                else if (Input.GetKey(KeyCode.S))
                {
                    _playerTransform.position =
                        Vector3.MoveTowards(_playerTransform.position, _bottomOfLadder.position, 2 * Time.deltaTime);
                }

                yield return null;
            }

            _playerControllerMaster.Enable();
            _onFinishedClimbing.Invoke();
        }
    }
}