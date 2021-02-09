using UnityEngine;
using System.Collections;

public class PowerStationField1 : MonoBehaviour
{

    public CapsuleCollider Collider;
    public AudioSource ForceField;
    public GameObject ForceFieldShield;
    public int Shots;

    private void Start()
    {
        Shots = 3;
    }
    private void Update()
    {
        if (Shots <= 0)
        {
            Shots = 0;
            ForceField.Play();
            Destroy(gameObject);
            ForceFieldShield.SetActive(false);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ElectronBall"))
        {

            Shots--;
           
        }
        else if (other.gameObject.CompareTag("ElectronBeam"))
        {

            Shots -= 3;
   
        }
    }

}

