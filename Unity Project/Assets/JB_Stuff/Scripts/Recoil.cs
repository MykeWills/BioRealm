using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    private float recoil = 0.0f;
    private float maxRecoil_x = 20f;
    private float maxRecoil_y = 0f;
    private float recoilSpeed = 2f;
    public Transform Target;

    public void StartRecoil(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
    {
        // in seconds
        recoil = recoilParam;
        maxRecoil_x = maxRecoil_xParam;
        recoilSpeed = recoilSpeedParam;
        
       
    }

    void recoiling()
    {
        if (recoil > 0f)
        {
            Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, maxRecoil_y, 0f);
            // Dampen towards the target rotation
            Target.transform.localRotation = Quaternion.Slerp(Target.transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0f;
            // Dampen towards the target rotation
            Target.transform.localRotation = Quaternion.Slerp(Target.transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        recoiling();
    }
}
