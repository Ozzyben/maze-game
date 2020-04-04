using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MazeDirectives : MonoBehaviour
{

    public int keysToFind;
    public Text keysValueText;
    public MazeGoal mazeGoalPrefab;
    public MazeKey mazeKeyPrefab;

    public int slowSpots;
    public int fastSpots;
    public SlowMud slowMudPrefab;
    public FastIce fastIcePrefab;

    MazeGoal mazeGoal;

    int foundKeys;
    List<Vector3> mazeKeyPositions;

    List<Vector3> slowMudPositions;
    List<Vector3> fastIcePositions;

    void Start()
    {
        SetKeyValueText();
    }

    void Awake()
    {
        MazeGenerator.OnMazeReady += StartDirectives;
    }

    void StartDirectives()
    {
        mazeGoal = Instantiate(mazeGoalPrefab, MazeGenerator.instance.mazeGoalPosition, Quaternion.identity) as MazeGoal;
        mazeGoal.transform.SetParent(transform);

        slowMudPositions = MazeGenerator.instance.GetRandomFloorPositions(slowSpots);

        for (int i = 0; i < slowMudPositions.Count; i++)
        {
            SlowMud slowMud = Instantiate(slowMudPrefab, slowMudPositions[i], Quaternion.identity) as SlowMud;
            slowMud.transform.SetParent(transform);
        }

        fastIcePositions = MazeGenerator.instance.GetRandomFloorPositions(fastSpots);

        for (int i = 0; i < fastIcePositions.Count; i++)
        {
            FastIce fastIce = Instantiate(fastIcePrefab, fastIcePositions[i], Quaternion.identity) as FastIce;
            fastIce.transform.SetParent(transform);
        }

        mazeKeyPositions = MazeGenerator.instance.GetRandomFloorPositions(keysToFind);

        for (int i = 0; i < mazeKeyPositions.Count; i++)
        {
            MazeKey mazeKey = Instantiate(mazeKeyPrefab, mazeKeyPositions[i], Quaternion.identity) as MazeKey;
            mazeKey.transform.SetParent(transform);
        }
    }

    public void OnGoalReached()
    {
        Debug.Log("Goal Reached");
        if (foundKeys == keysToFind)
        {
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


}