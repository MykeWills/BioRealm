using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SerpentEnemy : MonoBehaviour {

    public Transform alternateTurret = null;
    public Transform PlayerPosition;
    [HideInInspector]
    public Vector3 normalPosition;
    public float speed;
    public int Health;

    public MeshRenderer SerpentMesh;
    public MeshCollider SerpentCollider;

    public int iceDamage = 5;
    public int elecBallDamage = 5;
    public int elecBeamDamage = 5;
    public int magDamage = 25;
    public int magGrenadeDamage = 100;

    public int damage;
    public MainPlayer.Element element;


    public Text BossEnemyText;

    public float awarenessRange = 600f;

    public AudioSource BossMusic;
    public AudioSource RuneAura;
    public AudioSource QuakeSound;
    public AudioSource levelMusic;

    public AudioSource Hit;
    public AudioSource Death;
    public AudioSource Shoot;
    public AudioSource Encounter;

    public float enemyFlashTimer = 0.5f;
    private float enemyFlashTimerStart;
    public bool enemyFlashEnabled;
    public bool faceTarget;
    public float shakeDuration;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public ParticleSystem blood;
    public GameObject Lightning_Rune;
    public GameObject EnemyBlood;
    public GameObject Boss;

    PlantAutoFire plantFire;


    [SerializeField]

    public Transform camTransform;
    Vector3 originalPos;

    void Start()
    {
        plantFire = GetComponent<PlantAutoFire>();
        normalPosition = gameObject.transform.position;
        PlayerPosition = Camera.main.transform;
        gameObject.GetComponent<NavMeshAgent>().speed = speed;
        enemyFlashTimerStart = enemyFlashTimer;
        enemyFlashEnabled = false;
        faceTarget = true;
        

    }
    void Update()
    {
        if (PlayerPosition != null && Vector3.Magnitude(normalPosition - PlayerPosition.position) < awarenessRange)
        {
            MoveToPlayer();
            BossEnemyText.text = "Serpent " + Health.ToString();
            levelMusic.Stop();
            
            
            if (faceTarget == true)
            {
                if (alternateTurret == null)
                {
                    transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
          
                }
                else
                {
                    alternateTurret.transform.LookAt(Camera.main.transform.GetComponentInParent<CharacterController>().bounds.center);
                }
            }
        }
        if (Health <= 0)
        {
            BossEnemyText.text = "";
            Death.Play();
            enemyFlashEnabled = false;
            RuneAura.Play();
            SerpentMesh.enabled = false;
            plantFire.enabled = false;
            SerpentCollider.enabled = false;
            Lightning_Rune.SetActive(true);
            BossMusic.Stop();
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
    public void MoveToPlayer()
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(PlayerPosition.transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            MainPlayer.Instance().DoDamage(damage, element);
        }
        if (col.tag == "SerpentTerritory")
        {
            OnSerpentActive();
        }
    }

    public void DealDamage(int damage)
    {
        enemyFlashEnabled = true;
        Hit.Play();
        blood.Emit(30);
        Health -= damage;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Destroy(other.gameObject);
            DealDamage(elecBeamDamage);
            if (Health <= 0)
            {
                shakeDuration += 5;
            }
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            Destroy(other.gameObject);
            DealDamage(elecBallDamage);
            if (Health <= 0)
            {
                shakeDuration += 5;
            }
        }
        else if (other.gameObject.CompareTag("IceShot"))
        {
            Destroy(other.gameObject);
            DealDamage(iceDamage);
            if (Health <= 0)
            {
                shakeDuration += 5;
            }
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            Destroy(other.gameObject);
            DealDamage(magDamage);
            if (Health <= 0)
            {
                shakeDuration += 5;
            }
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            Destroy(other.gameObject);
            DealDamage(magGrenadeDamage);
            if (Health <= 0)
            {
                shakeDuration += 5;
            }
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
    void OnSerpentActive()
    {

            Encounter.Play();
            BossMusic.Play();

    }
}
