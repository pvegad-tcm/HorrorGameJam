using System;
using UnityEngine;

[Serializable]
public class QuickTimeEventStep
{
    public KeyCode InputKeyCode;
    public float InputNeededLength;
    public QuickTimeEventType Type;
}