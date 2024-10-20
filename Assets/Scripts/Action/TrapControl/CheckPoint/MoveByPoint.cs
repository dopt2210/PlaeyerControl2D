using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPoint : MonoBehaviour
{
    private static MoveByPoint instance;
    public static MoveByPoint Instance => instance;
    Vector2 startPosition;
    public float speed = 2f, acceleration = 0.5f, maxSpeed = 10f;
    public int pointIndex = 0;
    public float pointDis = Mathf.Infinity;
    public Transform pointHolder;
    public List<Transform> points;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Checkpoint Ctrl allowed"); }
        instance = this;
        startPosition = transform.position;
    }

    public void NextPointCal()
    {
        this.pointDis = Vector2.Distance(transform.position, this.GetCurrentPoint().position);
        if (pointDis == 0) { pointIndex++; }
        if (pointIndex >= points.Count) { pointIndex = 0; }
    }

    public void PointMoving()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        float step = this.speed * Time.deltaTime;
        Transform currentPoint = this.GetCurrentPoint();
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, step);

    }
    public void EarlyEndPointMoving()
    {
        float step = this.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, startPosition, step);
    }

    private Transform GetCurrentPoint()
    {
       return points[this.pointIndex]; 
    }

    public void LoadPoint()
    {
        pointHolder = GameObject.FindGameObjectWithTag("MovePoint").transform;
        foreach (Transform transform in pointHolder)
        {
            this.points.Add(transform);
        }
    }
}
