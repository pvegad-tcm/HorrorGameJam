using System;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class InteractableBase : MonoBehaviour
    {
        [SerializeField] private string tooltipMessage = "Interact";
        [SerializeField] private List<MeshRenderer> meshesToHighlight;
        public string TooltipMessage => tooltipMessage;

        public virtual void OnInteract()
        {
            Debug.LogWarning("INTERACTED: " + gameObject.name + " without any action.");
        }

        protected virtual void OnFinishedInteraction()
        {
        }

        public void SetLayer(int layerNum)
        {
            gameObject.layer = layerNum;
            meshesToHighlight.ForEach(mesh => mesh.gameObject.layer = layerNum);
        }
    }
}
