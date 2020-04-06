using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeKey : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "MazeRunner")
        {
            transform.parent.SendMessage("OnKeyFound", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("console_text").SendMessage("OnKeyFound", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            transform.parent.SendMessage("OnMoveKey", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("console_text").SendMessage("OnMoveKey", SendMessageOptions.DontRequireReceiver);
        }
        GameObject.Destroy(gameObject);
    }

}
