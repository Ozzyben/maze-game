using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float lightIntensity;
    public float flickerIntensity;

    float flickerTime = 0.5f;
    bool isFlickering = false;
    System.Random rg;

    Light flashlight;

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
            int flickerCount = rg.Next(3, 6);

            for (var i = 0; i < flickerCount; i++)
            {
                float flickingIntensity = lightIntensity - ((float)rg.NextDouble() * flickerIntensity);
                flashlight.intensity = flickingIntensity;

                float flickingTime = (float)rg.NextDouble() * flickerTime;
                yield return new WaitForSeconds(flickingTime);
            }
        }
    }

    public void BatteryRunningLow()
    {
        isFlickering = true;
        lightIntensity /= 2f;
        flickerTime /= 2f;
    }

    public void OnGameOver()
    {
        isFlickering = false;
        lightIntensity = 0f;
        flickerIntensity = 0f;
        flickerTime = 0f;
    }

    public void ResetFlashlight()
    {
        isFlickering = false;
        lightIntensity = 1.8f;
        flickerTime = 0.5f;
    }

}