using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeKey : MonoBehaviour
{

    void OnTriggerEnter2D()
    {
        transform.parent.SendMessage("OnKeyFound", SendMessageOptions.DontRequireReceiver);
        GameObject.Destroy(gameObject);
        GameObject.Find("console_text").SendMessage("OnKeyFound", SendMessageOptions.DontRequireReceiver);
    }

}
