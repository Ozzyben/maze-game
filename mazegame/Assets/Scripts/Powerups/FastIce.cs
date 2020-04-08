using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastIce : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "MazeRunner")
        {
            StartCoroutine(WaitBlock());
        }
    }

    IEnumerator WaitBlock()
    {
        GameObject.Find("MazeRunner").SendMessage("OnSlippyIce", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("MazeRunner").SendMessage("OnNormalTile", SendMessageOptions.DontRequireReceiver);
    }
}
