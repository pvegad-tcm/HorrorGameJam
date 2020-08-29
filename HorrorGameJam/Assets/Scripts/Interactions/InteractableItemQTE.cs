using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Interactions
{
    public class InteractableItemQTE : InteractableItem
    {
        [BoxGroup("Quick Time Events")] [SerializeField] 
        private QTEInstaller _qteInstaller;
        
        [BoxGroup("Quick Time Events")] [SerializeField] 
        private QuickTimeEventTemplate _template;
        
        public override void OnInteract()
        {
            base.OnInteract();
            StartCoroutine(NextFrameInteraction());
        }

        private IEnumerator NextFrameInteraction()
        {
            yield return null;
            _qteInstaller.Install(_template, OnFinishedInteractionDelayed);

        }
    }
}