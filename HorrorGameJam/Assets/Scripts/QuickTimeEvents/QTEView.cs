using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class QTEView : MonoBehaviour
{
    [SerializeField] private PlayableDirector _animationTimeline;
    [SerializeField] private PlayableAsset _keyPressedAnimation;    
    [SerializeField] private Image _keyImage;
    [SerializeField] private Animation _animationAura;
    [SerializeField] private AnimationClip _backgroundAnimationFast;
    [SerializeField] private AnimationClip _backgroundAnimationSlow;

    public PlayableDirector AnimationTimeline => _animationTimeline;
    public PlayableAsset KeyPressedAnimation => _keyPressedAnimation;
    public Image KeyImage => _keyImage;

    public void PlayBackgroundFast()
    {
        _animationAura.clip = _backgroundAnimationFast;
        _animationAura.Play();
    }
    
    public void PlayBackgroundSlow()
    {
        _animationAura.clip = _backgroundAnimationSlow;
        _animationAura.Play();
    }

    public void ActiveBackgroundAnimation(bool active)
    {
        _animationAura.gameObject.SetActive(active);
    }
}