using UnityEngine;
using System.Collections;

public class PowerStationField : MonoBehaviour
{

    public CapsuleCollider Collider;
    public AudioSource ForceField;
    public int Shots;

    private void Start()
    {
        Shots = 3;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("IceShot"))
        {

            Shots--;
            if (Shots == 0)
            {
                ForceField.Play();
                Destroy(gameObject);
            }
        }
    }

}

