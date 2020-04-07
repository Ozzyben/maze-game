using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    int levelCounter = 1;
    public Text levelText;

    public void OnNextLevel()
    {
        levelCounter++;
        levelText.text = levelCounter.ToString();
    }

    public void OnReset()
    {
        levelCounter = 1;
        levelText.text = levelCounter.ToString();
    }

    public void Start()
    {
        levelText.text = levelCounter.ToString();
    }

}
