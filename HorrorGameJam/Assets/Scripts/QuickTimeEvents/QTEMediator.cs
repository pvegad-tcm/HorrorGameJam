using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QTEMediator
{
    private QTEModel _model;
    private readonly QTEView _view;
    private readonly OnQTEStepCompleted _onStepCompleted;

    public QTEMediator(
        QuickTimeEventTemplate template,
        QTEModel model,
        QTEView view,
        OnQTEStepCompleted onStepCompleted)
    {
        _model = model;
        _view = view;
        _onStepCompleted = onStepCompleted;

        UpdateKeyImage();
        _view.KeyImage.enabled = true;
        
        CoroutineMaker.Instance.StartCoroutine(CheckInput());
    }

    private IEnumerator CheckInput()
    {
        while (true)
        {
            if (CorrectKeyReleased())
            {
                _model.TimeHolding = 0;
            }

            if (IsPressingCorrectKey())
            {
                _model.TimeHolding += Time.deltaTime;
                if (_model.IsCurrentQTEStepCompleted())
                {

                    if (!_model.IsQTECompleted())
                    {
                        UpdateQTEStep();
                    }
                    else
                    {
                        _view.KeyImage.enabled = false;
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    private bool IsPressingCorrectKey()
    {
        return Input.GetKey(_model.QTESteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private bool CorrectKeyReleased()
    {
        return Input.GetKeyUp(_model.QTESteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private void UpdateQTEStep()
    {
        _onStepCompleted.Execute();
        UpdateKeyImage();
    }

    private void UpdateKeyImage()
    {
        var nameOfKeyCode = _model.QTESteps[_model.CurrentQTEIndex].InputKeyCode.ToString();
        _view.KeyImage.sprite = Resources.Load<Sprite>(nameOfKeyCode);
    }

    private void Dispose()
    {
        //TODO: call
        //TODO: add desuscription
    }
}

//Debug.Log("Showing callback anim");
/*_view.KeyImage.enabled = false;

_view.AnimationTimeline.playableAsset = _model.CurrentQTEStep.CallbackAnimation;
_view.AnimationTimeline.stopped += OnPlayableDirectorStopped;
_view.AnimationTimeline.Play();*/