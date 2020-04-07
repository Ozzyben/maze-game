using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    int levelCounter = 0;
    public Text levelText;

    public void OnNextLevel()
    {
        levelCounter++;
        levelText.text = levelCounter.ToString();
    }

    public void ResetLevel()
    {
        levelCounter = 0;
        levelText.text = levelCounter.ToString();
    }

    public void Start()
    {
        OnNextLevel();
    }

}
