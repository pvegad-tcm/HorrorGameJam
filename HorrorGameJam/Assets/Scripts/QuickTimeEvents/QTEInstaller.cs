using UnityEngine;

public class QTEInstaller : MonoBehaviour
 {
      [SerializeField] private QTEView _view;
      [SerializeField] private PlayerControllerMaster _playerControllerMaster;
      public void Install(QuickTimeEventTemplate template)
      {
          var singlePressChecker = new QTESinglePressChecker();
          var holdPressChecker = new QTEHoldPressChecker(template);
          var model = new QTEModel();
          
          var onQTEstepCompleted = new OnQTEStepCompleted(model);
          var mediator = new QTEMediator(
              model, 
              template,
              _view, 
              onQTEstepCompleted, 
              _playerControllerMaster, 
              singlePressChecker, 
              holdPressChecker);
      }
 }