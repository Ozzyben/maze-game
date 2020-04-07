﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MazeDirectives : MonoBehaviour
{

    public int keysToFind = 3;
    public Text keysValueText;
    public MazeGoal mazeGoalPrefab;
    public MazeKey mazeKeyPrefab;

    MazeGoal mazeGoal;

    int foundKeys;

    void Start()
    {
        MazeGenerator.OnMazeReady += StartDirectives;
    }

    void Awake()
    {
        SetKeyValueText();
    }

    void StartDirectives()
    {
        foundKeys = 0;
        mazeGoal = Instantiate(mazeGoalPrefab, MazeGenerator.instance.mazeGoalPosition, Quaternion.identity) as MazeGoal;
        mazeGoal.transform.SetParent(transform);

        var mazeKeyPositions = MazeGenerator.instance.GetRandomFloorPositions(keysToFind);

        for (int i = 0; i < mazeKeyPositions.Count; i++)
        {
            MazeKey mazeKey = Instantiate(mazeKeyPrefab, mazeKeyPositions[i], Quaternion.identity) as MazeKey;
            mazeKey.transform.SetParent(transform);
        }
    }

    void OnMoveKey()
    {
        var mazeKeyPosition = MazeGenerator.instance.GetRandomFloorPositions(1)[0];
        MazeKey mazeKey = Instantiate(mazeKeyPrefab, mazeKeyPosition, Quaternion.identity) as MazeKey;
        mazeKey.transform.SetParent(transform);
    }

    public void OnGoalReached()
    {
        Debug.Log("Goal Reached");
        if (foundKeys == keysToFind)
        {
            GameObject.Find("console_text").SendMessage("keysFound", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Escaped first maze");
        }

    }

    public void OnKeyFound()
    {
        foundKeys++;

        SetKeyValueText();

        if (foundKeys == keysToFind)
        {
            GetComponentInChildren<MazeGoal>().OpenGoal();
        }
    }

    void SetKeyValueText()
    {
        keysValueText.text = foundKeys.ToString() + " of " + keysToFind.ToString();
    }


}