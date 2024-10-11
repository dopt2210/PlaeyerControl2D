using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPoint : MonoBehaviour
{
    public float speed = 1f;
    public int pointIndex = 0;
    public float pointDis = Mathf.Infinity;
    public Transform pointHolder;
    public List<Transform> points;

    private void Start()
    {
        this.LoadPoint();
    }
    private void Update()
    {
        this.PointMoving();
    }
    private void FixedUpdate()
    {
        this.NextPointCal();
    }

    private void NextPointCal()
    {
        this.pointDis = Vector2.Distance(transform.position, this.GetCurrentPoint().position);
        if (pointDis == 0) { pointIndex++; }
        if (pointIndex >= points.Count) { pointIndex = 0; }
    }

    private void PointMoving()
    {
        float step = this.speed * Time.deltaTime;
        Transform currentPoint = this.GetCurrentPoint();
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, step);

    }

    private Transform GetCurrentPoint()
    {
       return points[this.pointIndex]; 
    }

    private void LoadPoint()
    {
        pointHolder = GameObject.Find("_ListPoint").transform;
        foreach (Transform transform in pointHolder)
        {
            this.points.Add(transform);
        }
    }
}
