using UnityEngine;
using System.Collections;

public class SpiderAutoFire : MonoBehaviour
{
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    public float shotTime;
    public AudioSource Shoot;

    void Start()
    {
           
    }

    void Update()
    {
        shotTime -= Time.deltaTime;

        if(shotTime <= 0)
        {
            Shoot.Play();
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            //Temporary_Bullet_Handler.transform.Rotate(Vector3.forward * 90);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * -Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler, 10.0f);
            shotTime += 3.33f;
        }
    }
}