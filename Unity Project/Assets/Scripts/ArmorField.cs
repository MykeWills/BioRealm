using UnityEngine;
using System.Collections;

public class ArmorField : MonoBehaviour
{
    public int Shots;
    public static int x;
    public int PowerCore;
    public AudioSource ForceFieldDown;
    private void Start()
    {
        Shots = 3;
    }
    void Update()
    {
        
        PowerCore = x;
        if (Shots < 0)
        {
            ForceFieldDown.Play();
            x++;
            Shots = 0;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("IceShot"))
        {
            Shots--;

            
        }
    }

}

