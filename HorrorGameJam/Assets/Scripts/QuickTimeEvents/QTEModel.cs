public class QTEModel
{
    public bool QTEIsActive;
    public bool UserCanInteract;
    public int CurrentQTEIndex;
    public int PressedTimes;
    public double TimeHolding;

    public QTEModel()
    {
        QTEIsActive = true;
        UserCanInteract = true;
        CurrentQTEIndex = 0;
    }
    
    public bool IsQTECompleted(int totalSteps)
    {
        return CurrentQTEIndex >= totalSteps;
    }
}