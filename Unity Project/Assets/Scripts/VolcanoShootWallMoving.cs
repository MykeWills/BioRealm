using UnityEngine;
using System.Collections;

public class VolcanoShootWallMoving : MonoBehaviour
{
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public float DoorHeightMin;
    public int DoorShots;
    public float DoorCloseTimer;
    public float DoorTimer;
    private int Shots;

    public AudioSource ObjectDisabledSound;
    public AudioSource DoorOpenSound;
 
    public GameObject DoorObject;
    public GameObject ShootingObject;
    public MeshRenderer ShootingObjectMeshRender;

    private void Start()
    {
        Shots = 0;
        DoorTimer = DoorCloseTimer;
    }

    private void Update()
    {
       
        
        if (Shots >= DoorShots)
        {
            Shots = DoorShots;
            // move door up
            DoorObject.transform.Translate(Vector3.up * Time.deltaTime * DoorOpenSpeed);

            // clamp door
            if (DoorObject.transform.position.y > DoorheightMax)
            {
               
                DoorTimer -= Time.deltaTime;
                
                Vector3 newPosition = DoorObject.transform.position;
                newPosition.y = DoorheightMax;
                DoorObject.transform.position = newPosition;
                


            }
        }
        if (DoorTimer < 0)
        {
            DoorOpenSound.Play();
            DoorTimer = 0;
            Shots = 0;
        }
        if (Shots == 0 && DoorTimer == 0)
        {

            transform.Translate(Vector3.down * Time.deltaTime * DoorOpenSpeed);

            // clamp door
            if (transform.position.y < DoorHeightMin)
            {

                Vector3 newPosition = transform.position;
                newPosition.y = DoorHeightMin;

                transform.position = newPosition;
                //ShootingObjectMeshRender.enabled = true;
                DoorTimer = DoorCloseTimer;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Shoot Object with ElectronBeam

        if (collision.gameObject.CompareTag("ElectronBeam") || collision.gameObject.CompareTag("ElectronBall"))
        {
            Shots += 1;
            if (Shots ==1)
            {

                DoorOpenSound.Play();
            }
           
            
            

        }

        // Shoot Object with Arctic Proto Plasm

        if (collision.gameObject.CompareTag("IceShot"))
        {
            Shots += 1;
            if (Shots == 1)
            {
                DoorOpenSound.Play();
            }
        }

        // Shoot Object with Magma Flak Cannon

        if (collision.gameObject.CompareTag("MagmaShell") || collision.gameObject.CompareTag("MagmaGrenade"))
        {
            Shots += 1;
            if (Shots >= DoorShots)
            {
                DoorOpenSound.Play();
            }
        }
    }

}

