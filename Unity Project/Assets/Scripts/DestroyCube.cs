using UnityEngine;
using System.Collections;

public class DestroyCube : MonoBehaviour {
   // public AudioSource CubeDisappear;
    public int Shots;
    // Use this for initialization
    void Start () {
        Shots = 3;
	}
	
	// Update is called once per frame
	void Update () {
     

        
       
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("IceShot"))
        {

            Shots--;
            if (Shots == 0)
            {
                //CubeDisappear.Play();
                Destroy(gameObject);
            }
        }



    }
}
