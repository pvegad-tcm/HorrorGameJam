using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class QTEMediator
{
    private QTEModel _model;
    private readonly QTEView _view;

    public QTEMediator(QuickTimeEventTemplate template, QTEModel model, QTEView view)
    {
        _model = model;
        _view = view;

        _view.KeyImage.sprite = Resources.Load<Sprite>("key");
        _view.KeyImage.enabled = true;
        CoroutineMaker.Instance.StartCoroutine(CheckInput());
    }

    private IEnumerator CheckInput()
    {
        while (true)
        {
            if (_model.IsQTECompleted())
            {
                yield break;
            }
            
            if (Input.GetKeyUp(_model.qteSteps[_model.currentQTEIndex].InputKeyCode))
            {
                _model.TimeHolding = 0;
            }
            
            if (Input.GetKey(_model.qteSteps[_model.currentQTEIndex].InputKeyCode))
            {
                Debug.Log("key pressed");

                _model.TimeHolding += Time.deltaTime;
                if (_model.IsCurrentQTEStepCompleted())
                {
                    if (_model.CurrentQTEStep.CallbackAnimation != null)
                    {
                        Debug.Log("Showing callback anim");
                        /*_view.KeyImage.enabled = false;

                        _view.AnimationTimeline.playableAsset = _model.CurrentQTEStep.CallbackAnimation;
                        _view.AnimationTimeline.stopped += OnPlayableDirectorStopped;
                        _view.AnimationTimeline.Play();*/
                    }
                    else
                    {
                        Debug.Log("Callback anim is null");

                        UpdateQTEStep();
                    }
                }
                //_model.KeyPressed.Invoke();
            }
            
            yield return null;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        UpdateQTEStep();
    }

    private void UpdateQTEStep()
    {
        //TODO: move comprobacion a fuera
        if (!_model.IsQTECompleted())
        {
            Debug.Log("QTE is not completed yet");

            _view.KeyImage.sprite = Resources.Load<Sprite>("letter_b");
            _view.KeyImage.enabled = true;
            _model.UpdateCurrentQTEIndex();
        }
        else
        {
            Debug.Log("QTE is Completed");
        }
        
    }

    private void Dispose()
    {
        //TODO: call
        //TODO: add desuscription
    }
}