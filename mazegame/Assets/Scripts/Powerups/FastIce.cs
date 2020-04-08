using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIce : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        StartCoroutine(WaitBlock());
    }

    IEnumerator WaitBlock()
    {
        GameObject.Find("MazeRunner").SendMessage("OnSlippyIce", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("MazeRunner").SendMessage("OnNormalTile", SendMessageOptions.DontRequireReceiver);
    }
}
