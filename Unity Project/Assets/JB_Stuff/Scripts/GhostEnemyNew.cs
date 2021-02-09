using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GhostEnemyNew : MonoBehaviour {

    public Transform PlayerPosition;
    [HideInInspector]
    public Vector3 normalPosition;
    public float speed;
    public int Health;

    public int iceDamage = 1;
    public int elecBallDamage = 5;
    public int elecBeamDamage = 10;
    public int magDamage = 5;
    public int magGrenadeDamage = 100;

    public int damage;
    public MainPlayer.Element element;

    public float awarenessRange;

    public AudioSource Hit;
    public AudioSource Death;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;

    public ParticleSystem blood;

    public GameObject EnemyBlood;

    [SerializeField]

    void Start() {
        normalPosition = gameObject.transform.position;
        PlayerPosition = Camera.main.gameObject.transform;
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
        enemyFlashTimerStart = enemyFlashTimer;
        enemyFlashEnabled = false;
        faceTarget = true;
    }
    void Update() {
        if (PlayerPosition != null && Vector3.Magnitude(normalPosition - PlayerPosition.position) < awarenessRange) {
            MoveToPlayer();
        }
        if (Health <= 0) {
            Death.Play();
            enemyFlashEnabled = false;
            Destroy(gameObject, 0.5f);
        }

        if (enemyFlashEnabled == true) {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;
        } else if (enemyFlashEnabled == false) {
            EnemyBlood.SetActive(false);
        }

        if (enemyFlashTimer <= 0) {
            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;
        }
    }
    public void MoveToPlayer() {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(PlayerPosition.transform.position);
    }
    public void MoveToNormal() {

    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            MainPlayer.Instance().DoDamage(damage,element);
        }
    }

    public void DealDamage(int damage) {
        enemyFlashEnabled = true;
        Hit.Play();
        blood.Emit(30);
        Health -= damage;
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

