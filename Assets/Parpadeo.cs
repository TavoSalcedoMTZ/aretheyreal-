using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parpadeo : MonoBehaviour
{
    public float minFlickInterval, maxFlickInterval;
    public float minLightIntensity;
    public float minFlickDuration, maxFlickDuration;

    private Light lightComponent;
    private float intervalTime, originalLightIntensity;
    private float flickDuration;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        originalLightIntensity = lightComponent.intensity;
        intervalTime = Random.Range(minFlickInterval, maxFlickInterval);
    }

    // Update is called once per frame
    void Update()
    {
        Flick();
    }

    void Flick()
    {

        if (intervalTime <= 0f)
        {
            lightComponent.intensity = Random.Range(minLightIntensity, originalLightIntensity);
            intervalTime = Random.Range(minFlickInterval, maxFlickInterval);
            flickDuration = Random.Range(minFlickDuration, maxFlickDuration);
        }
        else
        {
            if (flickDuration > 0f)
            {
                flickDuration -= 1f * Time.deltaTime;
            }
            else
            {
                intervalTime -= 1f * Time.deltaTime;
                lightComponent.intensity = originalLightIntensity;
            }
        }
    }
}