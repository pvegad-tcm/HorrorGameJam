public class QTEStepCompletedChecker
{
    private readonly QuickTimeEventTemplate _qteTemplate;

    public QTEStepCompletedChecker(QuickTimeEventTemplate qteTemplate)
    {
        _qteTemplate = qteTemplate;
    }
    public bool IsStepFinished(QTEModel model)
    {
        var currentStep = _qteTemplate.QuickTimeEventSteps[model.CurrentQTEIndex];
        var holdedEnoughTime = model.TimeHolding >= currentStep.InputNeededLength;
        var pressedEnoughTimes = model.PressedTimes >= currentStep.NumberOfInputsNeeded;
        return holdedEnoughTime && pressedEnoughTimes;
    }
}