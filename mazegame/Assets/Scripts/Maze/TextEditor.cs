using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{

    string key1_found = "You found a key! I wonder where it fits...";
    string key2_found = "You found a second key!";
    string key3_found = "You found a third key! ... *click*... Sounds like a door has opened";

    string goal_reached_without_keys = "This door is really strong! Looks like you need more keys to open it..";
    string goal_reached_with_keys = "This door is open. You go through..";

    bool keysFound = false;

    public Text consoletext;
    
    Stack keys = new Stack();

    void Start()
    {
        
        keys.Push(key3_found);
        keys.Push(key2_found);
        keys.Push(key1_found);

    }

    public void OnKeyFound()
    {
        consoletext.text = (string)keys.Pop();
    }

    public void OnGoalReached()
    {
        if (keysFound)
            consoletext.text = goal_reached_with_keys;
        else
            consoletext.text = goal_reached_without_keys;
    }

    void KeysFound()
    {
        keysFound = true;
    }


    
}
