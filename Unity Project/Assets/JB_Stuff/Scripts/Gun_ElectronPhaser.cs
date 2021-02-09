using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun_ElectronPhaser : MonoBehaviour
{
    
    public Camera CameraPosition;

    public AudioSource audioSrc;

    public AudioClip BeamShoot;
    public AudioClip BallShoot;
    public AudioClip Reload;
    public AudioClip FirstBoot;
    public AudioSource ammoSound;

    public GameObject ElectronParticles;
    public GameObject Beam_Bullet_Emitter;
    public GameObject Ball_Bullet_Emitter;
    public GameObject BeamBullet;
    public GameObject BallBullet;
    public GameObject whiteScreen;
    public GameObject BeamMuzzleFlashObject;
    public GameObject BallMuzzleFlashObject;
    public GameObject CursorCrosshair;
    public GameObject ElectronPhaserGun;

    public float BeamBullet_Forward_Force;
    public float BallBullet_Forward_Force;
    public float muzzleFlashTimer = 0.1f;
    public float whiteScreenFlashTimer = 0.1f;
    public float fireRate;
    public float BallfireRate;
    public float PriRecoilStrength;
    public float PriRecoilSpeed;
    public float SecRecoilStrength;
    public float SecRecoilSpeed;
    public float AmmoRespawn;

    private float whiteScreenFlashTimerStart;
    private float muzzleFlashTimerStart;
    private float nextFire;

    public bool BeammuzzleFlashEnabled = false;
    public bool BallmuzzleFlashEnabled = false;
    public bool whiteScreenFlashEnabled = false;
    public bool BeamAmmoShot;
    public bool BallAmmoShot;
    //public bool paused;

    public static int Ammo;
   
    public Text ElectronCountAmmo;
    //public Text pauseText;

    //public GameObject OptionsMenu;
    //public GameObject ConfirmMenu;
    //public GameObject AudioMenu;
    //public GameObject VideoMenu;
    //public GameObject ControlsMenu;
  
    
    public AudioSource levelMusic;
   
    
   
    private Recoil recoilComponent;

    void Start()
    {
        //pauseText.text = "";
        var cam = GameObject.FindWithTag("MainCamera").transform;
        recoilComponent = cam.parent.GetComponent<Recoil>();
        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        AmmoRespawn = 0;
        Ammo = 50;
        SetCountAmmo();
        BeamAmmoShot = true;
        BallAmmoShot = true;
    }

    // Update is called once per frame
    public void Update()
    {
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
        //        BeamAmmoShot = false;
        //        BallAmmoShot = false;
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //        CursorCrosshair.SetActive(false);
        //        //OptionsMenu.SetActive(true);
        //        ElectronPhaserGun.SetActive(false);
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
        //        CursorCrosshair.SetActive(true);
        //        ElectronPhaserGun.SetActive(true);
        //        BeamAmmoShot = true;
        //        BallAmmoShot = true;
        //        Cursor.visible = false;
        //        pauseText.text = "";
        //        levelMusic.Play();
        //    }
        //}
        if (Input.GetMouseButton(0) && Time.time > nextFire && BeamAmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(PriRecoilSpeed, PriRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                nextFire = Time.time + fireRate;
                BeammuzzleFlashEnabled = true;
                audioSrc.clip = BeamShoot;
                audioSrc.Play();
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(BeamBullet, Beam_Bullet_Emitter.transform.position, Beam_Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Bullet_Handler.transform.localScale = Temporary_Bullet_Handler.transform.localScale * MainPlayer.Instance().transform.localScale.y;
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(CameraPosition.transform.forward * BeamBullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler, 4.0f);
            }
            if (Ammo <= 0)
            {
                Ammo = 0;
                BeamAmmoShot = false;
            }
        }

        if (Input.GetMouseButton(1) && Time.time > nextFire && BallAmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(SecRecoilSpeed, SecRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                nextFire = Time.time + BallfireRate;
                BallmuzzleFlashEnabled = true;
                audioSrc.clip = BallShoot;
                audioSrc.Play();
                //The Bullet instantiation happens here.
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(BallBullet, Ball_Bullet_Emitter.transform.position, Ball_Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Bullet_Handler.transform.localScale = Temporary_Bullet_Handler.transform.localScale * MainPlayer.Instance().transform.localScale.y;
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(CameraPosition.transform.forward * BallBullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler, 15.0f);

            }
            if (Ammo <= 0)
            {
                Ammo = 0;
                //ArcticProtoPlasmGun.SetActive(false);
                BallAmmoShot = false;
            }
 
        }
        // Color of ammo when is above the Low Rate
        if (Ammo <= 25)
        {
            ElectronCountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        // Color of ammo when is Below the Low Rate
        else if (Ammo >= 26)
        {
            ElectronCountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }

        if (BeammuzzleFlashEnabled == true)
        {
            ElectronParticles.SetActive(false);
            BeamMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            ElectronParticles.SetActive(true);
            BeamMuzzleFlashObject.SetActive(false);
            BeammuzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;

        }
        if (BallmuzzleFlashEnabled == true)
        {
            ElectronParticles.SetActive(false);
            BallMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            ElectronParticles.SetActive(true);
            BallMuzzleFlashObject.SetActive(false);
            BallmuzzleFlashEnabled = false;
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
        ElectronCountAmmo.text = Ammo.ToString() + " / 100";
    }
}




