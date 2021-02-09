using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpiderEnemy : MonoBehaviour
{
    public Transform playerPosition;
    public Transform centerPosition;

    public GameObject TheTerritory;
    public GameObject TheBallTerritory;
    public BoxCollider SpiderTerritory;
    public BoxCollider SpiderballTerritory;

    //public Text BossHealthText;

    public float speed;
    public float JumpForce;
    public int Health;
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;

    public float jumpTimer;

    public Camera targetCamera;
    public AudioSource monsterDeath;
    public AudioSource monsterHit;

    public CapsuleCollider SpiderCollider;
    public MeshRenderer SpiderMesh;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;
    public bool enemyJumping;

    private Vector3 distance;

    public GameObject EnemyBlood;

    [SerializeField]
    //private float fadePerSecond = 0.5f;

    SpiderAutoFire spiderFire;
    SpiderEnemy Spider;
    

    // Use this for initialization
    void Start()
    {
        spiderFire = GetComponent<SpiderAutoFire>();
        Spider = GetComponent<SpiderEnemy>();
        
        enemyFlashTimerStart = enemyFlashTimer;
        Health = 20;

        enemyFlashEnabled = false;
        Spider.enabled = false;
        spiderFire.enabled = false;
        

        faceTarget = true;
        enemyJumping = false;



    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.name);
        if (playerPosition != null)
        {
            if (faceTarget == true)
            {
                transform.LookAt(playerPosition);
                transform.Rotate(new Vector3(-90, 180, 0), Space.Self);
            }
        }

        if (Health <= 0) {

            monsterDeath.Play();

            /*var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));*/

            enemyFlashEnabled = false;
            Spider.enabled = false;
            spiderFire.enabled = false;
            SpiderCollider.enabled = false;
            

            enemyJumping = false;
            Destroy(TheTerritory);
            Destroy(TheBallTerritory);
            gameObject.SetActive(false);
        }

        if (enemyFlashEnabled == true)
        {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;

        }

        else if (enemyFlashEnabled == false)
        {
            //EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0)
        {

            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;

        }
        if (enemyJumping == true)
        {
            transform.LookAt(playerPosition.position);
            transform.Translate(Vector3.up * JumpForce * Time.deltaTime, Space.World);
            transform.Rotate(new Vector3(0, 180, 0), Space.Self);
            
        }
    }

    public void MoveToPlayer()
    {
        Spider.enabled = true;
        spiderFire.enabled = true;
        
        enemyJumping = true;
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("IceShot"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 1;
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 5;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 3;
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 100;
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            Destroy(other.gameObject);
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health = Health - 25;
        }
        else if (other.gameObject.CompareTag("Terrain"))
        {
            jumpTimer = 0;

        }

    }
    public void MeshRotation()
    {
        transform.LookAt(playerPosition.position);
        transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = centerPosition.position + distance;
    }
   
}

