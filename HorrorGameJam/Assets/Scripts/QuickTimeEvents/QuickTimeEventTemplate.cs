using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Quick Time Event", menuName = "QuickTimeEvents/Template")]
public class QuickTimeEventTemplate : ScriptableObject
{
    public QuickTimeEventStep[] QuickTimeEventSteps;
}