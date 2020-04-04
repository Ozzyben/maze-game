using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIce : MonoBehaviour
{
    private SpeedManager speedManager;

    void Awake()
    {
        speedManager = GetComponent<SpeedManager>();
    }

    void onTriggerEvent2D()
    {
        speedManager.setSpeedMultiplier(1.5f);
    }
}
