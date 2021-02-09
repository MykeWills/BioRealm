
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IceDragonRotation : MonoBehaviour
{
    public Transform player;
    public Transform center;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    public float degreesPerSecond = -65.0f;

    private Vector3 distance;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        distance = transform.position - center.position;
        distance.x = DistanceX;
        distance.y = DistanceY;
        distance.z = DistanceZ;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RotateAroundPlayer()
    {
        transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
        transform.Rotate(new Vector3(-0, -180, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = center.position + distance;
    }
}

