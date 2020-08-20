using System;
using UnityEngine;

public class QTEModel
{
    private KeyCode currentWaitingKey;

    public QTEModel(QuickTimeEventTemplate quickTimeEventTemplate)
    {
        currentWaitingKey = quickTimeEventTemplate.QuickTimeEventSteps[0].InputKeyCode;
    }
}