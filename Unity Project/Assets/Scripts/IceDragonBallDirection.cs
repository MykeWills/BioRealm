using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonBallDirection : MonoBehaviour
{

    //public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public GameObject Iceball;
    IceDragonBallOrbit bossenemy;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossenemy = Iceball.GetComponent<IceDragonBallOrbit>();
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            bossenemy.MoveIceBall();
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


