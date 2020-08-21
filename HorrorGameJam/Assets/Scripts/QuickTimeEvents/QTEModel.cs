using System;
using UnityEngine;
using UnityEngine.Events;

public class QTEModel
{
    public QuickTimeEventStep currentQTEStep
    {
        get => qteSteps[currentQTEIndex];
    }

    public int currentQTEIndex;
    public QuickTimeEventStep[] qteSteps;
    public QuickTimeEventStep CurrentQTEStep;
    public UnityEvent KeyPressed;
    public double TimeHolding;


    public QTEModel(QuickTimeEventTemplate quickTimeEventTemplate)
    {
        currentQTEIndex = 0;
        qteSteps = quickTimeEventTemplate.QuickTimeEventSteps;
        Debug.Log(quickTimeEventTemplate.QuickTimeEventSteps[0].InputKeyCode);
        Debug.Log(quickTimeEventTemplate.QuickTimeEventSteps[1].InputKeyCode);
        CurrentQTEStep = quickTimeEventTemplate.QuickTimeEventSteps[0];
        //if (KeyPressed == null)
        //    KeyPressed = new UnityEvent();

    }

    public bool IsCurrentQTEStepCompleted()
    {
        //TODO: change with polimorphysm
        if (qteSteps[currentQTEIndex].Type == QuickTimeEventType.SinglePress)
        {
            return true;
        }
        if (qteSteps[currentQTEIndex].Type == QuickTimeEventType.Hold)
        {
            if (TimeHolding >= qteSteps[currentQTEIndex].InputNeededLength)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsQTECompleted()
    {
        return currentQTEIndex >= qteSteps.Length;
    }

    public void UpdateCurrentQTEIndex()
    {
        //move to commands
        currentQTEIndex++;
    }
}