using UnityEngine;
using System.Collections;

public class FlyingTurretAutoFire : MonoBehaviour
{
    public Transform target;
    public GameObject First_Bullet_Emitter;
    public GameObject Bullet;
    public float First_Bullet_Forward_Force;
    public float FirstshotTime;

    public GameObject Second_Bullet_Emitter;
    public float Second_Bullet_Forward_Force;
    public float SecondtshotTime;

    public AudioSource Shoot;

    void Start()
    {

    }

    void Update()
    {
        FirstshotTime -= Time.deltaTime;

        if (FirstshotTime <= 0)
        {

            Shoot.Play();
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, First_Bullet_Emitter.transform.position, First_Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler.transform.LookAt(target);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.right * First_Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler, 10.0f);
            FirstshotTime += 3.33f;
        }

        SecondtshotTime -= Time.deltaTime;

        if (SecondtshotTime <= 0)
        {
            
            Shoot.Play();
            GameObject Temporary_Bullet_Handler2;
            Temporary_Bullet_Handler2 = Instantiate(Bullet, Second_Bullet_Emitter.transform.position, Second_Bullet_Emitter.transform.rotation) as GameObject;
            Temporary_Bullet_Handler2.transform.LookAt(target);
            Rigidbody Temporary_RigidBody2;
            Temporary_RigidBody2 = Temporary_Bullet_Handler2.GetComponent<Rigidbody>();
            Temporary_RigidBody2.AddForce(transform.right * Second_Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler2, 10.0f);
            SecondtshotTime += 3.33f;
        }
    }
    }