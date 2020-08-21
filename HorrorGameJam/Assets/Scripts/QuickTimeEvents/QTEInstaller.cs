using UnityEngine;

public class QTEInstaller : MonoBehaviour
 {
      [SerializeField] private QTEView _view;
      [SerializeField] private QuickTimeEventTemplate _qteTemplate;

      private void Start()
      {
          //TODO: remove
          Install(_qteTemplate);
      }

      public void Install(QuickTimeEventTemplate template)
      {
          var model = new QTEModel(_qteTemplate);

          var onQTEstepCompleted = new OnQTEStepCompleted(model);
          var mediator = new QTEMediator(_qteTemplate, model, _view, onQTEstepCompleted);
      }
 }