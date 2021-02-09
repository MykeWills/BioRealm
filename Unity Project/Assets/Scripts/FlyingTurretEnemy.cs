using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlyingTurretEnemy : MonoBehaviour
{
    public Transform PlayerPosition;
    public Transform normalPosition;
    public BoxCollider normalBoxCollider;
    
    public float speed;
    public float falling = 3.0f;
    public int Health;

    public Camera targetCamera;
    public AudioSource monsterDeath;
    public AudioSource monsterHit;
    public GameObject Territory;

    public bool faceTarget;

    FlyingTurretAutoFire TurretFire;
    FlyingTurretEnemy turret;




    [SerializeField]
    //private float fadePerSecond = 0.5f;

    // Use this for initialization
    void Start()
    {
        TurretFire = GetComponent<FlyingTurretAutoFire>();
        turret = GetComponent<FlyingTurretEnemy>();
        Health = 20;

        normalBoxCollider.isTrigger = true;
        turret.enabled = false;
        TurretFire.enabled = false;

        faceTarget = true;



    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPosition != null)
        {
            if (faceTarget == true)
            {
                transform.LookAt(PlayerPosition);
                transform.Rotate(new Vector3(-90, 0, -90), Space.Self);
            }
        }
    
        if (Health <= 0){

            monsterDeath.Play();
    
            /*var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));*/

            Territory.SetActive(false);
            normalBoxCollider.enabled = false;
            gameObject.SetActive(false);
        }

    }

    public void MoveToPlayer()
    {
        turret.enabled = true;
        TurretFire.enabled = true;
        normalBoxCollider.isTrigger = false;
        normalBoxCollider.enabled = false;
        speed = 3;
        transform.LookAt(PlayerPosition.position);
        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.Rotate(new Vector3(, 0, 0), Space.Self);

    }
    public void MoveToNormal()
    {
        normalBoxCollider.enabled = true;
        normalBoxCollider.isTrigger = true;
        transform.LookAt(normalPosition.position);
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(-90, 0, 90), Space.Self);

    }
   

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            MoveToNormal();
        }
        if (other.gameObject.CompareTag("IceShot"))
        {
            Destroy(other.gameObject);
     
            monsterHit.Play();
            Health = Health - 1;
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
    
            monsterHit.Play();
            Health = Health - 5;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
  
            monsterHit.Play();
            Health = Health - 3;
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            Destroy(other.gameObject);
  
            monsterHit.Play();
            Health = Health - 100;
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            Destroy(other.gameObject);

            monsterHit.Play();
            Health = Health - 25;
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Normal"))
        {
            turret.enabled = false;
            TurretFire.enabled = false;
            faceTarget = true;
            speed = 0;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Normal"))
        {
            faceTarget = false;
            speed = 3;
        }
    }
}

