using UnityEngine;
using System.Collections;

public class EnemyVolumeGroup : MonoBehaviour
{

    public int Count;
    public int ObjectCount;
    public GameObject Key;

    void Start()
    {
        Count = 0;
    }
    void Update()
    {
        Count = EnemyVolumeCount.x;

        if (Count >= ObjectCount)
        {
            Key.SetActive(true);
        }

    }
}

