using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public AudioSource monsterEncounter;
    public GameObject enemy;
    GhostEnemy ghostenemy;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ghostenemy = enemy.GetComponent<GhostEnemy>();
        playerInTerritory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            enemy.SetActive(true);
            ghostenemy.MoveToPlayer();
        }
        if (playerInTerritory == false)
        {
            ghostenemy.MoveToNormal();
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


