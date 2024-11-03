using System.Collections.Generic;
using UnityEngine;

public class BridgeCtrl : MonoBehaviour
{
    private static BridgeCtrl instance;
    public static BridgeCtrl Instance => instance;
    Vector2 startPosition;
    public float speed = 2f, acceleration = 0.5f, maxSpeed = 10f;
    public int pointIndex = 0;
    public float pointDis = Mathf.Infinity;
    public bool MoveBridge;
    public Transform pointHolder;
    public List<Transform> points;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); Debug.LogError("Only one Checkpoint Ctrl allowed"); }
        instance = this;
        startPosition = transform.position;
    }
    private void Start()
    {
        LoadPoint();
    }
    private void Update()
    {
        if (!MoveBridge)
            EarlyEndPointMoving();
        else
            PointMoving();

    }
    private void FixedUpdate()
    {
        NextPointCal();
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
        pointHolder = transform.GetChild(0);
        foreach (Transform transform in pointHolder)
        {
            this.points.Add(transform);
        }
    }
}
