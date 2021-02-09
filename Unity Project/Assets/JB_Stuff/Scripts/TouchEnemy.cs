using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchEnemy : MonoBehaviour {

    public Transform PlayerPosition;
    [HideInInspector]
    public Vector3 normalPosition; //Don't edit, this is the enemies start position.
    public float awarenessRange; // how close the player has to be to the enemy starting point for it to chase.
    public float speed; // how fast it moves.
    public int Health; // how much health.

    public int damage; //how much damage it does on touch
    //public MainPlayer.Element element; //what element the attack is, currently disabled because it requires the updated player controller.

    public AudioSource audioSrc;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip encounter;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    //public bool faceTarget;

    //initalize enemy
    void Start() {
        normalPosition = gameObject.transform.position;
        PlayerPosition = Camera.main.gameObject.transform;
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
        enemyFlashTimerStart = enemyFlashTimer;
        enemyFlashEnabled = false;
        //faceTarget = true;
    }
    void Update() {
        if (PlayerPosition != null) {
            if (Vector3.Magnitude(PlayerPosition.position - normalPosition) <= awarenessRange && awarenessRange != 0) {
                gameObject.GetComponent<NavMeshAgent>().SetDestination(PlayerPosition.transform.position); // chase player on navmesh
            } else {
                gameObject.GetComponent<NavMeshAgent>().SetDestination(normalPosition); // go home
            }
        }
        if (Health <= 0) {
            GameObject deathSound = new GameObject();
            deathSound.transform.position = gameObject.transform.position;
            deathSound.AddComponent<AudioSource>();
            deathSound.GetComponent<AudioSource>().playOnAwake = true;
            deathSound.GetComponent<AudioSource>().clip = death;
            Destroy(deathSound, 2);
            enemyFlashEnabled = false;
            Destroy(gameObject, 0.5f);
        }

        if (enemyFlashEnabled == true) {
            enemyFlashTimer -= Time.deltaTime;
        } else if (enemyFlashEnabled == false) {
        }

        if (enemyFlashTimer <= 0) {
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            //MainPlayer.Instance().DoDamage(damage, element); // deal damage to the player.
        }
    }
    public void DealDamage(int damage) {
        enemyFlashEnabled = true;
        audioSrc.clip = hit;
        audioSrc.Play();
        Health -= damage;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("ElectronBeam")) {
            Destroy(other.gameObject);
            DealDamage(2);
        } else if (other.gameObject.CompareTag("ElectronBall")) {
            Destroy(other.gameObject);
            DealDamage(5);
        } else if (other.gameObject.CompareTag("IceShot")) {
            Destroy(other.gameObject);
            DealDamage(5);
        }
    }
}

