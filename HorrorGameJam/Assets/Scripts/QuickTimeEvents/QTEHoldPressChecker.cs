public class QTEHoldPressChecker : QTEStepCompletedChecker
{
    private readonly QuickTimeEventTemplate _qteTemplate;

    public QTEHoldPressChecker(QuickTimeEventTemplate qteTemplate)
    {
        _qteTemplate = qteTemplate;
    }
    public bool IsStepFinished(QTEModel model)
    {
        var currentStep = _qteTemplate.QuickTimeEventSteps[model.CurrentQTEIndex];
        return model.TimeHolding >= currentStep.InputNeededLength;
    }
}