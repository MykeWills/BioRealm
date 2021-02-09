using UnityEngine;
using System.Collections;

public class EnemyVolumeCount : MonoBehaviour
{
    public int Shots;
    public static int x;
    public int Count;

    private void Start()
    {
        Shots = 20;
    }
    void Update()
    {
        
        Count = x;
        if (Shots < 0)
        {
            x++;
            Shots = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ElectronBall"))
        {
            Shots -= 3;
        }
        if (other.gameObject.CompareTag("ElectronBeam"))
        {
            Shots -= 5;


        }
    }

}

