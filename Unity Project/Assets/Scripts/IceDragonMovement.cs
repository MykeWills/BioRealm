using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonMovement : MonoBehaviour {

    bool playerInTerritory;

    public AudioSource monsterEncounter;
    public AudioSource LevelMusic;
    public AudioSource BossMusic;

    GameObject player;

    public GameObject triggerBoss;
    public GameObject Boss;

    public GameObject BossTriggerBox;
    public BoxCollider BossTerritoryBox;
    public GameObject IceballTrigger;
    public BoxCollider IceBallTerritoryBox;

    IceDragon bossenemy;
    IceDragonRotation bossRotation;

    void Start()
    {
       
        playerInTerritory = false;
        player = GameObject.FindGameObjectWithTag("Player");

        // Set boss Mesh off and fireball off
        
        //BossTriggerBox.SetActive(false);
        IceballTrigger.SetActive(false);
        // rotate both mesh and boss gameobject
        bossenemy = triggerBoss.GetComponent<IceDragon>();
        bossRotation = Boss.GetComponent<IceDragonRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            // Start boss TriggerBox  and Iceball start shooting

            IceballTrigger.SetActive(true);
            playerInTerritory = true;

            // start rotating both mesh and boss gameobject

            bossenemy.MeshRotation();
            bossRotation.RotateAroundPlayer();
            //BossTriggerBox.SetActive(false);
            //IceballTrigger.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //When player enters the trigger box
            LevelMusic.Stop();
            BossMusic.Play();
            monsterEncounter.Play();
            playerInTerritory = true;
            BossTerritoryBox.enabled = false;
            IceBallTerritoryBox.enabled = false;
        }
    }

}


