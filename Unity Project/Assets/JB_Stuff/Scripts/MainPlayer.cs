using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainPlayer : MonoBehaviour {
    //public int lives;
    private static MainPlayer instance;  //Code for making the player a globally accesible object, with "MainPlayer.Instance()". Will return null if there is no player.
    public static MainPlayer Instance() {
        instance = FindObjectOfType(typeof(MainPlayer)) as MainPlayer;
        if(!instance){
            Debug.Log("There isn't a player in your scene!");
        }
        return instance;
    }
    
    public static int health = 100;
    public int armor;
    public int armorCap;
    public float armorDamageMultiplier;
    public float elementDamageMultiplier;
    public int arctic;
    public int electron;
    public int magma;
    private int ArcticAmmo;
    private int ElectricAmmo;
    private int MagmaAmmo;

    public int SpiderTouchArmorDamage;
    public int SpiderBallArmorDamage;
    public int SpiderTouchHealthDamage;
    public int SpiderBallHealthDamage;

    public int GhostTouchArmorDamage;
    public int GhostTouchHealthDamage;

    public int IceDragonBallArmorDamage;
    public int IceDragonBallHealthDamage;

    public int SlimeTouchArmorDamage;
    public int SlimeTouchHealthDamage;

    public int PlantTouchArmorDamage;
    public int PlantBallArmorDamage;
    public int PlantTouchHealthDamage;
    public int PlantBallHealthDamage;

    public int SerpentTouchArmorDamage;
    public int SerpentBallArmorDamage;
    public int SerpentTouchHealthDamage;
    public int SerpentBallHealthDamage;


    public int TurretArmorDamage;
    public int TurretHealthDamage;

    public int FlyingTurretArmorDamage;
    public int FlyingTurretHealthDamage;

    public int BossArmorDamage;
    public int BossHealthDamage;
    public int BossProjectileArmorDamage;
    public int BossProjectileHealthDamage;

    //public Text countLives;
    public Text countHealth;
    public Text countArmor;
    public Text WorldText;
    public Slider armorBar;
    public Slider healthBar;
    public Text ItemPickupText;
    public Text WeaponSelection;
    public Text CheckPoint_Text;


    public Text BossHealthText;

    public GameObject ArcticProtoPlasmGun;
    public GameObject ElectronPhaserGun;
    public GameObject MagmaCannonGun;
    public GameObject redScreen;
    public GameObject whiteScreen;
    public GameObject blueScreen;
    public GameObject WaterScreen;

    public GameObject ElectricCrosshair;
    public GameObject ArcticCrosshair;
    public GameObject MagmaCrosshair;

    public GameObject ElectricWeaponUI;
    public GameObject ArcticWeaponUI;
    public GameObject MagmaWeaponUI;

    public GameObject ElectricVisor;
    public GameObject ArcticVisor;
    public GameObject MagmaVisor;

    public GameObject ArmorNumber;


    public bool ElectricArmorEnabled;
    public bool IceArmorEnabled;
    public bool MagmaArmorEnabled;
    public bool ArcticProto;
    public bool ElectronPhase;
    public bool MagmaFlak;
    public bool IceAreaDamage;
    public bool WaterAreaDamage;
    public bool ArmorEnabled;
    public bool redScreenFlashEnabled = false;
    public bool whiteScreenFlashEnabled = false;
    public bool ShieldSoundEnabled = false;

    public bool ElectricPlanet = false;
    public bool IcePlanet = false;
    public bool FirePlanet = false;

    public AudioSource playerSounds;
    public AudioSource playerArmorSounds;
    public AudioSource BeatLevel;
   

    public AudioClip HurtSound;
    public AudioClip HurtLowHealthSound;
    public AudioClip DeathSound;
    public AudioClip ammoSound;
    public AudioClip healthSound;
    public AudioClip ArmorSound;
    public AudioClip ArmorRecharge;
    public AudioClip RuneSound;
    public AudioClip ArcticFirstbootSound;
    public AudioClip ElectronFirstbootSound;
    public AudioClip MagmaFirstbootSound;

    public AudioSource RuneAura;

    public float redScreenFlashTimer = 0.5f;
    public float whiteScreenFlashTimer = 0.5f;

    private float redScreenFlashTimerStart;
    private float whiteScreenFlashTimerStart;
    private float ShieldTimerStart;

    public float hurtRate;
    public float nextHurt;
    public float armorRegen;
    public float timeRegen;
    public float ShieldTimer;
    public float FadeTime;
    public float FadeSpeed;
    public float DamageTime;
    public float LevelLoadtime;


    private Gun_ArcticProtoPlasm ArcticProtoPlasmScript;
    private Gun_ElectronPhaser ElectronPhaserScript;
    private Gun_MagmaCannon MagmaCannonScript;

    public Vector3 lastCheckpoint;

    public enum Element {
        none,
        ice,
        elec,
        fire
    }

    public void DoDamage(int damage, Element element = Element.none) {

        // if armor is enabled take armor & health from player
        if (ArmorEnabled == true) { //if armor is enabled, do less damage.
            whiteScreenFlashEnabled = true;
            armor -= damage;
            //if(element == Element.elec && ElectricArmorEnabled) {
            //    health -= (int)(damage * armorDamageMultiplier * elementDamageMultiplier);
            //} else if(element == Element.fire && MagmaArmorEnabled) {
            //    health -= (int)(damage * armorDamageMultiplier * elementDamageMultiplier);
            //} else if(element == Element.ice && IceArmorEnabled) {
            //    health -= (int)(damage * armorDamageMultiplier * elementDamageMultiplier); // if the element of the attack matches the current armor, do less damage.
            //} else if(element == Element.none) {
            //    health -= (int)(damage * armorDamageMultiplier);
            //}
            //health -= (int)(damage * armorDamageMultiplier);
            SetCountArmor();
            SetCountHealth();
        }
        // if armor is NOT enabled take just health from player
        else if (ArmorEnabled == false) {
            redScreenFlashEnabled = true;
            health -= damage;
            SetCountHealth();
        }
        armorRegen = 0;
        timeRegen = 0;
        timeRegen += 5;
        ShieldTimer += 5;
        if (ShieldTimer >= 5) {
            ShieldTimer = 5;
        }
        if (health >= 26) {
            playerSounds.clip = HurtSound;
            playerSounds.Play();
        } else if (health <= 25) {
            playerArmorSounds.clip = HurtLowHealthSound;
            playerArmorSounds.Play();
        }

    }

    void Start() {

        if (ElectricPlanet)
        {
            ElectricCrosshair.SetActive(false);
            ArcticCrosshair.SetActive(false);
            MagmaCrosshair.SetActive(false);
            WorldText.text = "Thundaria";
            FadeTime = 5.0f;
        }
        else if (IcePlanet)
        {
            WorldText.text = "Zofrorix";
            FadeTime = 5.0f;
        }
        else if (FirePlanet)
        {
            WorldText.text = "Unknown";
            FadeTime = 5.0f;
        }
        else
        {
            FadeTime = 0.0f;
        }


        IceAreaDamage = false;
        WaterAreaDamage = false;

        health = 100;
       
        FadeSpeed = 1.0f;

        WeaponSelection.text = "";
        ItemPickupText.text = "";
        CheckPoint_Text.text = "";
        SetCountHealth();

        SetCountArmor();
        redScreenFlashTimerStart = redScreenFlashTimer;
        whiteScreenFlashTimerStart = whiteScreenFlashTimer;
        ShieldTimerStart = ShieldTimer;

        ArcticProtoPlasmScript = GetComponent<Gun_ArcticProtoPlasm>();
        ElectronPhaserScript = GetComponent<Gun_ElectronPhaser>();
        MagmaCannonScript = GetComponent<Gun_MagmaCannon>();

        ArcticProtoPlasmScript.enabled = false;
        ElectronPhaserScript.enabled = false;
        MagmaCannonScript.enabled = false;

        lastCheckpoint = gameObject.transform.position;
    }
    void Update() {
        
        if (LevelLoadtime > 0)
        {
            LevelLoadtime -= Time.deltaTime;
        }
        if (LevelLoadtime < 0 && ElectricPlanet == true)
        {
            SceneManager.LoadScene(1);
            
            Screen.lockCursor = false;
            Cursor.visible = true;
        }
        else if (LevelLoadtime < 0 && IcePlanet == true)
        {
            SceneManager.LoadScene(1);
            Screen.lockCursor = false;
            Cursor.visible = true;
        }
        else if (LevelLoadtime < 0 && FirePlanet == true)
        {
            SceneManager.LoadScene(1);
            Screen.lockCursor = false;
            Cursor.visible = true;
        }

        ArcticAmmo = Gun_ArcticProtoPlasm.Ammo;
        ElectricAmmo = Gun_ElectronPhaser.Ammo;
        MagmaAmmo = Gun_MagmaCannon.Ammo;
        //-------------------------------------------------------------------ICE AREA--------------------------------------------------------------//
        // Hurt Player for Amount of Time In Ice Area
        if (hurtRate > 0) {
            hurtRate -= Time.deltaTime;
        }
        // If Player is in the Ice Area Hurt Player

        //-------------------------------------------------------------------WATER AREA--------------------------------------------------------------//
        // Hurt Player for Amount of Time In Ice Area

        // If Player is in the Ice Area Hurt Player
        else if (WaterAreaDamage == true && hurtRate <= 0) {

            health -= 1;
            hurtRate = 0;
            hurtRate += .1f;
            SetCountHealth();
            playerSounds.clip = HurtSound;
            playerSounds.Play();
        }
        //-------------------------------------------------------------------SHIELD RECHARGING--------------------------------------------------------------//
        // Count Down Shield Sound Timer
        if (ShieldTimer > 0) {
            playerArmorSounds.Stop();

            ShieldTimer -= Time.deltaTime;
        }
        // If shield timer is below 0 Play Sound Effect
        else if (ShieldTimer < 0) {
            whiteScreenFlashEnabled = true;
            playerArmorSounds.clip = ArmorRecharge;
            playerArmorSounds.Play();
            ShieldTimer = ShieldTimerStart;
        }
        // Cap the shield timer to 10 Max
        else if (ShieldTimer >= 5) {
            ShieldTimer = 5;
        }
        // Count Down The Time to Regenerate Armor
        if (timeRegen > 0) {
            timeRegen -= Time.deltaTime;
        }
        // If Timer is Below 0 Start to Regenerate Armor Timer
        else if (timeRegen < 0) {
            armorRegen += 0.01f;
            timeRegen = 0;
        }
        // Count Down Timer for Armor to Regenerate
        if (armorRegen > 0) {
            armorRegen -= Time.deltaTime;
        }
        // If Armor Timer is below 0 add +1 Armor
        else if (armorRegen < 0) {
            armor += 1;
            armorRegen = 0;
            timeRegen += .01f;
            SetCountArmor();
        }
        //-------------------------------------------------------------------ITEM PICKUP TEXT--------------------------------------------------------------//
        // Count Down the Item Pickup Text Timer
        if (FadeTime > 0) {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }
        // If Text Timer is Below 0 Item Pick up text is Null
        if (FadeTime < 0) {
            FadeTime = 0;
            ItemPickupText.text = "";
            CheckPoint_Text.text = "";
            WorldText.text = "";
        }
        // Cap the Fade timer to 5 if Player collects multiple Items
        if (FadeTime >= 5) {
            FadeTime = 5;
        }
        //-------------------------------------------------------------------RED FLASH DAMAGE--------------------------------------------------------------//
        // flash Screen when damaged
        if (redScreenFlashEnabled == true) {
            redScreen.SetActive(true);
            redScreenFlashTimer -= Time.deltaTime;
        }
        if (redScreenFlashTimer <= 0) {
            redScreen.SetActive(false);
            redScreenFlashEnabled = false;
            redScreenFlashTimer = redScreenFlashTimerStart;
        }
        //-------------------------------------------------------------------ITEM SCREEN FLASH--------------------------------------------------------------//
        // flash screen when item picked up
        if (whiteScreenFlashEnabled == true) {
            whiteScreen.SetActive(true);
            whiteScreenFlashTimer -= Time.deltaTime;
        }
        if (whiteScreenFlashTimer <= 0) {
            whiteScreen.SetActive(false);
            whiteScreenFlashEnabled = false;
            whiteScreenFlashTimer = whiteScreenFlashTimerStart;
        }
        //-------------------------------------------------------------------KEYBOARD NUM WEAPON SELECT--------------------------------------------------------------//
        // If the user presses alpha 1 or num pad 1 to select first weapon
        if (Input.GetButtonDown("Num1") && electron >= 1 && ElectronPhase == false) {

            playerSounds.clip = ElectronFirstbootSound;
            playerSounds.Play();
            WeaponSelection.text = "Electron Phaser";

            ArcticProto = false;
            ElectronPhase = true;
            MagmaFlak = false;



        }
        // If the user presses alpha 2 or num pad 2 to select second weapon
        else if (Input.GetButtonDown("Num2") && arctic >= 1 && ArcticProto == false) {

            playerSounds.clip = ArcticFirstbootSound;
            playerSounds.Play();
            WeaponSelection.text = "Arctic Proto Plasm";

            ArcticProto = true;
            ElectronPhase = false;
            MagmaFlak = false;

           

        }
        // If the user presses alpha 3 or num pad 3 to select third weapon
        else if (Input.GetButtonDown("Num3") && magma >= 1 && MagmaFlak == false) {

            playerSounds.clip = MagmaFirstbootSound;
            playerSounds.Play();
            WeaponSelection.text = "Magma Flak Cannon";

            ArcticProto = false;
            ElectronPhase = false;
            MagmaFlak = true;

        }
        //-------------------------------------------------------------------PLAYER HEALTH, LIVES & ARMOR--------------------------------------------------------------//

        if (ElectronPhase == true)
        {
            MagmaFlak = false;
            ArcticProto = false;
            WeaponSelection.text = "Electron Phaser";
            ArcticCrosshair.SetActive(false);
            MagmaCrosshair.SetActive(false);
            ElectricCrosshair.SetActive(true);

            ArcticWeaponUI.SetActive(false);
            MagmaWeaponUI.SetActive(false);
            ElectricWeaponUI.SetActive(true);
         
            ArcticProtoPlasmGun.SetActive(false);
            ElectronPhaserGun.SetActive(true);
            MagmaCannonGun.SetActive(false);

            ArcticProtoPlasmScript.enabled = false;
            ElectronPhaserScript.enabled = true;
            MagmaCannonScript.enabled = false;
        }
        if (ArcticProto == true)
        {
            ElectronPhase = false;
            MagmaFlak = false;
            WeaponSelection.text = "Arctic Proto Plasm";
            ArcticCrosshair.SetActive(true);
            MagmaCrosshair.SetActive(false);
            ElectricCrosshair.SetActive(false);

            ArcticWeaponUI.SetActive(true);
            MagmaWeaponUI.SetActive(false);
            ElectricWeaponUI.SetActive(false);

            ArcticProtoPlasmGun.SetActive(true);
            ElectronPhaserGun.SetActive(false);
            MagmaCannonGun.SetActive(false);

            ArcticProtoPlasmScript.enabled = true;
            ElectronPhaserScript.enabled = false;
            MagmaCannonScript.enabled = false;
        }
        if (MagmaFlak == true)
        {
            ElectronPhase = false;
            ArcticProto = false;
            WeaponSelection.text = "Magma Flak Cannon";
            ArcticCrosshair.SetActive(false);
            MagmaCrosshair.SetActive(true);
            ElectricCrosshair.SetActive(false);

            ArcticWeaponUI.SetActive(false);
            MagmaWeaponUI.SetActive(true);
            ElectricWeaponUI.SetActive(false);

            ArcticProtoPlasmGun.SetActive(false);
            ElectronPhaserGun.SetActive(false);
            MagmaCannonGun.SetActive(true);

            ArcticProtoPlasmScript.enabled = false;
            ElectronPhaserScript.enabled = false;
            MagmaCannonScript.enabled = true;
        }
        if(ElectricArmorEnabled == true)
        {
            IceArmorEnabled = false;
            MagmaArmorEnabled = false;
            ArcticVisor.SetActive(false);
            MagmaVisor.SetActive(false);
            ElectricVisor.SetActive(true);
            ArmorNumber.SetActive(true);
        }
        if (IceArmorEnabled == true)
        {
            ElectricArmorEnabled = false;
            MagmaArmorEnabled = false;
            ArcticVisor.SetActive(true);
            MagmaVisor.SetActive(false);
            ElectricVisor.SetActive(false);
            ArmorNumber.SetActive(true);
        }
        if (MagmaArmorEnabled == true)
        {
            ElectricArmorEnabled = false;
            IceArmorEnabled = false;
            ArcticVisor.SetActive(false);
            MagmaVisor.SetActive(true);
            ElectricVisor.SetActive(false);
            ArmorNumber.SetActive(true);
        }
        // Cap player health at 100
        if (health >= 100) {
            health = 100;
            SetCountHealth();
        }
        // fixes Player 0 health issue
        if (health <= 0) {
            health = 0;
            playerSounds.clip = DeathSound;
            playerSounds.Play();
            gameObject.transform.position = lastCheckpoint;
            //DamageTime = 0.1f;
            health += 100;
            SetCountHealth();
            
            //Debug.Log(gameObject.transform.position = lastCheckpoint)
;        }

        //Damage Time to fix 0 Health Issue
        if (DamageTime > 0) {
            DamageTime -= Time.deltaTime;
            redScreenFlashEnabled = true;
            health -= 1;
            DamageTime = 0;
            SetCountHealth();
        }

        // if player health is 25 or below Heatlh UI Text set to Flash Red
        if (health <= 25) {
            countHealth.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);

        }
        // if player health is 26 or above Heatlh UI Text set to white
        if (health >= 26) {
            countHealth.color = new Color(1f, 1f, 1f, 1f);
        }
        // If armor is below 0 cap the armor at 0 and set armor inactive
        if (armor < 0) {
            ArmorEnabled = false;
            armor = 0;
            SetCountArmor();
        }
        // if armor is above 1 armor is active
        else if (armor >= 1) {
            ArmorEnabled = true;
        }
        // if player armor is 25 or below Armor UI Text set to Flash Red
        if (armor <= 25) {
            countArmor.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);

        }
        // if player Armor is 26 or above Armor UI Text set to white
        if (armor >= 26) {
            countArmor.color = new Color(1f, 1f, 1f, 1f);
        }
        //-------------------------------------------------------------------SPECIAL ARMOR--------------------------------------------------------------//
        // if Electric Armor is enabled Cap armor to 50 Max
        if (ElectricArmorEnabled == true && armor >= 50) {
            hurtRate = 0;
            armorRegen = 0;
            timeRegen = 0;
            armor = 50;
            armorCap = 50;
            SetCountArmor();
        }
        // if armor is below 0 and armor is enabled cap at 0 and set to inactive
        else if (ElectricArmorEnabled == true && armor < 0) {

            timeRegen += 5;
            ShieldTimer += 5;
            armor = 0;
            ArmorEnabled = false;
            SetCountArmor();
        }
        // if Arctic Armor is enabled Cap armor to 100 Max
        if (IceArmorEnabled == true && armor >= 100) {
            hurtRate = 0;
            armorRegen = 0;
            timeRegen = 0;
            armor = 100;
            armorCap = 100;
            SetCountArmor();
        }
        // if armor is below 0 and armor is enabled cap at 0 and set to inactive
        else if (IceArmorEnabled == true && armor < 0) {
            ArmorEnabled = false;
            armor = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            SetCountArmor();
        }
        // if Magma Armor is enabled Cap armor to 150 Max
        if (MagmaArmorEnabled == true && armor >= 150) {
            hurtRate = 0;
            armorRegen = 0;
            timeRegen = 0;
            armor = 150;
            armorCap = 150;
            SetCountArmor();

        }
        // if armor is below 0 and armor is enabled cap at 0 and set to inactive
        else if (MagmaArmorEnabled == true && armor < 0) {
            armor = 0;
            ArmorEnabled = false;
            ShieldTimer += 10;
            timeRegen += 5;
            SetCountArmor();
        }
        // if no armor is active Cap armor to only 0 Max
        if (MagmaArmorEnabled == false && IceArmorEnabled == false && ElectricArmorEnabled == false && armor >= 0) {
            armorRegen = 0;
            timeRegen = 0;
            ShieldTimer = 0;
            armor = 0;
            ArmorEnabled = false;
            SetCountArmor();
        }
    }

    public void PickUpAmmo(string text, string gun) {
        whiteScreenFlashEnabled = true;
        ItemPickupText.text = "Picked Up "+text;
        FadeTime += 5;
        playerSounds.clip = ammoSound;
        playerSounds.Play();
        if(gun == "mag") {
            Gun_MagmaCannon.Ammo += 20;
            Gun_MagmaCannon.Ammo = Mathf.Clamp(Gun_MagmaCannon.Ammo, 0, 80);
            MagmaCannonScript.SetCountAmmo();
            MagmaCannonScript.GrenadeAmmoShot = true;
            MagmaCannonScript.ShellAmmoShot = true;
        } else if (gun == "arctic") {
            Gun_ArcticProtoPlasm.Ammo += 50;
            Gun_ArcticProtoPlasm.Ammo = Mathf.Clamp(Gun_ArcticProtoPlasm.Ammo, 0, 300);
            ArcticProtoPlasmScript.SetCountAmmo();
            ArcticProtoPlasmScript.AmmoShot = true;
            ArcticProtoPlasmScript.CubeAmmoShot = true;
        } else if (gun == "elec") {
            Gun_ElectronPhaser.Ammo += 25;
            Gun_ElectronPhaser.Ammo = Mathf.Clamp(Gun_ElectronPhaser.Ammo, 0, 100);
            ElectronPhaserScript.SetCountAmmo();
            ElectronPhaserScript.BeamAmmoShot = true;
            ElectronPhaserScript.BallAmmoShot = true;
        }
    }
    private float lastHurtTrigger;
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Lava") && Time.time > lastHurtTrigger+0.1f) {
            DoDamage(10, Element.fire);
            if (MagmaArmorEnabled == true && ArmorEnabled == true) {
                lastHurtTrigger = Time.time + 0.4f;
            }else {
                lastHurtTrigger = Time.time;
            }
        }
        if (other.gameObject.CompareTag("IceArea") && Time.time > lastHurtTrigger + 0.1f && IceArmorEnabled == false)
        {

                DoDamage(20, Element.ice);
                lastHurtTrigger = Time.time + 0.4f;
           
        }
    }
    //-------------------------------------------------------------------ON TRIGGER ENTER TAGS--------------------------------------------------------------//
    void OnTriggerEnter(Collider other) {
        //not item pickups
        if (other.CompareTag("CheckPoint"))
        {
            CheckPoint_Text.text = "Check Point";
            FadeTime += 5;
            lastCheckpoint = gameObject.transform.position;

        }

        //=====================================================ITEM PICKUPS=================================================================================//
        // if player enters Ice Area set area damage to player and enable blue screen effect
        if (other.gameObject.CompareTag("IceAreaDamage")) {
            IceAreaDamage = true;
        }
        if (other.gameObject.CompareTag("IceArea")) {
            blueScreen.SetActive(true);
        }
        // if player enters Water Area set area damage to player and enable water screen effect
        if (other.gameObject.CompareTag("WaterArea")) {
            WaterAreaDamage = true;
            WaterScreen.SetActive(true);
        }
        // if player collects the arctic Rune beat the level
        if (other.gameObject.CompareTag("IceRune"))
        { 
            ItemPickupText.text = "You Got The Ice Rune!";
            FadeTime += 5;
            playerSounds.clip = RuneSound;
            playerSounds.Play();
            RuneAura.Play();
            BeatLevel.Play();
            whiteScreenFlashEnabled = true;
            other.gameObject.SetActive(false);
            LevelLoadtime += 5;

          
        }

        if (other.gameObject.CompareTag("ElectricRune"))
        {
            ItemPickupText.text = "You Got The Electric Rune!";
            FadeTime += 5;
            playerSounds.clip = RuneSound;
            playerSounds.Play();
            RuneAura.Play();
            BeatLevel.Play();
            whiteScreenFlashEnabled = true;
            other.gameObject.SetActive(false);
            LevelLoadtime += 5;
        }

        if (other.gameObject.CompareTag("FireRune"))
        {
            ItemPickupText.text = "You Got The Fire Rune!";
            FadeTime += 5;
            playerSounds.clip = RuneSound;
            playerSounds.Play();
            RuneAura.Play();
            BeatLevel.Play();
            whiteScreenFlashEnabled = true;
            other.gameObject.SetActive(false);
            LevelLoadtime += 5;
            

        }
        // if player obtains a health pack add health to player
        if (other.gameObject.CompareTag("HealthPack")) {
            if (health <= 99) {
                if (other.GetComponent<Pickup_Health>()) {
                    if (other.GetComponent<Pickup_Health>().active == true) {
                        ItemPickupText.text = "Health +10";
                        FadeTime += 5;
                        whiteScreenFlashEnabled = true;
                        playerSounds.clip = healthSound;
                        playerSounds.Play();
                        health += 10; ;
                        SetCountHealth();
                        other.GetComponent<Pickup_Health>().pickup();
                    }
                }
            }
        }
        if (other.gameObject.CompareTag("ArcticAmmo")) {
            if (ArcticAmmo <= 299) {
                if (other.GetComponent<Pickup_Ammo>()) {
                    if (other.GetComponent<Pickup_Ammo>().active == true) {
                        PickUpAmmo("Plasm Cartridge", "arctic");
                        other.GetComponent<Pickup_Ammo>().pickup();
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("ElectronAmmo")) {
            if (ElectricAmmo <= 99) {
                if (other.GetComponent<Pickup_Ammo>()) {
                    if (other.GetComponent<Pickup_Ammo>().active == true) {
                        PickUpAmmo("Electron Cells", "elec");
                        other.GetComponent<Pickup_Ammo>().pickup();
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("MagmaAmmo")) {
            if (MagmaAmmo <= 79) {
                if (other.GetComponent<Pickup_Ammo>()) {
                    if (other.GetComponent<Pickup_Ammo>().active == true) {
                        PickUpAmmo("Magma Shells", "mag");
                        other.GetComponent<Pickup_Ammo>().pickup();
                    }
                }
            }
        }
        //=====================================================WEAPON PICKUPS=================================================================================//
        // when player picks up the Arctic Proto Plasm
        else if (other.gameObject.CompareTag("ArcticProtoPlasm")) {
            WeaponSelection.text = "Arctic Proto Plasm";
            ItemPickupText.text = "Arctic Proto Plasm";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            arctic += 1;
            playerSounds.clip = ArcticFirstbootSound;
            playerSounds.Play();

            ArcticProto = true;
            ElectronPhase = false;
            MagmaFlak = false;

            ArcticWeaponUI.SetActive(true);
            MagmaWeaponUI.SetActive(false);
            ElectricWeaponUI.SetActive(false);

            other.gameObject.SetActive(false);
        }
        // when player picks up the Electron Phaser
        else if (other.gameObject.CompareTag("ElectronPhaser")) {
            WeaponSelection.text = "Electron Phaser";
            ItemPickupText.text = "Electron Phaser";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            electron += 1;
            playerSounds.clip = ElectronFirstbootSound;
            playerSounds.Play();

            ArcticProto = false;
            ElectronPhase = true;
            MagmaFlak = false;

            ArcticWeaponUI.SetActive(false);
            MagmaWeaponUI.SetActive(false);
            ElectricWeaponUI.SetActive(true);

            other.gameObject.SetActive(false);
        }
        // when player picks up the Magma Flak Cannon
        else if (other.gameObject.CompareTag("MagmaCannon")) {
            ArcticCrosshair.SetActive(false);
            MagmaCrosshair.SetActive(true);
            ElectricCrosshair.SetActive(false);
            WeaponSelection.text = "Magma Cannon";
          
            ItemPickupText.text = "Magma Flak Cannon";
            FadeTime += 5;

            whiteScreenFlashEnabled = true;
            magma += 1;
            playerSounds.clip = MagmaFirstbootSound;
            playerSounds.Play();

            ArcticProto = false;
            ElectronPhase = false;
            MagmaFlak = true;

            ArcticWeaponUI.SetActive(false);
            MagmaWeaponUI.SetActive(true);
            ElectricWeaponUI.SetActive(false);

            other.gameObject.SetActive(false);
        }
        //=====================================================ARMOR PICKUPS=================================================================================//
        // If player obtains the electric armor
        else if (other.gameObject.CompareTag("ElectricArmor")) {
            
            ArmorEnabled = true;
            ItemPickupText.text = "Picked Up Electric Armor";
            FadeTime += 5;
            ElectricArmorEnabled = true;
            MagmaArmorEnabled = false;
            IceArmorEnabled = false;
            armor += 50;
            playerArmorSounds.clip = ArmorSound;
            playerArmorSounds.Play();
            SetCountArmor();
            other.gameObject.SetActive(false);
        }
        // If player obtains the Arctic armor
        else if (other.gameObject.CompareTag("ArcticArmor")) {
            ArcticVisor.SetActive(true);
            MagmaVisor.SetActive(false);
            ElectricVisor.SetActive(false);

            ArmorEnabled = true;
            ItemPickupText.text = "Picked Up Arctic Armor";
            FadeTime += 5;
            IceArmorEnabled = true;
            ElectricArmorEnabled = false;
            MagmaArmorEnabled = false;
            armor += 100;
            playerArmorSounds.clip = ArmorSound;
            playerArmorSounds.Play();
            SetCountArmor();
            other.gameObject.SetActive(false);
        }
        // If player obtains the magma armor
        else if (other.gameObject.CompareTag("VolcanicArmor")) {
            ArcticVisor.SetActive(false);
            MagmaVisor.SetActive(true);
            ElectricVisor.SetActive(false);

            ArmorEnabled = true;
            ItemPickupText.text = "Picked Up Magma Armor";
            FadeTime += 5;
            MagmaArmorEnabled = true;
            IceArmorEnabled = false;
            ElectricArmorEnabled = false;
            armor += 150;
            playerArmorSounds.clip = ArmorSound;
            playerArmorSounds.Play();
            SetCountArmor();
            other.gameObject.SetActive(false);
        }

    }
    //-------------------------------------------------------------------ON TRIGGER EXIT TAGS--------------------------------------------------------------//
    void OnTriggerExit(Collider other) {
        // when player leaves trigger deactivate IceArea
        if (other.gameObject.CompareTag("IceAreaDamage")) {
            IceAreaDamage = false;

        }
        if (other.gameObject.CompareTag("IceArea")) {

            blueScreen.SetActive(false);
        }
        // when player leaves trigger deactivate WaterArea
        else if (other.gameObject.CompareTag("WaterArea")) {
            WaterAreaDamage = false;
            WaterScreen.SetActive(false);
        }
    }
    //-------------------------------------------------------------------ON COLLISION ENTER TAGS--------------------------------------------------------------//
    void OnCollisionEnter(Collision other) {

       

        //if player collides with the Ice ghost enemy
        if (other.gameObject.CompareTag("IceGhost")) {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true) {
                whiteScreenFlashEnabled = true;
                armor -= GhostTouchArmorDamage;
                SetCountArmor();
             
            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false) {
                redScreenFlashEnabled = true;
                health -= GhostTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5) {
                ShieldTimer = 5;
            }
            if (health >= 26) {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            } else if (health <= 25) {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        //if player collides with the Ice Spider enemy
        if (other.gameObject.CompareTag("IceSpider")) {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true) {
                whiteScreenFlashEnabled = true;
                armor -= SpiderTouchArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false) {
                redScreenFlashEnabled = true;
                health -= SpiderTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5) {
                ShieldTimer = 5;
            }
            if (health >= 26) {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            } else if (health <= 25) {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
        // if player was hit by Dragon Ice Ball
        if (other.gameObject.CompareTag("IceBall")) {

            // if armor is enabled take armor & health from player

            if (ArmorEnabled == true) {
                whiteScreenFlashEnabled = true;
                armor -= IceDragonBallArmorDamage;
                health -= 10;
                SetCountArmor();
                SetCountHealth();
            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false) {
                redScreenFlashEnabled = true;
                health -= IceDragonBallHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5) {
                ShieldTimer = 5;
            }
            if (health >= 26) {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            } else if (health <= 25) {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
        // if player was hit by Spider Ball
        if (other.gameObject.CompareTag("SpiderBall"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= SpiderBallArmorDamage;
                SetCountArmor();
            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= SpiderBallHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
        if (other.gameObject.CompareTag("Bullet"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= FlyingTurretArmorDamage;
                SetCountArmor();
            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= FlyingTurretHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
        if (other.gameObject.CompareTag("Rocket"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= TurretArmorDamage;
                SetCountArmor();
            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= TurretHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
        if (other.gameObject.CompareTag("Slime"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= SlimeTouchArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= SlimeTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("Serpent"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= SerpentTouchArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= SerpentTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("SerpentBall"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= SerpentTouchArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= SerpentTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("PlantBall"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= PlantBallArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= PlantBallHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("Plant"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= PlantTouchArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= PlantTouchHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("Boss"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= BossArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= BossHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }

        }
        if (other.gameObject.CompareTag("BossProjectile"))
        {

            // if armor is enabled take armor & health from player
            if (ArmorEnabled == true)
            {
                whiteScreenFlashEnabled = true;
                armor -= BossProjectileArmorDamage;
                SetCountArmor();

            }
            // if armor is NOT enabled take just health from player
            else if (ArmorEnabled == false)
            {
                redScreenFlashEnabled = true;
                health -= BossProjectileHealthDamage;
                SetCountHealth();
            }
            armorRegen = 0;
            timeRegen = 0;
            timeRegen += 5;
            ShieldTimer += 5;
            if (ShieldTimer >= 5)
            {
                ShieldTimer = 5;
            }
            if (health >= 26)
            {
                playerSounds.clip = HurtSound;
                playerSounds.Play();
            }
            else if (health <= 25)
            {
                playerArmorSounds.clip = HurtLowHealthSound;
                playerArmorSounds.Play();
            }
        }
    }
    //-------------------------------------------------------------------USER INTERFACE FUNCTIONS--------------------------------------------------------------//
    // Setting the Lives to the canvas
    /*void SetCountLives()
    {
        countLives.text = "Lives " + lives.ToString();
    }
    */

    // Setting the Health to the canvas
    void SetCountHealth() {
        countHealth.text = health.ToString();
        healthBar.value = health;
    }
    // Setting the Armor to the canvas
    void SetCountArmor() {
        countArmor.text = armor.ToString();
        armorBar.maxValue = armorCap;
        armorBar.value = armor;
    }

}