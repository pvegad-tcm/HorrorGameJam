using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Quick Time Event", menuName = "QuickTimeEvents/Template")]
public class QuickTimeEventTemplate : ScriptableObject
{
    public TimelineAsset PreConditionAnimation;
    public QuickTimeEventStep[] QuickTimeEventSteps;
}