using UnityEngine;

public class QTEInstaller : MonoBehaviour
{
    [SerializeField] private QTEView _view;
    public void Install(QuickTimeEventTemplate template)
    {
        var model = new QTEModel(template);
        var mediator = new QTEMediator(template, model, _view);
    }
}