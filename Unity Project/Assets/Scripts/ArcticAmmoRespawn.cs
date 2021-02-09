using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcticAmmoRespawn : MonoBehaviour {

    public AudioSource ammoSound;
    GameObject player;
    bool playerInTerritory;

    public int ArcticAmmo;
 

    public float ArcticRespawnTimer;

    public GameObject ArcticAmmoObject;

    // Use this for initialization
    void Start () {

        ArcticRespawnTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update () {

        ArcticAmmo = Gun_ArcticProtoPlasm.Ammo;
        Debug.Log("I exist?!");
        
        if (ArcticRespawnTimer > 0)
        {
            ArcticRespawnTimer -= Time.deltaTime;
        }
        if (ArcticRespawnTimer < 0)
        {
            ArcticRespawnTimer = 0;
            ArcticAmmoObject.SetActive(true); 
        }
        if (ArcticRespawnTimer >= 20)
        {
            ArcticRespawnTimer = 20;
        }
        if (playerInTerritory == true && ArcticRespawnTimer == 0)
        {
            ArcticRespawnTimer += 20;
            ArcticAmmoObject.SetActive(false);
            playerInTerritory = false;

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && ArcticRespawnTimer == 0 && ArcticAmmo <= 199)
        {
            playerInTerritory = true;
        }
    }
}
