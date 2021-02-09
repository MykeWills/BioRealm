using UnityEngine;
using System.Collections;

public class ItemSinWave : MonoBehaviour {

    public float speed;
    public float AddAboveZero;
    public float UpAndDownAmount;
    public Vector3 rotations = new Vector3(0,90,0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPosition;
        newPosition = Vector3.zero;
        newPosition.y = (Mathf.Sin(Time.time * speed) + AddAboveZero) / UpAndDownAmount;
        //transform.Rotate(new Vector3(rotations.x, rotations.y, rotations.z) * Time.deltaTime);
        transform.localPosition = newPosition;
	
	}
}
