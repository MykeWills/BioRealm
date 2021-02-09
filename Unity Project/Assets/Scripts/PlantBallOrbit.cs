using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBallOrbit : MonoBehaviour
{
    public Transform target;
    public Transform center;
    public Vector3 distance;
    public float degreesPerSecond = 0.0f;

    void Start()
    {
        target = Camera.main.transform;
        distance = transform.position - center.position;

    }
    public void MovePlantBall()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(90, 180, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * distance;
        transform.position = center.position + distance;
    }

}
