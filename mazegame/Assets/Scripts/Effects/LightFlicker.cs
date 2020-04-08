using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float startLightIntensity;
    public float startFlickerIntensity;
    float flickerIntensity;
    float lightIntensity;

    float flickerTime = 1f;
    bool isFlickering = false;
    System.Random rg;

    Light flashlight;

    void Start()
    {
        ResetFlashlight();
    }

    void Awake()
    {
        rg = new System.Random();
        flashlight = GetComponent<Light>();
    }

    void Update()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        flashlight.intensity = lightIntensity;

        if (isFlickering)
        {
            float flickingIntensity = lightIntensity - ((float)rg.NextDouble() * flickerIntensity);
            flashlight.intensity = flickingIntensity;
            float flickingTime = (float)rg.NextDouble() * flickerTime;
            yield return new WaitForSeconds(flickingTime);
        }
    }

    public void BatteryRunningLow()
    {
        isFlickering = true;
        lightIntensity /= 2f;
        flickerTime *= 3f;
    }

    public void OnGameOver()
    {
        isFlickering = false;
        lightIntensity = 0f;
        flickerIntensity = 0f;
    }

    public void ResetFlashlight()
    {
        isFlickering = false;
        lightIntensity = startLightIntensity;
        flickerIntensity = startFlickerIntensity;
        flickerTime = 1f;
    }

}