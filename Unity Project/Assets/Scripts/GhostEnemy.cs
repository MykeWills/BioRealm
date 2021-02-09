using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GhostEnemy : MonoBehaviour
{
    public Transform PlayerPosition;
    public Vector3 normalPosition;
    //public BoxCollider normalBoxCollider;
    
    public float speed;
    public float falling = 3.0f;
    public int Health = 20;

    public Camera targetCamera;
    public AudioSource AudioSrc;
    public AudioClip monsterDeath;
    public AudioClip monsterHit;
    //public GameObject Territory;
   
    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;


    //public GameObject EnemyBlood;

    [SerializeField]
    //private float fadePerSecond = 0.5f;

    // Use this for initialization
    void Start()
    {
        normalPosition = gameObject.transform.position;
        PlayerPosition = Camera.main.transform;
        enemyFlashTimerStart = enemyFlashTimer;
        Health = 20;
        enemyFlashEnabled = false;
        //normalBoxCollider.isTrigger = true;
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
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            }
        }
    
        if (Health <= 0){

            AudioSrc.clip = monsterDeath;
            AudioSrc.Play();
            /*var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));*/

            //Territory.SetActive(false);
            //normalBoxCollider.enabled = false;
            enemyFlashEnabled = false;
            gameObject.SetActive(false);
        }

        if (enemyFlashEnabled == true)
        {
            //EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        }
        else if (enemyFlashEnabled == false)
        {
            //EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0)
        {

            //EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;

        }
        
    }

    public void MoveToPlayer()
    {

        //normalBoxCollider.isTrigger = false;
        //normalBoxCollider.enabled = false;
        speed = 20;
        transform.LookAt(PlayerPosition.position);
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

    }
    public void MoveToNormal()
    {
        faceTarget = false;
        //normalBoxCollider.enabled = true;
        //normalBoxCollider.isTrigger = true;
        transform.LookAt(normalPosition);
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

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
            enemyFlashEnabled = true;
            AudioSrc.clip = monsterHit;
            AudioSrc.Play();
            Health = Health - 1;
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            AudioSrc.clip = monsterHit;
            AudioSrc.Play();
            Health = Health - 5;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            AudioSrc.clip = monsterHit;
            AudioSrc.Play();
            Health = Health - 3;
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            AudioSrc.clip = monsterHit;
            AudioSrc.Play();
            Health = Health - 15;
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            AudioSrc.clip = monsterHit;
            AudioSrc.Play();
            Health = Health - 15;
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Normal"))
        {
            faceTarget = true;
            speed = 0;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Normal"))
        {
            faceTarget = false;
            speed = 20;
        }
    }
}

