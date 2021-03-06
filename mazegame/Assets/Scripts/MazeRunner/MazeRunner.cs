﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunner : MonoBehaviour
{

    public float walkSpeed;
    public float rotationSpeed;

    public Transform rotationTransform;

    int targetX = 1;
    int targetY = 1;

    int currentX = 1;
    int currentY = 1;

    float currentAngle;
    float lastAngle;
    float speedMultiplier = 1f;

    void Update()
    {
        var targetReached = (((transform.position.x) == (targetX)) && ((transform.position.y) == (targetY)));

        var currentX = Mathf.FloorToInt(transform.position.x);
        var currentY = Mathf.FloorToInt(transform.position.y);

        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        var newAngle = lastAngle;

        var right = false;
        var left = false;
        var up = false;
        var down = false;

        //8 directional movement
        if (direction.x > 0)
        {
            right = true;
            newAngle = 270;

            if (MazeGenerator.instance.GetMazeGridCell(currentX + 1, currentY) && targetReached)
            {
                targetX = currentX + 1;
            }
        }
        if (direction.x < 0)
        {
            left = true;
            newAngle = 90;

            if (MazeGenerator.instance.GetMazeGridCell(currentX - 1, currentY) && targetReached)
            {
                targetX = currentX - 1;
            }
        }
        if (direction.y > 0)
        {
            up = true;
            newAngle = 0;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY + 1) && targetReached)
            {
                targetY = currentY + 1;
            }
        }
        if (direction.y < 0)
        {
            down = true;
            newAngle = 180;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY - 1) && targetReached)
            {
                targetY = currentY - 1;
            }
        }
        //check diagonals for light angle
        if (right && up)
            newAngle = 315;
        else if (right && down)
            newAngle = 225;
        else if (left && down)
            newAngle = 135;
        else if (left && up)
            newAngle = 45;

        currentAngle = Mathf.LerpAngle(currentAngle, newAngle, speedMultiplier * rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), speedMultiplier * walkSpeed * Time.deltaTime);
        rotationTransform.eulerAngles = new Vector3(0, 0, currentAngle);
        lastAngle = newAngle;
    }

    public void resetPlayer()
    {
        this.currentX = 1;
        this.currentY = 1;
        targetX = 1;
        targetY = 1;

        this.transform.position = new Vector3(1, 1, 0);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(1, 1), walkSpeed * Time.deltaTime);
        Debug.Log("resetPlayer");
    }

    void reachedDoor()
    {
        float doorX = GameObject.Find("MazeGoal").transform.position.x;
        float doorY = GameObject.Find("MazeGoal").transform.position.y;

        if (this.currentX == doorX && this.currentY == doorY)
        {
            Debug.Log("atDoor");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Enemy")
        {
            GameObject.Find("console_text").SendMessage("OnGameOver");
        }
    }

    void OnStuckInMud()
    {
        speedMultiplier = 0.25f;
    }

    void OnSlippyIce()
    {
        speedMultiplier = 4.0f;
    }

    void OnNormalTile()
    {
        speedMultiplier = 1.0f;
    }

}
