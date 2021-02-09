using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IceDragon : MonoBehaviour
{
    
    public BoxCollider BossTerritory;
    public BoxCollider IceballTerritory;

    public Transform Player;
    public Transform center;

    public Text BossHealthText;
    public bool EndBoss = false;

    public int Health;
    public float degreesPerSecond = -65.0f;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    public float falling = 3.0f;
    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    private float fadePerSecond = 2.5f;

    public GameObject DragonWings;
    public GameObject DragonArms;
    public GameObject DragonWingClaw;
    public GameObject DragonHead;
    public GameObject DragonLower;
    public GameObject DragonUpper;
    public GameObject Boss;

    public CapsuleCollider DragonCollider;
    public MeshRenderer DragonMesh;

    public Camera targetCamera;

    public AudioSource monsterDeath;
    public AudioSource monsterHit;
    public AudioSource BossMusic;
    public AudioSource levelMusic;
    public AudioSource RuneAura;


    public float shakeDuration;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public AudioSource ThunderSound;
    public AudioSource QuakeSound;
    public GameObject RuneAppear;
    public GameObject EnemyBlood;

    private Vector3 distance;

    public bool enemyReady;
    public bool enemyFlashEnabled = false;
    public bool faceTarget;

    IceDragonBallAutoFire iceDragonball;
    IceDragon iceDragon;
    IceDragonRotation iceDragonRotation;
   

    [SerializeField]

    public Transform camTransform;
    Vector3 originalPos;


    // Use this for initialization
    void Start()
    {
        faceTarget = true;
        iceDragonball = GetComponent<IceDragonBallAutoFire>();
        iceDragon = GetComponent<IceDragon>();
        iceDragonRotation = GetComponent<IceDragonRotation>();
        iceDragonball.enabled = false;
        iceDragon.enabled = false;
        enemyFlashTimerStart = enemyFlashTimer;
        enemyReady = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            if (faceTarget == true)
            {
                    transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
            }
        }
        if (Health <= 0)
        {
            
            enemyFlashEnabled = false;

            var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            transform.Translate(Vector3.forward * falling * Time.deltaTime);

            if (DragonWings) {
                DragonWings.SetActive(false);
                DragonArms.SetActive(false);
                DragonWingClaw.SetActive(false);
                DragonHead.SetActive(false);
                DragonLower.SetActive(false);
                DragonUpper.SetActive(false);
                DragonCollider.enabled = false;
                DragonMesh.enabled = false;
            }

            RuneAura.Play();
            iceDragonball.enabled = false;
            iceDragonRotation.enabled = false;
            EnemyBlood.SetActive(false);
            
            enemyReady = false;
            RuneAppear.SetActive(true);
            BossMusic.Stop();
           
            MainPlayer.Instance().BossHealthText.text = "";

           
        }
        if (enemyFlashEnabled == true)
        {
            EnemyBlood.SetActive(true);
            enemyFlashTimer -= Time.deltaTime;
        }
        if (enemyFlashTimer <= 0)
        {
            EnemyBlood.SetActive(false);
            enemyFlashEnabled = false;
            enemyFlashTimer = enemyFlashTimerStart;
        }
        if (shakeDuration > 0)
        {

            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }

        else if (shakeDuration < 0)
        {
            Destroy(Boss);
            QuakeSound.Stop();
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            
        }

    }

    public void MeshRotation()
    {
        enemyReady = true;
        iceDragonball.enabled = true;
        iceDragon.enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("IceShot") && enemyReady == true)
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 1;
            SetCountHealth();
            if (Health <= 0)
            {
                shakeDuration += 5;
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();

            }
        }
        else if (other.gameObject.CompareTag("ElectronBeam") && enemyReady == true)
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 10;
            SetCountHealth();
            if (Health <= 0)
            {
                shakeDuration += 5;
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                levelMusic.Play();

            }
        }
        else if (other.gameObject.CompareTag("ElectronBall") && enemyReady == true)
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 5;
            SetCountHealth();
            if (Health <= 0)
            {
                shakeDuration += 5;
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                levelMusic.Play();

            }
        }
        else if (other.gameObject.CompareTag("MagmaShell") && enemyReady == true)
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 5;
            SetCountHealth();
            if (Health <= 0)
            {
                shakeDuration += 5;
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                levelMusic.Play();


            }
        }
        else if (other.gameObject.CompareTag("MagmaGrenade") && enemyReady == true)
        {
            enemyFlashEnabled = true;
            monsterHit.Play();
            Health -= 100;
            SetCountHealth();
            if (Health <= 0)
            {
                shakeDuration += 5;
                monsterDeath.Play();
                ThunderSound.Play();
                QuakeSound.Play();
                levelMusic.Play();

            }
        }
    }

    void SetCountHealth()
    {
        if(EndBoss == true)
        {
            MainPlayer.Instance().BossHealthText.text = "Magma Dragon " + Health.ToString();
        }
        else
        {
            MainPlayer.Instance().BossHealthText.text = "Ice Dragon " + Health.ToString();
        }
        
    }

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

}

