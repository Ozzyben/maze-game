using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float speedMultiplier = 1;

    public void setSpeedMultiplier(float speed)
    {
        speedMultiplier = speed;
    }

    public float getSpeedMultiplier()
    {
        return speedMultiplier;
    }
}
