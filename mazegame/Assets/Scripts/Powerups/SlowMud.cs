using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMud : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        StartCoroutine(WaitBlock());
    }

    IEnumerator WaitBlock()
    {
        GameObject.Find("MazeRunner").SendMessage("OnStuckInMud", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("MazeRunner").SendMessage("OnNormalTile", SendMessageOptions.DontRequireReceiver);
    }
}
