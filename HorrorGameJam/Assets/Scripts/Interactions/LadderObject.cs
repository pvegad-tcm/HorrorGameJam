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
            gameObject.layer = (int)LayerValue.Default;
            _ladderInstaller.StartClimbing(OnFinishedClimbing);
        }

        private void OnFinishedClimbing()
        {
            gameObject.layer = (int)LayerValue.Selected;
        }
    }
}