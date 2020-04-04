using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMud : MonoBehaviour
{
    private SpeedManager speedManager;

    void Awake()
    {
        speedManager = GetComponent<SpeedManager>();
    }

    void onTriggerEvent2D()
    {
        speedManager.setSpeedMultiplier(0.5f);
    }
}
