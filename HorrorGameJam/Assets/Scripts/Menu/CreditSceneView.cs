using UnityEngine;
using UnityEngine.Playables;

public class CreditSceneView : MonoBehaviour
{
    [SerializeField] private PlayableDirector _creditsAnimationTimeline;

    public PlayableDirector CreditsAnimationTimeline => _creditsAnimationTimeline;
}