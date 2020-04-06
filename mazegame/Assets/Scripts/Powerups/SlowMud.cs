using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMud : MonoBehaviour
{
    public SpeedManager speedManager;

    void Awake()
    {
        GameObject speedManagerObj = GameObject.FindWithTag("SpeedManager");
        if (speedManagerObj != null)
        {
            speedManager = speedManagerObj.GetComponent<SpeedManager>();
        }
    }

    void OnTriggerEnter2D()
    {
        StartCoroutine(WaitBlock());
    }

    IEnumerator WaitBlock()
    {
        speedManager.setSpeedMultiplier(0.5f);
        yield return new WaitForSeconds(0.1);
        speedManager.setSpeedMultiplier(1.0f);
    }
}
