using System;
using System.Collections;
using Ladder;
using UnityEngine;
using VHS;

namespace Interactions
{
    public class LadderObject : InteractableBase
    {
        [SerializeField] private LadderInstaller _ladderInstaller;
        private Action _onFinishedClimbing;
        
        public override void OnInteract()
        {
            gameObject.layer = 0;
            _ladderInstaller.StartClimbing(OnFinishedClimbing);
        }

        private void OnFinishedClimbing()
        {
            gameObject.layer = 10;
        }
    }
}