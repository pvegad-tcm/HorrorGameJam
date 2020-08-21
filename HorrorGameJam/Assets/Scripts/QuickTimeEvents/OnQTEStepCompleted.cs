using UnityEngine;

public class OnQTEStepCompleted
{
    private readonly QTEModel _model;

    public OnQTEStepCompleted(QTEModel model)
    {
        _model = model;
    }
    public void Execute()
    {
        _model.UpdateCurrentQTEIndex();
    }
}