using UnityEngine;
using System.Collections;

public class wallMove : MonoBehaviour
{
    float speed = 10.0f; // units / second
    //float heightMin = 180f;
    float heightMax = 188f;
    public int Power;
    public AudioSource PowerDown;
    public AudioSource DoorOpenSound;
    public GameObject PowerPole;
    public GameObject Door;
    public MeshRenderer Mesh;

    private void Start()
    {
        //PowerPole.SetActive(true);
        Power = 0;
    }

    private void Update()
    {
        if (Power >= 5)
        {
           
          
            Power = 5;
            // move door up
            Door.transform.Translate(Vector3.up * Time.deltaTime * speed);

            // clamp door
            if (Door.transform.position.y > heightMax)
            {
                // too far up... do something about it
                Vector3 newPosition = Door.transform.position;
                newPosition.y = heightMax;
                Door.transform.position = newPosition;
                DoorOpenSound.Play();
                PowerPole.SetActive(false);
            }
        }
        /*else if (Power == 0)
        {

            // move door down
            transform.Translate(Vector3.down * Time.deltaTime * speed);

            // clamp door
            if (transform.position.y < heightMin)
            {
                // too far up... do soemthig about it
                Vector3 newPosition = transform.position;
                newPosition.y = heightMin;

                transform.position = newPosition;

            }

        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ElectronBeam") || collision.gameObject.CompareTag("ElectronBall"))
        {
            if (Power >= 5)
            {
                Mesh.enabled = false;
                PowerDown.Play();
                
            }

            Power+=1;

        }
    }

}

