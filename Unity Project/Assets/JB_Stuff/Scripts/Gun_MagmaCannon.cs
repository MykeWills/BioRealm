using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun_MagmaCannon : MonoBehaviour
{
    public GameObject MagmaParticles;
    public Camera CameraPosition;
    public AudioSource audioSrc;
    public AudioClip ShellShoot;
    public AudioClip GrenadeShoot;
    public AudioClip Reload;
    public AudioClip FirstBoot;
    public GameObject Bullet_Emitter1;
    public GameObject Bullet_Emitter2;
    public GameObject Bullet_Emitter3;
    public GameObject Bullet_Emitter4;
    public GameObject Bullet_Emitter5;
    public GameObject Bullet_Emitter6;
    public GameObject Grenade_Bullet_Emitter;
    public GameObject ShellBullet;
    public GameObject GrenadeBullet;
    public GameObject whiteScreen;

    //public AudioSource ammoSound;
    public float Bullet_Forward_Force1;
    public float Bullet_Side_Force1;
    public float Bullet_Forward_Force2;
    public float Bullet_Side_Force2;
    public float Bullet_Forward_Force3;
    public float Bullet_Side_Force3;
    public float Bullet_Forward_Force4;
    public float Bullet_Side_Force4;
    public float Bullet_Forward_Force5;
    public float Bullet_Side_Force5;
    public float Bullet_Forward_Force6;
    public float Bullet_Side_Force6;
    public float Bullet_Up_Force;
    public float Grenade_Up_Force;
    public float GrenadeBullet_Forward_Force;
    public GameObject ShellMuzzleFlashObject;
    public GameObject GrenadeMuzzleFlashObject;

    public float PriRecoilStrength;
    public float PriRecoilSpeed;
    public float SecRecoilStrength;
    public float SecRecoilSpeed;

    public float muzzleFlashTimer = 0.1f;
    private float muzzleFlashTimerStart;
    public bool ShellmuzzleFlashEnabled = false;
    public bool GrenademuzzleFlashEnabled = false;
    public float whiteScreenFlashTimer = 0.1f;
    private float whiteScreenFlashTimerStart;
    public static int Ammo;
    public bool whiteScreenFlashEnabled = false;
    public Text MagmaCountAmmo;
    //public Text pauseText;
    //public GameObject OptionsMenu;
    //public GameObject ConfirmMenu;
    //public GameObject AudioMenu;
    //public GameObject VideoMenu;
    //public GameObject ControlsMenu;
    public GameObject ArcticCrosshair;
    public GameObject MagmaCrosshair;

    public bool ShellAmmoShot;
    public bool GrenadeAmmoShot;
    //public bool paused;
    //public AudioSource levelMusic;
    public float fireRate;
    public float GrenadefireRate;
    private float nextFire;
    public GameObject MagmaCannonGun;
    public GameObject Grenade;
    public Rigidbody Grenaderb;
    private Recoil recoilComponent;



    void Start()
    {
        //pauseText.text = "";
        var cam = GameObject.FindWithTag("MainCamera").transform;
        recoilComponent = cam.parent.GetComponent<Recoil>();
        Grenaderb = Grenade.GetComponent<Rigidbody>();
        muzzleFlashTimerStart = muzzleFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        ShellAmmoShot = true;
        GrenadeAmmoShot = true;
        Grenaderb.useGravity = false;
        ArcticCrosshair.SetActive(false);
        MagmaCrosshair.SetActive(true);
        Ammo = 40;
        SetCountAmmo();
    }

    // Update is called once per frame
    public void Update()
    {

        if (Ammo >= 80)
        {
            Ammo = 80;
            SetCountAmmo();
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
        //        ShellAmmoShot = false;
        //        GrenadeAmmoShot = false;
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //        MagmaCrosshair.SetActive(false);
        //        //OptionsMenu.SetActive(true);
        //        MagmaCannonGun.SetActive(false);
        //        pauseText.text = "Game Paused";
        //        //levelMusic.Pause();
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
        //        MagmaCrosshair.SetActive(true);
        //        MagmaCannonGun.SetActive(true);
        //        ShellAmmoShot = true;
        //        GrenadeAmmoShot = true;
        //        Cursor.visible = false;
        //        pauseText.text = "";
        //        //levelMusic.Play();
        //    }
        //}
        if (Input.GetMouseButton(0) && Time.time > nextFire && ShellAmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(PriRecoilSpeed, PriRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                nextFire = Time.time + fireRate;
                ShellmuzzleFlashEnabled = true;
                audioSrc.clip = ShellShoot;
                audioSrc.Play();

                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(ShellBullet, Bullet_Emitter1.transform.position, Bullet_Emitter1.transform.rotation) as GameObject;
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force1);
                Temporary_RigidBody.AddForce(CameraPosition.transform.right * Bullet_Side_Force1);
                Temporary_RigidBody.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler, 10.0f);

                GameObject Temporary_Bullet_Handler2;
                Temporary_Bullet_Handler2 = Instantiate(ShellBullet, Bullet_Emitter2.transform.position, Bullet_Emitter2.transform.rotation) as GameObject;
                Temporary_Bullet_Handler2.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody2;
                Temporary_RigidBody2 = Temporary_Bullet_Handler2.GetComponent<Rigidbody>();
                Temporary_RigidBody2.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force2);
                Temporary_RigidBody2.AddForce(CameraPosition.transform.right * Bullet_Side_Force2);
                Temporary_RigidBody2.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler2, 10.0f);

                GameObject Temporary_Bullet_Handler3;
                Temporary_Bullet_Handler3 = Instantiate(ShellBullet, Bullet_Emitter3.transform.position, Bullet_Emitter3.transform.rotation) as GameObject;
                Temporary_Bullet_Handler3.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody3;
                Temporary_RigidBody3 = Temporary_Bullet_Handler3.GetComponent<Rigidbody>();
                Temporary_RigidBody3.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force3);
                Temporary_RigidBody3.AddForce(CameraPosition.transform.right * Bullet_Side_Force3);
                Temporary_RigidBody3.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler3, 10.0f);

                GameObject Temporary_Bullet_Handler4;
                Temporary_Bullet_Handler4 = Instantiate(ShellBullet, Bullet_Emitter4.transform.position, Bullet_Emitter4.transform.rotation) as GameObject;
                Temporary_Bullet_Handler4.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody4;
                Temporary_RigidBody4 = Temporary_Bullet_Handler4.GetComponent<Rigidbody>();
                Temporary_RigidBody4.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force4);
                Temporary_RigidBody4.AddForce(CameraPosition.transform.right * Bullet_Side_Force4);
                Temporary_RigidBody4.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler4, 10.0f);


                GameObject Temporary_Bullet_Handler5;
                Temporary_Bullet_Handler5 = Instantiate(ShellBullet, Bullet_Emitter5.transform.position, Bullet_Emitter5.transform.rotation) as GameObject;
                Temporary_Bullet_Handler5.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody5;
                Temporary_RigidBody5 = Temporary_Bullet_Handler5.GetComponent<Rigidbody>();
                Temporary_RigidBody5.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force5);
                Temporary_RigidBody5.AddForce(CameraPosition.transform.right * Bullet_Side_Force5);
                Temporary_RigidBody5.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler5, 10.0f);

                GameObject Temporary_Bullet_Handler6;
                Temporary_Bullet_Handler6 = Instantiate(ShellBullet, Bullet_Emitter6.transform.position, Bullet_Emitter6.transform.rotation) as GameObject;
                Temporary_Bullet_Handler6.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody6;
                Temporary_RigidBody6 = Temporary_Bullet_Handler6.GetComponent<Rigidbody>();
                Temporary_RigidBody6.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force6);
                Temporary_RigidBody6.AddForce(CameraPosition.transform.right * Bullet_Side_Force6);
                Temporary_RigidBody6.AddForce(CameraPosition.transform.up * Bullet_Up_Force);
                Destroy(Temporary_Bullet_Handler6, 10.0f);
            }

            if (Ammo <= 0)
            {
                Ammo = 0;
                ShellAmmoShot = false;
            }
           
        }
        if (Input.GetMouseButton(1) && Time.time > nextFire && GrenadeAmmoShot == true)
        {
            if (Ammo >= 1)
            {
                recoilComponent.StartRecoil(SecRecoilSpeed, SecRecoilStrength, 10f);
                Ammo -= 1;
                SetCountAmmo();
                nextFire = Time.time + GrenadefireRate;
                GrenademuzzleFlashEnabled = true;
                Grenaderb.useGravity = true;
                audioSrc.clip = GrenadeShoot;
                audioSrc.Play();
                //The Bullet instantiation happens here.
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(GrenadeBullet, Grenade_Bullet_Emitter.transform.position, Grenade_Bullet_Emitter.transform.rotation) as GameObject;

                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(CameraPosition.transform.forward * GrenadeBullet_Forward_Force);
                Temporary_RigidBody.AddForce(CameraPosition.transform.up * Grenade_Up_Force);
                Destroy(Temporary_Bullet_Handler, 15.0f);
            }

            if (Ammo <= 0)
            {
                Ammo = 0;
                //MagmaCannonGun.SetActive(false);
                GrenadeAmmoShot = false;
            }
        }
        // Color of ammo when is above the Low Rate
        if (Ammo <= 10)
        {
            MagmaCountAmmo.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
        }
        // Color of ammo when is Below the Low Rate
        else if (Ammo >= 11)
        {
            MagmaCountAmmo.color = new Color(1f, 1f, 1f, 1f);
        }

        if (ShellmuzzleFlashEnabled == true)
        {
            MagmaParticles.SetActive(false);
            ShellMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            MagmaParticles.SetActive(true);
            ShellMuzzleFlashObject.SetActive(false);
            ShellmuzzleFlashEnabled = false;
            muzzleFlashTimer = muzzleFlashTimerStart;

        }
        if (GrenademuzzleFlashEnabled == true)
        {
            MagmaParticles.SetActive(false);
            GrenadeMuzzleFlashObject.SetActive(true);
            muzzleFlashTimer -= Time.deltaTime;
        }
        if (muzzleFlashTimer <= 0)
        {
            MagmaParticles.SetActive(true);
            GrenadeMuzzleFlashObject.SetActive(false);
            GrenademuzzleFlashEnabled = false;
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
        MagmaCountAmmo.text = Ammo.ToString() + " / 80";
    }
}




