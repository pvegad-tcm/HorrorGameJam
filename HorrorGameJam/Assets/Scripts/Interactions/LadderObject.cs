using System.Collections;
using Ladder;
using UnityEngine;
using VHS;

namespace Interactions
{
    public class LadderObject : InteractableBase
    {
        [SerializeField] private LadderInstaller _ladderInstaller;
        
        public override void OnInteract()
        {
            SetLayer((int)LayerValue.Default);
            StartCoroutine(NextFrameInteraction());
        }
        
        private IEnumerator NextFrameInteraction()
        {
            yield return null;
            _ladderInstaller.StartClimbing(OnFinishedClimbing);
        }
        
        private void OnFinishedClimbing()
        {
            SetLayer((int)LayerValue.Interactable);
        }
    }
}