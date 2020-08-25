using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShakeManager : MonoBehaviour
{
    public List<Shake> shakeList;
    public float intensity;
    public float duration;

    private bool activated;


    // Start is called before the first frame update
    void Start()
    {

        foreach (Shake shake in shakeList)
        {
            shake.Begin(intensity, duration);
        }
    }
}
