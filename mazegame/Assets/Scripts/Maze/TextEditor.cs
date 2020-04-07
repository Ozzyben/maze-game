using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
   
    string nextLevel = "*** ESCAPE THE MAZE ***";

    public Text consoletext;
    
    Stack keys = new Stack();

    string first_key_found = "You found a key! I wonder where it fits...";
    string second_key_found = "You found a second key!";
    string third_key_found = "You found a third key! ... *click*... Sounds like a door has opened";

    void Start()
    { 

        keys.Push(third_key_found);
        keys.Push(second_key_found);
        keys.Push(first_key_found);
    }

    void restart()
    {
        keys.Push(third_key_found);
        keys.Push(second_key_found);
        keys.Push(first_key_found);
    }

    public void OnKeyFound()
    {
        consoletext.text = (string)keys.Pop();
    }

    public void OnMoveKey()
    {
        consoletext.text = "\"Yeet?\" Oh. The keys been moved...";
    }

    public void OnGoalReached()
    {
        consoletext.text = nextLevel;
    }

    public void OnGameOver()
    {
        consoletext.text = "Game Over!";
        GameObject.Find("Maze").SendMessage("LoadNextLevel", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("MazeRunner").SendMessage("OnNormalTile", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("TimeManager").SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("ScoreValue").SendMessage("OnReset");
        var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
        foreach (var light in flashLights)
            light.SendMessage("ResetFlashlight");
        consoletext.text = nextLevel;
    }

    public void OnBatteryLow()
    {
        consoletext.text = "I need to hurry. The batteries are running out...";
    }

}
