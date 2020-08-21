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
            _qteInstaller.Install(_template);
            Debug.Log("Installation completed."); //TODO: delete
        }
    }
}