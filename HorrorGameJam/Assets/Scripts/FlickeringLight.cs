using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public Light _Light;

    public float flickerMinTime;
    public float flickerMaxTime;

    public float throbMinTime;
    public float throbMaxTime;
    public float throbMinIntensity;
    public float throbMaxIntensity;

    private float flickerTimer;
    private float throbTimer;


    public AudioSource AS;

    void Start()
    {
        flickerTimer = Random.Range(flickerMinTime, flickerMaxTime);
        throbTimer = Random.Range(throbMinTime, throbMaxTime);

        //Debug.Log("Initial Intensity: " + _Light.intensity);

    }

    void Update()
    {
        FlickerLight();
    }

    void FlickerLight()
    {
        if (flickerTimer > 0)
            flickerTimer -= Time.deltaTime;

        if (flickerTimer <= 0)
        {
            _Light.enabled = !_Light.enabled;
            flickerTimer = Random.Range(flickerMinTime, flickerMaxTime);
        }

        if (throbTimer > 0)
            throbTimer -= Time.deltaTime;

        if (throbTimer <= 0)
        {
            _Light.intensity = Random.Range(throbMinIntensity, throbMaxIntensity);
            throbTimer = Random.Range(throbMinTime, throbMaxTime);
           //Debug.Log(_Light.intensity);
        }
    }

    


}
