using System;
using UnityEngine;

public class QTEInstaller : MonoBehaviour
 {
      [SerializeField] private QTEView _view;
      [SerializeField] private PlayerControllerMaster _playerControllerMaster;
      public void Install(QuickTimeEventTemplate template, Action onFinished)
      {
          var stepCompletedChecker = new QTEStepCompletedChecker(template);
          var model = new QTEModel();
          
          var mediator = new QTEMediator(
              model, 
              template,
              _view, 
              _playerControllerMaster, 
              stepCompletedChecker,
              onFinished
          );
      }
 }