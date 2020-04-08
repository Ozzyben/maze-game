using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
   
    string nextLevel = "Found some batteries.";

    string[] enemyText = { "Oh no its grandma. Don't let her take me to church",
                           "Avoid the dentist!",
                           "Oh no the tax collector is here!",
                           "I have no idea what that red light is...",
                           "Just what this maze needs. A scary clown...",
                           "Don't get sucked up by that Henry Hoover",
                           "I owe £0.69 to that librarian. Better not let her catch me",
                           "My god, that bunny has rabies!" };

    string[] gameOverText = {  "Church time",
                                "You have 5 new fillings",
                                "But they'll never take you alive",
                                "Now you're back to the beginning",
                                "The clown stabbed you with a banana",
                                "That sucks",
                                "You're broke now",
                                "Aaaaaaa...."
    };

    System.Random rnd;
    int enemyIndex = 0;

    public Text consoletext;
    
    Stack keys = new Stack();

    string first_key_found = "You found a key! I wonder where it fits...";
    string second_key_found = "You found a second key!";
    string third_key_found = "You found a third key! ... *click*... Sounds like a door has opened";

    void Start()
    {
        rnd = new System.Random(); 
        keys.Push(third_key_found);
        keys.Push(second_key_found);
        keys.Push(first_key_found);
        OnGoalReached();
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
        enemyIndex = rnd.Next(0, enemyText.Length);
        consoletext.text = nextLevel + " " + enemyText[enemyIndex];
    }

    public void OnGameOver()
    {
        consoletext.text = "Game Over!" + " " + gameOverText[enemyIndex];
        GameObject.Find("Maze").SendMessage("LoadNextLevel", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("MazeRunner").SendMessage("OnNormalTile", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("TimeManager").SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("ScoreValue").SendMessage("OnReset");
        var flashLights = GameObject.FindGameObjectsWithTag("FlashLight");
        foreach (var light in flashLights)
            light.SendMessage("ResetFlashlight");
        OnGoalReached();
    }

    public void OnBatteryLow()
    {
        consoletext.text = "I need to hurry. The batteries are running out...";
    }

}
