using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text displayTime;
    float currentTime = 0f;
    public float startTime;
    float checkPointTime;
    bool gameOverReached = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        checkPointTime = startTime / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverReached)
        {
            currentTime -= 1 * Time.deltaTime;
            displayTime.text = ((int)currentTime).ToString();
            if (currentTime <= 0)
            {
                gameOverReached = true;
                var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
                foreach (var light in flashLights)
                    light.SendMessage("OnGameOver");
                GameObject.Find("console_text").SendMessage("OnGameOver");
            }
            else if ((currentTime <= checkPointTime))
            {
                checkPointTime /= 2f;
                GameObject.Find("console_text").SendMessage("OnBatteryLow");
                var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
                foreach (var light in flashLights)
                    light.SendMessage("BatteryRunningLow");
            }
            
        }
    }

    public void OnReset()
    {
        gameOverReached = false;
        currentTime = startTime;
        checkPointTime = startTime / 2f;
    }
}