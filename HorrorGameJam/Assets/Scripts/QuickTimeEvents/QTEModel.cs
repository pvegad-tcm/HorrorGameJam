using System;
using UnityEngine;
using UnityEngine.Events;

public class QTEModel
{
    public bool QTEIsActive;
    public bool UserCanInteract;
    public int CurrentQTEIndex;
    public QuickTimeEventStep[] QTESteps;
    public double TimeHolding;


    public QTEModel(QuickTimeEventTemplate quickTimeEventTemplate)
    {
        QTEIsActive = true;
        UserCanInteract = true;
        CurrentQTEIndex = 0;
        QTESteps = quickTimeEventTemplate.QuickTimeEventSteps;
    }

    public bool IsCurrentQTEStepCompleted()
    {
        //TODO: change with polimorphysm
        if (QTESteps[CurrentQTEIndex].Type == QuickTimeEventType.SinglePress)
        {
            return true;
        }

        if (QTESteps[CurrentQTEIndex].Type == QuickTimeEventType.Hold)
        {
            if (TimeHolding >= QTESteps[CurrentQTEIndex].InputNeededLength)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsQTECompleted()
    {
        return CurrentQTEIndex >= QTESteps.Length - 1;
    }

    public void UpdateCurrentQTEIndex()
    {
        CurrentQTEIndex++;
    }
}