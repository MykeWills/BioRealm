using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBallDirection : MonoBehaviour
{

    //public BoxCollider playerTerritory;
    GameObject player;
    bool playerInTerritory;
    public GameObject Plantball;
    PlantBallOrbit plantenemy;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plantenemy = Plantball.GetComponent<PlantBallOrbit>();
        playerInTerritory = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTerritory == true)
        {
            plantenemy.MovePlantBall();
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


