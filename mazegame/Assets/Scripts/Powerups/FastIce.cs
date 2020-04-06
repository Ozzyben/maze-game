using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIce : MonoBehaviour
{
    public SpeedManager speedManager;

    void Awake()
    {
        GameObject speedManagerObj = GameObject.FindWithTag("SpeedManager");
        if (speedManagerObj != null)
        {
            speedManager = speedManagerObj.GetComponent<SpeedManager>();
            Debug.Log("SpeedManager linked");
        }
    }

    void OnTriggerEnter2D()
    {
        StartCoroutine(WaitBlock());
    }

    IEnumerator WaitBlock()
    {
        speedManager.setSpeedMultiplier(1.5f);
        Debug.Log("Speed up active");
        yield return new WaitForSeconds(5);
        Debug.Log("Speed up ended");
        speedManager.setSpeedMultiplier(1.0f);
    }
}
