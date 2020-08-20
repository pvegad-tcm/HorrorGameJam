using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class QuickTimeEventStep
{
    public KeyCode InputKeyCode;
    public float InputNeededLength;
    public QuickTimeEventType Type;
    public TimelineAsset CallbackAnimation;
}