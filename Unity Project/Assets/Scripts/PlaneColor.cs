using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColor : MonoBehaviour {
    float speed = 1.0f;

    float timeStamp;

    void Start()
    {
     
    }

    void Update()
    {

        Color newColor;


        newColor.r = (Mathf.Sin(Time.time * speed) + 1f) / 1;
        newColor.g = (Mathf.Sin(Time.time * speed) + 1f) / 1;
        newColor.b = (Mathf.Sin(Time.time * speed) + 1f) / 1;
        newColor.a = 1.0f; // (Mathf.Sin(Time.time * speed) + 1f) / 1;

        GetComponent<Renderer>().material.color = newColor;


    }
}