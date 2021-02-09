using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_FlyingTurret : MonoBehaviour {
    [HideInInspector]
    public Vector3 PlayerPosition;
    [HideInInspector]
    public Vector3 normalPosition;
    //public BoxCollider normalBoxCollider;

    public float speed;
    public float falling = 3.0f;
    public int Health;

    public float awarenessRange;
    public float attackRange;

    public Camera targetCamera;
    public AudioSource Hit;
    public AudioSource Shoot;
    public AudioSource Death;

    public int iceDamage = 5;
    public int elecBallDamage = 5;
    public int elecBeamDamage = 10;
    public int magDamage = 5;
    public int magGrenadeDamage = 100;

    public Transform[] bulletSpawners;
    public GameObject projectile;
    public float projectileForce;
    [HideInInspector]
    public float lastShot;
    public float fireRate = 1;
    //public GameObject Territory;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;


    //public GameObject EnemyBlood;

    [SerializeField]
    //private float fadePerSecond = 0.5f;
    public void DealDamage(int damage) {
        enemyFlashEnabled = true;
        Hit.Play();
        //blood.Emit(30);
        Health -= damage;
    }
    // Use this for initialization
    void Start() {
        normalPosition = gameObject.transform.position;
        enemyFlashTimerStart = enemyFlashTimer;
      
        enemyFlashEnabled = false;
        //normalBoxCollider.isTrigger = true;
        faceTarget = true;
    }
    public void shoot() {
        for (int i = 0; i < bulletSpawners.Length; i++) {
            GameObject newBullet= Instantiate(projectile, bulletSpawners[i].position, bulletSpawners[i].rotation) as GameObject;
            newBullet.transform.LookAt(PlayerPosition);
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
            Destroy(newBullet, 10.0f);
        }
        Shoot.Play();
        lastShot = Time.time;
    }
    // Update is called once per frame
    void Update() {
        projectile.transform.rotate(Vector3.left * 90);
        PlayerPosition = Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center;
        if (Vector3.Magnitude(transform.position - PlayerPosition) < attackRange && Time.time > lastShot+fireRate) {
            shoot();
        }
        if (PlayerPosition != null) {
            if (faceTarget == true) {
                //transform.LookAt(PlayerPosition);
                //transform.Rotate(new Vector3(-90, -90, 0), Space.Self);
            }
        }

        if(Vector3.Magnitude(normalPosition - PlayerPosition) < awarenessRange) {
            MoveToPlayer();
        }else {
            if (Vector3.Magnitude(normalPosition - transform.position) > 0.1) {
                MoveToNormal();
            }
        }

        if (Health <= 0) {

            Death.Play();
            /*var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));*/

            //Territory.SetActive(false);
            //normalBoxCollider.enabled = false;
            enemyFlashEnabled = false;
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (enemyFlashEnabled == true) {
            //EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        } else if (enemyFlashEnabled == false) {
            //EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0) {

            //EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;

        }

    }

    public void MoveToPlayer() {
        speed = 20;
        //transform.LookAt(PlayerPosition);
        var lookRot = Quaternion.LookRotation(PlayerPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        transform.position += transform.forward * speed * Time.deltaTime;
        

    }
    public void MoveToNormal() {
        faceTarget = false;
        var lookRot = Quaternion.LookRotation(normalPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        transform.position += transform.forward * speed * Time.deltaTime;
        

    }


    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("ElectronBeam")) {
            Destroy(other.gameObject);
            DealDamage(elecBeamDamage);
        } else if (other.gameObject.CompareTag("ElectronBall")) {
            Destroy(other.gameObject);
            DealDamage(elecBallDamage);
        } else if (other.gameObject.CompareTag("IceShot")) {
            Destroy(other.gameObject);
            DealDamage(iceDamage);
        } else if (other.gameObject.CompareTag("MagmaShell")) {
            Destroy(other.gameObject);
            DealDamage(magDamage);
        } else if (other.gameObject.CompareTag("MagmaGrenade")) {
            Destroy(other.gameObject);
            DealDamage(magGrenadeDamage);
        }
    }
}

