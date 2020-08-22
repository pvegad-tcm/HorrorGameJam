using System.Collections;
using UnityEngine;
using VHS;

namespace Interactions
{
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private QTEInstaller _qteInstaller;
        [SerializeField] private QuickTimeEventTemplate _template;

        public override void OnInteract()
        {
            gameObject.layer = 0;
            StartCoroutine(NextFrameInteraction());
        }

        private IEnumerator NextFrameInteraction()
        {
            yield return null;
            _qteInstaller.Install(_template);
            
            yield return null;
            Destroy(this);
        }
    }
}