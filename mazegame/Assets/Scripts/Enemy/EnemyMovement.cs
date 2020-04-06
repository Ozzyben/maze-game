using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed;
    public float rotationSpeed;

    public Transform rotationTransform;

    int targetX = -1;
    int targetY = -1;

    int currentX = 1;
    int currentY = 1;

    float currentAngle;
    float lastAngle;

    void Update()
    {
        currentX = Mathf.FloorToInt(transform.position.x);
        currentY = Mathf.FloorToInt(transform.position.y);

        bool targetReached = (currentX == targetX) && (currentY == targetY);
        bool noTargetSet = (targetX == -1) && (targetY == -1);
        if (targetReached || noTargetSet)
        {
            var newTarget = PickNewTarget();
            targetX = (int)newTarget.x;
            targetY = (int)newTarget.y;
        }

        MoveEnemy();
    }

    Vector2 PickNewTarget()
    {
        int[,] region = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
        Vector2[] availableMoves = new Vector2[4];
        for(int j = 0; j < region.GetLength(0); j++)
        {
            int x = currentX + region[j, 0];
            int y = currentY + region[j, 1];
            Vector2 move = new Vector2(-1, -1);
            if (MazeGenerator.instance.GetMazeGridCell(x, y))
            {
                move.x = x;
                move.y = y;   
            }
            availableMoves[j] = move;                
        }
        Vector2 target = new Vector2(-1,-1);
        System.Random rnd = new System.Random();
        while (target.x < 1 || target.y < 1)
        {
            var randomIndex = rnd.Next(4);
            target = availableMoves[randomIndex];
        }
        return target;
    }

    void MoveEnemy()
    {
        bool targetReached = (currentX == targetX) && (currentY == targetY);
        Vector2 direction = Vector2.zero;
        direction.x = targetX - currentX;
        direction.y = targetY - currentY;
        float newAngle = 0;

        if (direction.x > 0)
        {
            newAngle = 270;

            if (MazeGenerator.instance.GetMazeGridCell(currentX + 1, currentY) && targetReached)
            {
                targetX = currentX + 1;
                targetY = currentY;
            }
        }
        else if (direction.x < 0)
        {
            newAngle = 90;

            if (MazeGenerator.instance.GetMazeGridCell(currentX - 1, currentY) && targetReached)
            {
                targetX = currentX - 1;
                targetY = currentY;
            }
        }
        else if (direction.y > 0)
        {
            newAngle = 0;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY + 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY + 1;
            }
        }
        else if (direction.y < 0)
        {
            newAngle = 180;

            if (MazeGenerator.instance.GetMazeGridCell(currentX, currentY - 1) && targetReached)
            {
                targetX = currentX;
                targetY = currentY - 1;
            }
        }
        else
        {
            newAngle = lastAngle;
        }

        currentAngle = Mathf.LerpAngle(currentAngle, newAngle, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), walkSpeed * Time.deltaTime);
        rotationTransform.eulerAngles = new Vector3(0, 0, currentAngle);
        lastAngle = newAngle;
    }
}
