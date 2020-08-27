using UnityEngine;

namespace VHS
{
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] private string tooltipMessage = "Interact";
        public string TooltipMessage => tooltipMessage;

        public virtual void OnInteract()
        {
            Debug.LogWarning("INTERACTED: " + gameObject.name + " without any action.");
        }

        protected virtual void OnFinishedInteraction()
        {
        }
    }
}
