using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using VHS;

namespace Interactions
{
    public class InteractableItemWithAnimation : InteractableBase
    {
        [SerializeField] private PlayableDirector _animationTimeline;

        public override void OnInteract()
        {
            gameObject.layer = 0;
            _animationTimeline.Play();
        }
    }
}