using UnityEngine;
using System.Collections;

public class ArmorForceField : MonoBehaviour
{
    public int PowerCorecount;
    public int powercore;
    public int ObjectCount;

    void Start()
    {
        powercore = 0;
    }
    void Update()
    {
        powercore = ArmorField.x;

        if (powercore >= ObjectCount)
        {
            gameObject.SetActive(false);
        }

    }
}

