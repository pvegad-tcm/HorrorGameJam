using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace PostProcessingAnimation
{
    public class ChangeColorOfWorld : MonoBehaviour
    {
        [SerializeField] private float _secondsChangeFinalColor;
        [SerializeField] private float _secondsRestartInitialColor;
        [SerializeField] private float _secondsWaitingInNewColor;
        
        [SerializeField] private Color32 _startColor;
        [SerializeField] private Color32 _finalColor;
        [SerializeField] private Volume _postProcessingSettings;
        private ColorAdjustments dofComponent;
        private float _timeSinceStart = 0;

        private void Start()
        {
            ColorAdjustments tmp;

            _postProcessingSettings.profile.TryGet<ColorAdjustments>(out tmp);
            dofComponent = tmp;

            StartCoroutine(ChangeColor(_finalColor, _startColor));
        }

        private void RevertColorChange()
        {
            StartCoroutine(ChangeColor(_startColor, _finalColor));
        }

        IEnumerator ChangeColor(Color finalColor, Color initialColor)
        {
            while (dofComponent.colorFilter.value != finalColor)
            {
                _timeSinceStart += Time.deltaTime / _secondsChangeFinalColor;
                dofComponent.colorFilter.value = Color.Lerp(initialColor, finalColor, _timeSinceStart);
                yield return null;
            }

            _timeSinceStart = 0;
            Invoke("RevertColorChange", _secondsWaitingInNewColor);
        }
    }
}