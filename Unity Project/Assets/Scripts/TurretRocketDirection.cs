using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRocketDirection : MonoBehaviour
{

    //public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public GameObject Rocket;
    TurretRocketOrbit bossenemy;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossenemy = Rocket.GetComponent<TurretRocketOrbit>();
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            bossenemy.MoveRocket();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
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


