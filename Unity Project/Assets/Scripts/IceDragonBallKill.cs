using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDragonBallKill : MonoBehaviour {
    public int ballHealth = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ballHealth <= 0)
        {
            Destroy(gameObject); 
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceShot"))
        {
            ballHealth = ballHealth - 1;
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {
            ballHealth = ballHealth - 3;
        }
        else if (other.gameObject.CompareTag("ElectronBall"))
        {
            ballHealth = ballHealth - 3;
        }
        else if (other.gameObject.CompareTag("MagmaGrenade"))
        {
            ballHealth = ballHealth - 100;
        }
        else if (other.gameObject.CompareTag("MagmaShell"))
        {
            ballHealth = ballHealth -25;
        }
    }
}
