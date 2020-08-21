using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Quick Time Event", menuName = "QuickTimeEvents/Template")]
public class QuickTimeEventTemplate : ScriptableObject
{
    public QTEInstaller _installer;
    public QuickTimeEventStep[] QuickTimeEventSteps;

    public void StartQTE()
    {
        //TODO: iniciar timeline y despues instalar el monobehaviour
        _installer.Install(this);
    }
}