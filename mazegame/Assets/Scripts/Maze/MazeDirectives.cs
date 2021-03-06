﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MazeDirectives : MonoBehaviour
{

    public int keysToFind;
    public Text keysValueText;
    public MazeGoal mazeGoalPrefab;
    public MazeKey mazeKeyPrefab;

    public int numSlowSpots;
    public int numFastSpots;
    public SlowMud slowMudPrefab;
    public FastIce fastIcePrefab;

    List<Vector3> mazeKeyPositions;

    //List<Vector3> slowMudPositions;
    //List<Vector3> fastIcePositions;

    MazeGoal mazeGoal;

    public int foundKeys;

    void Start()
    {
        SetKeyValueText();
    }

    public static MazeDirectives instance;


    void Awake()
    {
        MazeGenerator.OnMazeReady += StartDirectives;
        instance = this;
    }

    void StartDirectives()
    {
        mazeGoal = Instantiate(mazeGoalPrefab, MazeGenerator.instance.mazeGoalPosition, Quaternion.identity) as MazeGoal;
        mazeGoal.transform.SetParent(transform);

        var mazeKeyPositions = MazeGenerator.instance.GetRandomFloorPositions(keysToFind);

        for (int i = 0; i < mazeKeyPositions.Count; i++)
        {
            MazeKey mazeKey = Instantiate(mazeKeyPrefab, mazeKeyPositions[i], Quaternion.identity) as MazeKey;
            mazeKey.transform.SetParent(transform);
        }

        var slowMudPositions = MazeGenerator.instance.GetRandomFloorPositions(numSlowSpots);

        for (int i = 0; i < slowMudPositions.Count; i++)
        {
            SlowMud slowMud = Instantiate(slowMudPrefab, slowMudPositions[i], Quaternion.identity) as SlowMud;
            slowMud.transform.SetParent(transform);
        }

        var fastIcePositions = MazeGenerator.instance.GetRandomFloorPositions(numFastSpots);

        for (int i = 0; i < fastIcePositions.Count; i++)
        {
            FastIce fastIce = Instantiate(fastIcePrefab, fastIcePositions[i], Quaternion.identity) as FastIce;
            fastIce.transform.SetParent(transform);
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
            Debug.Log("Escape the maze");
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

    void reset()
    {
        foundKeys = 0;
        keysToFind = 3;
        GetComponentInChildren<MazeGoal>().closeGoal();
        Debug.Log("reset keys");

    }
}