using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElectricHealthRespawn : MonoBehaviour {

    public AudioSource ammoSound;
    GameObject player;
    bool playerInTerritory;

    public int Health;
    public float TimeAmount;
 

    public float HealthRespawnTimer;

    public GameObject HealthObject;

    // Use this for initialization
    void Start () {

        HealthRespawnTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update () {

        Health = MainPlayer.health;

        
        if (HealthRespawnTimer > 0)
        {
            HealthRespawnTimer -= Time.deltaTime;
        }
        if (HealthRespawnTimer < 0)
        {
            HealthRespawnTimer = 0;
            HealthObject.SetActive(true); 
        }
        if (HealthRespawnTimer >= TimeAmount)
        {
            HealthRespawnTimer = TimeAmount;
        }
        if (playerInTerritory == true && HealthRespawnTimer == 0)
        {
            HealthRespawnTimer += TimeAmount;
            HealthObject.SetActive(false);
            playerInTerritory = false;

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && HealthRespawnTimer == 0 && Health <= 99)
        {
            playerInTerritory = true;
        }
    }
}
