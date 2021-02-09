using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElectronAmmoRespawn : MonoBehaviour {

    public AudioSource ammoSound;
    GameObject player;
    bool playerInTerritory;

    public int ElectronAmmo;
 

    public float ElectricRespawnTimer;

    public GameObject ElectronAmmoObject;

    // Use this for initialization
    void Start () {

        ElectricRespawnTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update () {

        ElectronAmmo = Gun_ElectronPhaser.Ammo;

        
        if (ElectricRespawnTimer > 0)
        {
            ElectricRespawnTimer -= Time.deltaTime;
        }
        if (ElectricRespawnTimer < 0)
        {
            ElectricRespawnTimer = 0;
            ElectronAmmoObject.SetActive(true); 
        }
        if (ElectricRespawnTimer >= 20)
        {
            ElectricRespawnTimer = 20;
        }
        if (playerInTerritory == true && ElectricRespawnTimer == 0)
        {
            ElectricRespawnTimer += 20;
            ElectronAmmoObject.SetActive(false);
            playerInTerritory = false;

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && ElectricRespawnTimer == 0 && ElectronAmmo <= 89)
        {
            playerInTerritory = true;
        }
    }
}
