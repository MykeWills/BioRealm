using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRocketOrbit : MonoBehaviour
{
    public Transform target;
    public Transform center;
    public Vector3 distance;
    public float degreesPerSecond = 0.0f;

    void Start()
    {
        distance = transform.position - center.position;

    }
    public void MoveRocket()
    {
        transform.LookAt(target.position);
        //transform.Rotate(new Vector3(0, 180, -90), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * distance;
        transform.position = center.position + distance;
    }

}
