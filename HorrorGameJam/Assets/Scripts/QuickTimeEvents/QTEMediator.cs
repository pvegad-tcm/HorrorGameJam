using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QTEMediator
{
    private readonly QTEModel _model;
    private readonly QuickTimeEventTemplate _qteTemplate;
    private readonly QTEView _view;
    private readonly PlayerControllerMaster _playerControllerMaster;
    private readonly QTEStepCompletedChecker _stepCompletedChecker;

    public QTEMediator(QTEModel model,
        QuickTimeEventTemplate qteTemplate,
        QTEView view,
        PlayerControllerMaster playerControllerMaster,
        QTEStepCompletedChecker stepCompletedChecker)
    {
        _model = model;
        _qteTemplate = qteTemplate;
        _view = view;
        _playerControllerMaster = playerControllerMaster;
        _stepCompletedChecker = stepCompletedChecker;

        SetUpView();
        _playerControllerMaster.Disable();
        CoroutineMaker.Instance.StartCoroutine(CheckInput());
    }

    private void SetUpView()
    {
        UpdateKeyImage();

        DisplayAura();
        _view.KeyImage.enabled = true;
    }

    private IEnumerator CheckInput()
    {
        while (_model.QTEIsActive)
        {
            if (_model.UserCanInteract)
            {
                if (CorrectKeyReleased())
                {
                    _model.TimeHolding = 0;
                }

                if (CorrectKeyStartedPressing())
                {
                    _model.PressedTimes++;
                    _view.AnimationTimeline.Play(_view.KeyPressedAnimation);
                }

                if (CorrectKeyHolding())
                {
                    _model.TimeHolding += Time.deltaTime;
                }

                if (IsCurrentStepCompleted())
                {
                    UpdateQTEStepAndShowCallbackIfNeeded();
                }

                if (_model.IsQTECompleted(_qteTemplate.QuickTimeEventSteps.Length))
                {
                    FinishQTE();
                }
            }

            yield return null;
        }
    }

    private void UpdateQTEStepAndShowCallbackIfNeeded()
    {
        if (_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].CallbackAnimation != null)
        {
            _view.AnimationTimeline.stopped += OnPlayableDirectorStopped;
            StartCallbackAnimation();
        }
        else
        {
            UpdateQTEStep();
        }
    }

    private void StartCallbackAnimation()
    {
        DisableQTEWhileAnimation();

        _view.AnimationTimeline.playableAsset =
            _qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].CallbackAnimation;
        _view.AnimationTimeline.Play();
    }

    private void DisableQTEWhileAnimation()
    {
        _model.UserCanInteract = false;
        _view.KeyImage.enabled = false;
        _view.ActiveBackgroundAnimation(false);
    }

    private bool IsCurrentStepCompleted()
    {
        return _stepCompletedChecker.IsStepFinished(_model);
    }

    private void OnPlayableDirectorStopped(PlayableDirector obj)
    {
        _model.UserCanInteract = true;
        _view.AnimationTimeline.stopped -= OnPlayableDirectorStopped;
        UpdateQTEStep();
    }

    private void FinishQTE()
    {
        _view.KeyImage.enabled = false;
        _view.ActiveBackgroundAnimation(false);
        _model.QTEIsActive = false;
        _playerControllerMaster.Enable();
    }

    private bool CorrectKeyHolding()
    {
        var currentKeyCode = _qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode;
        if (_model.TimeHolding <= 0)
        {
            return Input.GetKeyDown(currentKeyCode);
        }

        return Input.GetKey(currentKeyCode);
    }

    private bool CorrectKeyReleased()
    {
        return Input.GetKeyUp(_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private bool CorrectKeyStartedPressing()
    {
        return Input.GetKeyDown(_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private void UpdateQTEStep()
    {
        _model.PressedTimes = 0;
        _model.CurrentQTEIndex++;
        UpdateKeyImage();
        DisplayAura();
    }

    private void UpdateKeyImage()
    {
        if (_model.CurrentQTEIndex < _qteTemplate.QuickTimeEventSteps.Length)
        {
            var nameOfKeyCode = _qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode.ToString();
            _view.KeyImage.sprite = Resources.Load<Sprite>(nameOfKeyCode);
            _view.KeyImage.enabled = true;
        }
    }

    private void DisplayAura()
    {
        _view.ActiveBackgroundAnimation(true);

        if (_model.CurrentQTEIndex < _qteTemplate.QuickTimeEventSteps.Length)
        {
            if (_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].NumberOfInputsNeeded > 1)
            {
                _view.PlayBackgroundFast();
            }
            else
            {
                _view.PlayBackgroundSlow();
            }
        }
    }
}