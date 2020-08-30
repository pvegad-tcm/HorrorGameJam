using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector timeline;

    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "PlayerTag")
        {
            timeline.Play();
        }
    }
}
