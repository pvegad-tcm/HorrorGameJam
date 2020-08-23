using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QTEMediator
{
    private readonly QTEModel _model;
    private readonly QuickTimeEventTemplate _qteTemplate;
    private readonly QTEView _view;
    private readonly OnQTEStepCompleted _onStepCompleted;
    private readonly PlayerControllerMaster _playerControllerMaster;
    private readonly QTEStepCompletedChecker _checkerHoldPress;
    private readonly QTEStepCompletedChecker _checkerSinglePress;

    public QTEMediator(QTEModel model,
        QuickTimeEventTemplate qteTemplate,
        QTEView view,
        OnQTEStepCompleted onStepCompleted,
        PlayerControllerMaster playerControllerMaster,
        QTEStepCompletedChecker checkerHoldPress,
        QTEStepCompletedChecker checkerSinglePress)
    {
        _model = model;
        _qteTemplate = qteTemplate;
        _view = view;
        _onStepCompleted = onStepCompleted;
        _playerControllerMaster = playerControllerMaster;
        _checkerHoldPress = checkerHoldPress;
        _checkerSinglePress = checkerSinglePress;

        SetUpView();
        _playerControllerMaster.Disable();
        CoroutineMaker.Instance.StartCoroutine(CheckInput());
    }

    private void SetUpView()
    {
        UpdateKeyImage();
        _view.KeyImage.enabled = true;
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

        if (IsCurrentStepCompleted())
        {
            if (_model.IsQTECompleted(_qteTemplate.QuickTimeEventSteps.Length))
            {
                FinishQTEAndShowCallbackIfNeeded();
            }
            else
            {
                UpdateQTEStepAndShowCallbackIfNeeded();
            }
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

    private void FinishQTEAndShowCallbackIfNeeded()
    {
        FinishQTE();
        if (_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].CallbackAnimation != null)
        {
            StartCallbackAnimation();
        }
    }

    private void StartCallbackAnimation()
    {
        DisableQTEWhileAnimation();

        _view.AnimationTimeline.playableAsset = _qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].CallbackAnimation;
        _view.AnimationTimeline.Play();
    }

    private void DisableQTEWhileAnimation()
    {
        _model.UserCanInteract = false;
        _view.KeyImage.enabled = false;
    }

    private bool IsCurrentStepCompleted()
    {
        switch (_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].Type)
        {
            case QuickTimeEventType.SinglePress:
                return _checkerSinglePress.IsStepFinished(_model);
            case QuickTimeEventType.Hold:
                return _checkerHoldPress.IsStepFinished(_model);
            default:
                return false;}
    }

    private void OnPlayableDirectorStopped(PlayableDirector obj)
    {
        _view.AnimationTimeline.stopped -= OnPlayableDirectorStopped;
        UpdateQTEStep();
    }

    private void FinishQTE()
    {
        _view.KeyImage.enabled = false;
        _model.QTEIsActive = false;
        _playerControllerMaster.Enable();
    }

    private bool IsPressingCorrectKey()
    {
        return Input.GetKey(_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private bool CorrectKeyReleased()
    {
        return Input.GetKeyUp(_qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode);
    }

    private void UpdateQTEStep()
    {
        _onStepCompleted.Execute();
        UpdateKeyImage();
    }

    private void UpdateKeyImage()
    {
        var nameOfKeyCode = _qteTemplate.QuickTimeEventSteps[_model.CurrentQTEIndex].InputKeyCode.ToString();
        _view.KeyImage.sprite = Resources.Load<Sprite>(nameOfKeyCode);
        _view.KeyImage.enabled = true;
    }
}