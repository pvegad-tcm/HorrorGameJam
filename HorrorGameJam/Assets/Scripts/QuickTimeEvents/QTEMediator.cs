using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QTEMediator
{
    private QTEModel _model;
    private readonly QTEView _view;
    private readonly OnQTEStepCompleted _onStepCompleted;
    private readonly PlayerControllerMaster _playerControllerMaster;

    public QTEMediator(
        QTEModel model,
        QTEView view,
        OnQTEStepCompleted onStepCompleted,
        PlayerControllerMaster playerControllerMaster
    )
    {
        _model = model;
        _view = view;
        _onStepCompleted = onStepCompleted;
        _playerControllerMaster = playerControllerMaster;

        UpdateKeyImage();
        _view.KeyImage.enabled = true;
        _playerControllerMaster.Disable();

        CoroutineMaker.Instance.StartCoroutine(CheckInput());
    }

    private IEnumerator CheckInput()
    {
        while (_model.QTEIsActive)
        {
            if (!_model.UserCanInteract)
            {
                yield return null;
            }

            if (CorrectKeyReleased())
            {
                _model.TimeHolding = 0;
            }

            if (IsPressingCorrectKey())
            {
                OnCorrectKeyPressed();
            }

            yield return null;
        }
    }

    private void OnCorrectKeyPressed()
    {
        _model.TimeHolding += Time.deltaTime;

        if (_model.IsCurrentQTEStepCompleted())
        {
            if (_model.IsQTECompleted())
            {
                StopQTE();
                if (_model.QTESteps[_model.CurrentQTEIndex].CallbackAnimation != null)
                {
                    _model.UserCanInteract = false;
                    _view.AnimationTimeline.playableAsset = _model.QTESteps[_model.CurrentQTEIndex].CallbackAnimation;
                    _view.AnimationTimeline.Play();
                    _view.KeyImage.enabled = false;
                }
            }
            else
            {
                if (_model.QTESteps[_model.CurrentQTEIndex].CallbackAnimation != null)
                {
                    _model.UserCanInteract = false;
                    _view.AnimationTimeline.playableAsset = _model.QTESteps[_model.CurrentQTEIndex].CallbackAnimation;
                    _view.AnimationTimeline.stopped += OnPlayableDirectorStopped;
                    _view.AnimationTimeline.Play();
                    _view.KeyImage.enabled = false;
                }
                else
                {
                    UpdateQTEStep();
                }
            }
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector obj)
    {
        _view.AnimationTimeline.stopped -= OnPlayableDirectorStopped;
        UpdateQTEStep();
    }

    private void StopQTE()
    {
        _view.KeyImage.enabled = false;
        _model.QTEIsActive = false;
        _playerControllerMaster.Enable();
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
        _view.KeyImage.enabled = true;
    }
}