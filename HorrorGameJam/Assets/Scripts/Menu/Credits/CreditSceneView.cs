using UnityEngine;
using UnityEngine.Playables;

namespace Menu
{
    public class CreditSceneView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _creditsAnimationTimeline;

        public PlayableDirector CreditsAnimationTimeline => _creditsAnimationTimeline;
    }
}