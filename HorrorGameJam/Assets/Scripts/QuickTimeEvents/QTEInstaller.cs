using UnityEngine;

public class QTEInstaller : MonoBehaviour
 {
      [SerializeField] private QTEView _view;
      [SerializeField] private QuickTimeEventTemplate _qteTemplate;
      [SerializeField] private PlayerControllerMaster _playerControllerMaster;
      public void Install(QuickTimeEventTemplate template)
      {
          var model = new QTEModel(_qteTemplate);
          var onQTEstepCompleted = new OnQTEStepCompleted(model);
          var mediator = new QTEMediator(model, _view, onQTEstepCompleted, _playerControllerMaster);
      }
 }