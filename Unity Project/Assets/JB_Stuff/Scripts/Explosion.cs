using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public GameObject explosion;

    void OnCollisionEnter(Collision collision)
    {
       
        
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        expl.transform.localScale = explosion.transform.localScale * MainPlayer.Instance().transform.localScale.y;
        if (expl.GetComponent<AudioSource>() != null) {
            expl.GetComponent<AudioSource>().Play();
        }
        Destroy(gameObject); // destroy the grenade
        Destroy(expl, 3); // delete the explosion after 3 seconds
    }
}
