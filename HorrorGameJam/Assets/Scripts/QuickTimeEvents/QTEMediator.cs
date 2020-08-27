using System;
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
    private readonly Action _onFinished;

    public QTEMediator(QTEModel model,
        QuickTimeEventTemplate qteTemplate,
        QTEView view,
        PlayerControllerMaster playerControllerMaster,
        QTEStepCompletedChecker stepCompletedChecker, 
        Action onFinished
    )
    {
        _model = model;
        _qteTemplate = qteTemplate;
        _view = view;
        _playerControllerMaster = playerControllerMaster;
        _stepCompletedChecker = stepCompletedChecker;
        _onFinished = onFinished;

        _playerControllerMaster.Disable();
        
        if (_qteTemplate.PreConditionAnimation != null)
        {
            StartPreConditionAnimation();
        }
        else
        {
            StartQTE();
        }
    }

    private void StartPreConditionAnimation()
    {
        _view.AnimationTimeline.stopped += StartQTEAfterAnimation;
        _view.AnimationTimeline.playableAsset = _qteTemplate.PreConditionAnimation;
        _view.AnimationTimeline.Play();
    }

    private void StartQTEAfterAnimation(PlayableDirector obj)
    {
        _view.AnimationTimeline.stopped -= StartQTEAfterAnimation;
        StartQTE();
    }

    private void StartQTE()
    {
        SetUpView();
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
                if (_model.IsQTECompleted(_qteTemplate.QuickTimeEventSteps.Length))
                {
                    FinishQTE();
                    yield break;
                }
                
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
        if (IsLastAnimation())
        {
            FinishQTE();
        }
        else
        {
            _model.UserCanInteract = false;
            _view.KeyImage.enabled = false;
            _view.ActiveBackgroundAnimation(false);
        }
    }

    private bool IsLastAnimation()
    {
        return _model.CurrentQTEIndex == _qteTemplate.QuickTimeEventSteps.Length - 1;
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
        _onFinished.Invoke();
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
        if (_model.CurrentQTEIndex < _qteTemplate.QuickTimeEventSteps.Length)
        {
            _view.ActiveBackgroundAnimation(true);

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