using UnityEngine;

public class QTEInstaller : MonoBehaviour
{
    [SerializeField] private QTEView _view;
    [SerializeField] private QuickTimeEventTemplate _qteTemplate;
    private void Awake()
    {
        var model = new QTEModel(_qteTemplate);
        var mediator = new QTEMediator(_qteTemplate, model, _view);
    }
}