﻿using UnityEngine;

/// <summary>
/// Makes the platform move between two positions
/// </summary>
public class MovingPlatformScript : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        else if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
