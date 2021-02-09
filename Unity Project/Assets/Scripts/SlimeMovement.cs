using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {

    public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public AudioSource monsterEncounter;
    public GameObject enemy;
    SlimeEnemy slimeenemy;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slimeenemy = enemy.GetComponent<SlimeEnemy>();
        playerInTerritory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            enemy.SetActive(true);
            slimeenemy.MoveToPlayer();
        }
        if (playerInTerritory == false)
        {
            slimeenemy.MoveToNormal();
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


