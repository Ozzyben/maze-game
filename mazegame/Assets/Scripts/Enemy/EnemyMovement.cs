using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    float currentSpeed;
    public float rotationSpeed;
    public Transform rotationTransform;
    int visionRadius = 3;
    int targetX = -1;
    int targetY = -1;

    int currentX = 1;
    int currentY = 1;

    float currentAngle;
    float lastAngle;
    bool persuing;

    void Update()
    {
        if(persuing)
            currentSpeed = Mathf.Min(runSpeed, currentSpeed + 0.05f);
        else
            currentSpeed = Mathf.Max(walkSpeed, currentSpeed - 0.05f);
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

    List<Vector2> GetAvailableMoves()
    {
        int[,] region = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
        List<Vector2> availableMoves = new List<Vector2>();
        for(int j = 0; j<region.GetLength(0); j++)
        {
            int x = currentX + region[j, 0];
            int y = currentY + region[j, 1];
            Vector2 move = new Vector2(-1, -1);
            if (MazeGenerator.instance.GetMazeGridCell(x, y))
            {
                move.x = x;
                move.y = y;
                availableMoves.Add(move);
            }
        }
        if (availableMoves.Count == 0)
            availableMoves.Add(new Vector2(0, 0));
        return availableMoves;
    }

    Vector2 GetClosestObject(Vector2 currentTarget)
    {
        var playerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        bool playerInRegion = (Mathf.Abs(playerPosition.x - currentTarget.x) <= visionRadius) && (Mathf.Abs(playerPosition.y - currentTarget.y) <= visionRadius);
        if (playerInRegion)
        {
            persuing = true;
            return playerPosition;
        }
        else
        {
            persuing = false;
            return currentTarget;
        }
    }

    Vector2 PickNewTarget()
    {
        var availableMoves = GetAvailableMoves();
        Vector2 target = new Vector2(-1,-1);
        System.Random rnd = new System.Random();
        var randomIndex = rnd.Next(availableMoves.Count);
        target = availableMoves[randomIndex];

        target = GetClosestObject(target);
        return target;
    }

    void MoveEnemy()
    {
        bool targetReached = (currentX == targetX) && (currentY == targetY);
        Vector2 direction = Vector2.zero;
        direction.x = targetX - currentX;
        direction.y = targetY - currentY;
        float newAngle = lastAngle;

        if (direction.x > 0)
        {
            newAngle = 270;
            if (!MazeGenerator.instance.GetMazeGridCell(currentX + 1, currentY))
                targetX = currentX;
        }
        if (direction.x < 0)
        {
            newAngle = 90;
            if (!MazeGenerator.instance.GetMazeGridCell(currentX - 1, currentY))
                targetX = currentX;
        }
        if (direction.y > 0)
        {
            newAngle = 0;
            if (!MazeGenerator.instance.GetMazeGridCell(currentX, currentY + 1))
                targetY = currentY;
        }
        if (direction.y < 0)
        {
            newAngle = 180;
            if (!MazeGenerator.instance.GetMazeGridCell(currentX, currentY - 1))
                targetY = currentY;
        }

        currentAngle = Mathf.LerpAngle(currentAngle, newAngle, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY), currentSpeed * Time.deltaTime);
        rotationTransform.eulerAngles = new Vector3(0, 0, currentAngle);
        lastAngle = newAngle;
    }
}
