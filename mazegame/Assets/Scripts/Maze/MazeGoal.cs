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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "MazeRunner")
        {
            transform.parent.SendMessage("OnGoalReached", SendMessageOptions.DontRequireReceiver);
            GameObject.Find("console_text").SendMessage("OnGoalReached", SendMessageOptions.DontRequireReceiver);
            GameObject.Destroy(gameObject);
        }
    }

}
