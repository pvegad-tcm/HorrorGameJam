using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using VHS;

namespace Interactions
{
    public class InteractableItemWithAnimation : InteractableItem
    {
        [SerializeField] private PlayableDirector _animationTimeline;

        public override void OnInteract()
        {
            base.OnInteract();
            _animationTimeline.Play();
            OnFinishedInteraction();
        }
    }
}