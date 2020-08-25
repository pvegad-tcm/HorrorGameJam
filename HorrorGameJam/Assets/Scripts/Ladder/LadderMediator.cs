using System;
using System.Collections;
using UnityEngine;

namespace Ladder
{
    public class LadderMediator
    {
        private readonly PlayerControllerMaster _playerControllerMaster;
        private readonly Transform _playerTransform;
        private readonly Transform _finalDestinationTop;
        private readonly Transform _finalDestinationBot;
        private readonly Transform _cameraHolderTransform;
        private readonly Transform _startingPointTop;
        private readonly Transform _startingPointBot;
        private readonly Transform _cameraPivotTransform;
        private readonly Vector3 _playerOrientation;
        private readonly float _climbingSpeed;
        private Action _onFinishedClimbing;


        public LadderMediator(PlayerControllerMaster playerControllerMaster,
            Transform playerTransform,
            Transform cameraHolderTransform,
            Transform cameraPivotTransform,
            Transform finalDestinationTop,
            Transform finalDestinationBot,
            Transform startingPointTop,
            Transform startingPointBot, 
            float climbingSpeed,
            Vector3 playerOrientation)
        {
            _playerControllerMaster = playerControllerMaster;
            _playerTransform = playerTransform;
            _finalDestinationTop = finalDestinationTop;
            _finalDestinationBot = finalDestinationBot;
            _cameraHolderTransform = cameraHolderTransform;
            _startingPointTop = startingPointTop;
            _startingPointBot = startingPointBot;
            _climbingSpeed = climbingSpeed;
            _cameraPivotTransform = cameraPivotTransform;
            _playerOrientation = playerOrientation;
        }

        public void Climb(Action onFinishedClimbing)
        {
            _onFinishedClimbing = onFinishedClimbing;
            _playerControllerMaster.Disable();

            DecideStartingPoint();
            CoroutineMaker.Instance.StartCoroutine(StartMovement());
        }

        private void DecideStartingPoint()
        {
            var distanceToTopOfLadder = Vector3.Distance(_finalDestinationTop.position, _playerTransform.position);
            var distanceToBottomOfLadder = Vector3.Distance(_finalDestinationBot.position, _playerTransform.position);

            if (distanceToTopOfLadder > distanceToBottomOfLadder)
            {
                _playerTransform.position = _startingPointBot.position;
            }
            else
            {
                _playerTransform.position = _startingPointTop.position;
            }
        }

        private IEnumerator StartMovement()
        {
            var distanceToTopOfLadder = Vector3.Distance(_finalDestinationTop.position, _playerTransform.position);
            var distanceToBottomOfLadder = Vector3.Distance(_finalDestinationBot.position, _playerTransform.position);
            SetCameraRotation();

            while (distanceToTopOfLadder > 0.5f && distanceToBottomOfLadder > 0.5f)
            {
                distanceToTopOfLadder = Vector3.Distance(_finalDestinationTop.position, _playerTransform.position);
                distanceToBottomOfLadder = Vector3.Distance(_finalDestinationBot.position, _playerTransform.position);
                
                if (Input.GetKey(KeyCode.W))
                {
                    Ascend();
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Descend();
                }
                yield return null;
            }

            _playerControllerMaster.Enable();
            _onFinishedClimbing.Invoke();
        }

        private void Descend()
        {
            _playerTransform.position =
                Vector3.MoveTowards(_playerTransform.position, _finalDestinationBot.position, _climbingSpeed * Time.deltaTime);
        }

        private void Ascend()
        {
            _playerTransform.position =
                Vector3.MoveTowards(_playerTransform.position, _finalDestinationTop.position, _climbingSpeed * Time.deltaTime);
        }

        private void SetCameraRotation()
        {
            _playerTransform.localRotation = Quaternion.Euler(_playerOrientation);
            _cameraHolderTransform.localRotation = Quaternion.identity;
            _cameraPivotTransform.localRotation = Quaternion.identity;
        }
    }
}