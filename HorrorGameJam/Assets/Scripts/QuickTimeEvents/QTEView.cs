using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class QTEView : MonoBehaviour
{
    public PlayableDirector AnimationTimeline => _animationTimeline;
    public Image KeyImage
    {
        get => _keyImage;
        set => _keyImage = value;
    }

    [SerializeField] private PlayableDirector _animationTimeline;
    [SerializeField] private Image _keyImage;

}