using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour {

    public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public AudioSource monsterEncounter;
    public GameObject enemy;
    TurretEnemy Turretenemy;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Turretenemy = enemy.GetComponent<TurretEnemy>();
        playerInTerritory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            enemy.SetActive(true);
            Turretenemy.MoveToPlayer();
        }
        if (playerInTerritory == false)
        {
            enemy.SetActive(false);
            //spiderenemy.MoveToPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            monsterEncounter.Play();
            playerInTerritory = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInTerritory = false;
        }
    }
}


