using UnityEngine;
using System.Collections;

public class VolcanoTriggerWallMoving : MonoBehaviour
{
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public float DoorHeightMin;

    public bool inTerritory;

    public AudioSource DoorOpenSound;
 
    public GameObject DoorObject;

    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        inTerritory = false;
    }

    void Update()
    {
        if (inTerritory)
        {
            // move door up
            DoorObject.transform.Translate(Vector3.up * Time.deltaTime * DoorOpenSpeed);

            // clamp door
            if (DoorObject.transform.position.y > DoorheightMax)
            {
                Vector3 newPosition = DoorObject.transform.position;
                newPosition.y = DoorheightMax;
                DoorObject.transform.position = newPosition;
            }
        }

        else if (!inTerritory)
        {
            DoorObject.transform.Translate(Vector3.down * Time.deltaTime * DoorOpenSpeed);

            // clamp door
            if (DoorObject.transform.position.y < DoorHeightMin)
            {
                Vector3 newPosition = DoorObject.transform.position;
                newPosition.y = DoorHeightMin;
                DoorObject.transform.position = newPosition;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject == Player)
        {
            DoorOpenSound.Play();
            inTerritory = true;
        }

    }
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == Player)
        {
            DoorOpenSound.Play();
            inTerritory = false;
        }

    }

}

