using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantEnemy : MonoBehaviour
{
    public Transform playerPosition;
    //public Transform centerPosition;

    //public GameObject TheTerritory;
    //public GameObject TheBallTerritory;
    //public BoxCollider PlantTerritory;
    //public BoxCollider PlantballTerritory;

    //public Text BossHealthText;

    public Transform alternateTurret = null;

    public float speed;
    public int Health;
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;

    public Camera targetCamera;

    public CapsuleCollider PlantCollider;
    public SkinnedMeshRenderer PlantMesh;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;

    public AudioSource Hit;
    public AudioSource Death;
    public AudioSource Shoot;

    private Vector3 distance;

    public GameObject EnemyBlood;

    [SerializeField]
    //private float fadePerSecond = 0.5f;

    PlantAutoFire plantFire;
    

    // Use this for initialization
    void Start()
    {
        plantFire = GetComponent<PlantAutoFire>();
        playerPosition = Camera.main.transform;
        enemyFlashTimerStart = enemyFlashTimer;
        

        enemyFlashEnabled = false;
        //Plant.enabled = false;
        //plantFire.enabled = false;
        
        faceTarget = true;
        



    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerPosition != null)
        {
            if (faceTarget == true)
            {
                if (alternateTurret == null) {
                    transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
                    //transform.Rotate(new Vector3(0, 0, 0), Space.Self);
                } else {
                    alternateTurret.transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
                }
            }
        }

        if (Health <= 0) {
            Death.Play();
            enemyFlashEnabled = false;
            plantFire.enabled = false;
            PlantCollider.enabled = false;
            Destroy(gameObject);
        }

        if (enemyFlashEnabled == true)
        {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        }

        else if (enemyFlashEnabled == false)
        {
            EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0)
        {

            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;

        }
        
    }

    void OnCollisionEnter(Collision other)
    {

        
        if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            Hit.Play();
            Health = Health - 10;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            Hit.Play();
            Health = Health - 5;
        }
        else if (other.gameObject.CompareTag("IceShot")) {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            Hit.Play();
            Health = Health - 1;
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            Hit.Play();
            Health = Health - 5;
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            Hit.Play();
            Health = Health - 100;
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

