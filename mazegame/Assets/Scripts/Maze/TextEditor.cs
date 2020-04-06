using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    string goal_reached_without_keys = "This door is really strong! Looks like you need more keys to open it..";
    string goal_reached_with_keys = "This door is open. You go through..";

    bool keysFound = false;

    public Text consoletext;
    
    Stack keys = new Stack();

    void Start()
    {
        var first_key_found = "You found a key! I wonder where it fits...";
        var second_key_found = "You found a second key!";
        var third_key_found = "You found a third key! ... *click*... Sounds like a door has opened";

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
