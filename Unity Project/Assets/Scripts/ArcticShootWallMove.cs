using UnityEngine;
using System.Collections;

public class ArcticShootWallMove : MonoBehaviour
{
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public int DoorShots;
    private int Shots;

    public AudioSource ObjectDisabledSound;
    public AudioSource DoorOpenSound;
 
    public GameObject DoorObject;
    public GameObject ShootingObject;
    public MeshRenderer ShootingObjectMeshRender;

    private void Start()
    {
        Shots = 0;
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

                Vector3 newPosition = DoorObject.transform.position;
                newPosition.y = DoorheightMax;
                DoorObject.transform.position = newPosition;
                DoorOpenSound.Play();
                ShootingObject.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Shoot Object with ElectronBeam

        if (collision.gameObject.CompareTag("ElectronBeam") || collision.gameObject.CompareTag("ElectronBall"))
        {
            if (Shots >= DoorShots)
            {
                ShootingObjectMeshRender.enabled = false;
                ObjectDisabledSound.Play();

            }

            Shots += 1;

        }

        // Shoot Object with Arctic Proto Plasm

        //if (collision.gameObject.CompareTag("IceShot"))
        //{
        //    if (Shots >= DoorShots)
        //    {
        //        ShootingObjectMeshRender.enabled = false;
        //        ObjectDisabledSound.Play();

        //    }

        //    Shots += 1;

        //}

        // Shoot Object with Magma Flak Cannon

        //if (collision.gameObject.CompareTag("MagmaShell") || collision.gameObject.CompareTag("MagmaGrenade"))
        //{
        //    if (Shots >= DoorShots)
        //    {
        //        ShootingObjectMeshRender.enabled = false;
        //        ObjectDisabledSound.Play();

        //    }

        //    Shots += 1;

        //}
    }

}

