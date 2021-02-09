using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurretEnemy : MonoBehaviour
{
    public Transform playerPosition;
    public Transform centerPosition;

    public GameObject TurretTerritory;
    public GameObject RocketTerritory;

    public float speed;
    public float JumpForce;
    public int Health;
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;

    public Camera targetCamera;
    public AudioSource monsterDeath;
    public AudioSource monsterHit;

    public MeshCollider TurretCollider;
    public MeshRenderer TurretMesh;

    public bool faceTarget;

    private Vector3 distance;


    [SerializeField]
    //private float fadePerSecond = 0.5f;

    TurretAutoFire TurretFire;
    TurretEnemy turret;
    

    // Use this for initialization
    void Start()
    {
        TurretFire = GetComponent<TurretAutoFire>();
        turret = GetComponent<TurretEnemy>();
        
        Health = 20;

        turret.enabled = false;
        TurretFire.enabled = false;
        

        faceTarget = true;



    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition != null)
        {
            if (faceTarget == true)
            {
                transform.LookAt(playerPosition);
                transform.Rotate(new Vector3(0, 0, 0), Space.Self);
            }
        }

        if (Health <= 0) {

            monsterDeath.Play();

            /*var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));*/

            turret.enabled = false;
            TurretFire.enabled = false;
            TurretCollider.enabled = false;
            Destroy(TurretTerritory);
            Destroy(RocketTerritory);
            gameObject.SetActive(false);
        }
    }

    public void MoveToPlayer()
    {
        turret.enabled = true;
        TurretFire.enabled = true;
        transform.LookAt(playerPosition.position);
        transform.Translate(Vector3.up * JumpForce * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, 0), Space.Self);

    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("IceShot"))
        {
            Destroy(other.gameObject);
            monsterHit.Play();
            Health -= 1;
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
            monsterHit.Play();
            Health -= 5;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
            monsterHit.Play();
            Health -= 3;
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
    public void MeshRotation()
    {
        transform.LookAt(playerPosition.position);
        transform.Rotate(new Vector3(0, 0, 0), Space.Self);
        distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
        transform.position = centerPosition.position + distance;
    }
   
}

