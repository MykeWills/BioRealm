using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun_ArcticProtoPlasm : MonoBehaviour
{
    public Camera CameraPosition;

    public Text ArcticCountAmmo;
    //public Text pauseText;

    public AudioSource audioSrc;

    public AudioClip Shoot;
    public AudioClip CubeShoot;
    public AudioClip Reload;
    public AudioClip FirstBoot;
    public AudioClip CubeDisappear;
    public AudioSource ammoSound;
    public AudioSource levelMusic;

    public GameObject ArcticParticles;
    public GameObject Bullet_Emitter;
    public GameObject Cube_Bullet_Emitter;
    public GameObject IceBullet;
    public GameObject CubeBullet;
    public GameObject whiteScreen;
    public GameObject IceMuzzleFlashObject;
    public GameObject CubeMuzzleFlashObject;
    GameObject ArcticProtoPlasmGun;

    public GameObject ArcticCrosshair;
    public GameObject MagmaCrosshair;

    GameObject Temporary_Cube_Bullet_Handler;

    public static int Ammo;
    public int Cube;
    public int CubeShots;

    public float IceBullet_Forward_Force;
    public float CubeBullet_Forward_Force;
    public float CubeTimer = 0.0f;
    public float Speed = 1.0f;
    public float muzzleFlashTimer = 0.1f;
    public float whiteScreenFlashTimer = 0.1f;
    public float fireRate;
    public float CubefireRate;
    public float PriRecoilStrength;
    public float PriRecoilSpeed;
    public float SecRecoilStrength;
    public float SecRecoilSpeed;
    private float nextFire;
    private float CubenextFire;
    private float muzzleFlashTimerStart;
    private float whiteScreenFlashTimerStart;

    public bool IcemuzzleFlashEnabled = false;
    public bool CubemuzzleFlashEnabled = false;
    public bool whiteScreenFlashEnabled = false;
    public bool AmmoShot;
    public bool CubeAmmoShot;
    //public bool paused;

    private Recoil recoilComponent;

    //public GameObject OptionsMenu;
    //public GameObject ConfirmMenu;
    //public GameObject AudioMenu;
    //public GameObject VideoMenu;
    //public GameObject ControlsMenu;

    void Start()
    {
        ArcticProtoPlasmGun = gameObject;
        var cam = GameObject.FindWithTag("MainCamera").transform;
        recoilComponent = cam.parent.GetComponent<Recoil>();
        //pauseText.text = "";
        Cube = 0;
        Ammo = 150;
        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoShot = true;
        CubeAmmoShot = true;
        SetCountAmmo();
        ArcticCrosshair.SetActive(true);
        MagmaCrosshair.SetActive(false);

    }

    public void Update()
    {
        SetCountAmmo();
        if (Ammo >= 300)
        {
            Ammo = 300;
            SetCountAmmo();
        }
        if (CubeTimer > 0)
        {
            CubeTimer -= Time.deltaTime * Speed;
        }
    
        if (CubeTimer < 0.1f)
        {
            Cube = 0;
            CubeAmmoShot = true;
        }

        if (CubeTimer > 0.1f && CubeTimer <= 10f)
        {
            Cube = 1 ;
            CubeAmmoShot = true;
        }

        if (CubeTimer > 10.1f && CubeTimer <= 20f)
        {
            Cube = 2;
            CubeAmmoShot = true;
        }

        if (CubeTimer > 20.1f && CubeTimer <= 30f)
        {
            Cube = 3;
            CubeAmmoShot = false;
        }

        if (Cube >= 3)
        {
            CubeAmmoShot = false;

            Cube = 3;
        }
        if (CubeTimer >= 30)
        {
            CubeTimer = 30;
        }

        //if (Input.GetButtonDown("Pause"))
        //{
        //    paused = !paused;
        //    Time.timeScale = paused ? 0 : 1;
        //    if (Input.GetMouseButtonDown(0) && paused == true)
        //    {
        //        Ammo -= 0;
        //    }
        //    if (paused == true)
        //    {
        //        AmmoShot = false;
        //        CubeAmmoShot = false;
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //        ArcticCrosshair.SetActive(false);
        //        //OptionsMenu.SetActive(true);
        //        ArcticProtoPlasmGun.SetActive(false);
        //        pauseText.text = "Game Paused";
        //        levelMusic.Pause();
        //    }
        //    else
        //    {
        //        //OptionsMenu.SetActive(false);
        //        //ConfirmMenu.SetActive(false);
        //        //AudioMenu.SetActive(false);
        //        //VideoMenu.SetActive(false);
        //        //ControlsMenu.SetActive(false);
        //        audioSrc.clip = FirstBoot;
        //        audioSrc.Play();
        //        ArcticCrosshair.SetActive(true);
        //        ArcticProtoPlasmGun.SetActive(true);
        //        AmmoShot = true;
        //        CubeAmmoShot = true;
        //        Cursor.visible = false;
        //        pauseText.text = "";
        //        levelMusic.Play();
        //    }
        //}
        if (Input.GetMouseButton(0) && Time.time > nextFire && AmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(PriRecoilSpeed, PriRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                nextFire = Time.time + fireRate;
                IcemuzzleFlashEnabled = true;
                audioSrc.clip = Shoot;
                audioSrc.Play();
                //The Bullet instantiation happens here.
                GameObject Temporary_Ice_Bullet_Handler;
                Temporary_Ice_Bullet_Handler = Instantiate(IceBullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Ice_Bullet_Handler.transform.localScale = Temporary_Ice_Bullet_Handler.transform.localScale * MainPlayer.Instance().transform.localScale.y;
                //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                Temporary_Ice_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Ice_Bullet_Handler.GetComponent<Rigidbody>();

                //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
                Temporary_RigidBody.AddForce(Camera.main.transform.forward * IceBullet_Forward_Force);

                //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                Destroy(Temporary_Ice_Bullet_Handler, 4.0f);
            }
            if (Ammo <= 0)
            {
                Ammo = 0;
                //ArcticProtoPlasmGun.SetActive(false);
                AmmoShot = false;
                SetCountAmmo();

            }
            

        }
        if (Input.GetMouseButton(1) && Time.time > CubenextFire && CubeAmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(SecRecoilSpeed, SecRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                CubeTimer += 10.0f;
                CubenextFire = Time.time + CubefireRate;

                CubemuzzleFlashEnabled = true;
                audioSrc.clip = CubeShoot;
                audioSrc.Play();
                //The Bullet instantiation happens here.

                Temporary_Cube_Bullet_Handler = Instantiate(CubeBullet, Cube_Bullet_Emitter.transform.position, Cube_Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Cube_Bullet_Handler.transform.localScale = Temporary_Cube_Bullet_Handler.transform.localScale*MainPlayer.Instance().transform.localScale.y;
                Temporary_Cube_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Cube_Bullet_Handler.GetComponent<Rigidbody>();


                Temporary_RigidBody.AddForce(Camera.main.transform.forward * CubeBullet_Forward_Force);

                Destroy(Temporary_Cube_Bullet_Handler, 15.0f);
            }

            if (Ammo <= 0)
            {
                Ammo = 0;
                //ArcticProtoPlasmGun.SetActive(false);
                CubeAmmoShot = false;
                SetCountAmmo();
            }
           
        }
        // Color of ammo when is above the Low Rate
        if (Ammo <= 50)
        {
            ArcticCountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        // Color of ammo when is Below the Low Rate
        else if (Ammo >= 51)
        {
            ArcticCountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }

        if (IcemuzzleFlashEnabled == true)
        {
            ArcticParticles.SetActive(false);
            IceMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            ArcticParticles.SetActive(true);
            IceMuzzleFlashObject.SetActive(false);
            IcemuzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;

        }
        if (CubemuzzleFlashEnabled == true)
        {
            ArcticParticles.SetActive(false);
            CubeMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            ArcticParticles.SetActive(true);
            CubeMuzzleFlashObject.SetActive(false);
            CubemuzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;

        }

        if (whiteScreenFlashEnabled == true)
        {
            whiteScreen.SetActive(true);
            whiteScreenFlashTimer -= Time.deltaTime;
        }
        if (whiteScreenFlashTimer <= 0)
        {
            whiteScreen.SetActive(false);
            whiteScreenFlashEnabled = false;
            whiteScreenFlashTimer = whiteScreenFlashTimerStart;
        }

    }

    public void SetCountAmmo()
    {
        ArcticCountAmmo.text = Ammo.ToString() + " / 300";
    }
}




