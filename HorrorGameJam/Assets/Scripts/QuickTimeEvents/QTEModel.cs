using System;
using UnityEngine;
using UnityEngine.Events;

public class QTEModel
{
    public int CurrentQTEIndex;
    public QuickTimeEventStep[] QTESteps;
    public UnityEvent KeyPressed;
    public double TimeHolding;


    public QTEModel(QuickTimeEventTemplate quickTimeEventTemplate)
    {
        CurrentQTEIndex = 0;
        QTESteps = quickTimeEventTemplate.QuickTimeEventSteps;
        Debug.Log(quickTimeEventTemplate.QuickTimeEventSteps[0].InputKeyCode);
        Debug.Log(quickTimeEventTemplate.QuickTimeEventSteps[1].InputKeyCode);
        if (KeyPressed == null)
            KeyPressed = new UnityEvent();
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
        //move to commands
        CurrentQTEIndex++;
    }
}