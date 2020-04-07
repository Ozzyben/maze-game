using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGoal : MonoBehaviour
{
    
    public Sprite closedGoalSprite;
    public Sprite openedGoalSprite;
 

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = closedGoalSprite;
    }

    public void OpenGoal()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = openedGoalSprite;
    }

    public void closeGoal()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = closedGoalSprite;
    }

    void OnTriggerEnter2D()
    {
        if (MazeDirectives.instance.keysToFind == MazeDirectives.instance.foundKeys)
        {
            transform.parent.SendMessage("OnGoalReached", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("console_text").SendMessage("OnGoalReached", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("Maze").SendMessage("LoadNextLevel", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("TimeManager").SendMessage("OnLoadNextLevel", SendMessageOptions.DontRequireReceiver);        
            GameObject.Find("ScoreValue").SendMessage("OnNextLevel");

            var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
            foreach (var light in flashLights)
                light.SendMessage("ResetFlashlight");

        }
    }

    void OnReset()
    {
        closeGoal();
        GameObject.Find("ScoreValue").SendMessage("ResetLevel");
        var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
        foreach (var light in flashLights)
            light.SendMessage("ResetFlashlight");

    }

}
