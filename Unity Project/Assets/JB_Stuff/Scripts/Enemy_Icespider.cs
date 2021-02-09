using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Icespider : MonoBehaviour {
    [HideInInspector]
    public Transform playerPosition;
    [HideInInspector]
    public Vector3 centerPosition; //Get the spiders default starting position.

    //public GameObject TheTerritory;
    //public GameObject TheBallTerritory;
    //public BoxCollider SpiderTerritory;
    //public BoxCollider SpiderballTerritory; // Code is now unnessecary

    //public Text BossHealthText;

    public float speed;
    public float JumpForce;
    public int Health; //sets default health variable in the inspector
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;

    public float jumpTimer;

    //public Camera targetCamera; unused variable
    public AudioSource Hit;
    public AudioSource Shoot;
    public AudioSource Death;



    //public AudioSource monsterHit;

    //public CapsuleCollider SpiderCollider; //Unused variable
    //public MeshRenderer SpiderMesh; //Unused variable

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;
    public bool enemyJumping;

    private Vector3 distance;

    //public GameObject EnemyBlood;

    [SerializeField]
    private float fadePerSecond = 0.5f;

    //SpiderAutoFire spiderFire;
    //SpiderEnemy Spider;
    //Unnessecary

        //Combining the autofire script
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force = 1000;
    public float shotDelay = 0.1f;
    private float lastShot;


    public float attackRange = 10; //Range in which the enemy will attack the player.

    // Use this for initialization
    void Start() {
        //spiderFire = GetComponent<SpiderAutoFire>();
        //Spider = GetComponent<SpiderEnemy>(); //Unused variable

        enemyFlashTimerStart = enemyFlashTimer;
        //Health = 20; health variable set manually in the inspector.

        playerPosition = Camera.main.transform; //Set playerposition to be the camera.

        enemyFlashEnabled = false;
        //Spider.enabled = false;
        //spiderFire.enabled = false;

        centerPosition = transform.position;

        faceTarget = true;
        enemyJumping = false;



    }
    void shootBullet() {
        Shoot.Play();
        GameObject Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
        //You can decalre the handler and instantiate on the same line.

        //Temporary_Bullet_Handler.transform.Rotate(Vector3.forward * 90);
        /*Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(transform.forward * -Bullet_Forward_Force);*/
        //---------//
        //Unnessecary, we can get the rigid body without having to declare a new variable.

        Temporary_Bullet_Handler.transform.LookAt(Camera.main.transform); //tell the new bullet to look at the player.
        Temporary_Bullet_Handler.GetComponent<Rigidbody>().AddForce(Bullet_Forward_Force * Temporary_Bullet_Handler.transform.forward); //Add force relative to the bullet, as we cannot use the enemy itself anymore due to the y-only rotation.
        Destroy(Temporary_Bullet_Handler, 10.0f);
        lastShot = Time.time; //Set the time the last shot was fire, this is more reliable then time.deltatime.
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Magnitude(transform.position - playerPosition.position) < attackRange) {
            
            if (enemyJumping == false) {
                enemyJumping = true;
            }
            if (Time.time > lastShot + shotDelay) {
                shootBullet();
            }
        }else {
            if (enemyJumping == true) {
                enemyJumping = false;
            }
        }
        //Only jump when the enemy is within the attack range.

       if (playerPosition != null) {
            if (faceTarget == true) {
                transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center); //Look at the center of the player
                Bullet_Emitter.transform.LookAt(Camera.main.transform); // Make the bullet emitter look at the player.
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0); //Zero out the X and Z rotation, so the enemy doesn't tilt up and down.
            }
        }

        if (Health <= 0) {
            Death.Play();

            //-------------------------------------------------DEPRECATED-------------------------------------------------//
            //enemyFlashEnabled = false;
            //Spider.enabled = false;
            //spiderFire.enabled = false;
            //SpiderCollider.enabled = false;
            //enemyJumping = false;
            //Destroy(TheTerritory);
            //Destroy(TheBallTerritory);
            //-------------------------------------------------DEPRECATED-------------------------------------------------//

            //We're deactivating the gameObject entirely, so we don't need to disable the scripts.
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }

        if (enemyFlashEnabled == true) {
            //EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        } else if (enemyFlashEnabled == false) {
            //EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0) {


            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;

        }
        if (enemyJumping == true) {
            transform.Translate(Vector3.up * JumpForce * Time.deltaTime, Space.World);
        }
    }

    public void dealDamage(int damage, Collision other) { //Combine all the oncollision enter else/if statements into one, basically.
        Destroy(other.gameObject);
        enemyFlashEnabled = true;
        Hit.Play();
        Health -= damage;
    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("IceShot")) {
            dealDamage(1, other);
        } else if (other.gameObject.CompareTag("ElectronBeam")) {
            dealDamage(10, other);
        } else if (other.gameObject.CompareTag("ElectronBall")) {
            dealDamage(5, other);
        } else if (other.gameObject.CompareTag("MagmaGrenade")) {
            dealDamage(100, other);
        } else if (other.gameObject.CompareTag("MagmaShell")) {
            dealDamage(5, other);
        } else if (other.gameObject.CompareTag("Terrain")) {
            jumpTimer = 0;

        }

    }

}

