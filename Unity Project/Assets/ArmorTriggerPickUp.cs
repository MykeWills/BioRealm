using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorTriggerPickUp : MonoBehaviour {

    public GameObject Gate;
    public int Count;
    public int ArmorOrb;
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public float DoorheightMin;
    public AudioSource audioSrc; //Only need one audio source.
    public AudioClip ArmorOrbPickup;
    public Text OrbText;
    public GameObject OrbUI;
    public Text OrbGateText;
    public float FadeTime;
    public float FadeSpeed;


    // Use this for initialization
    void Start () {

        Gate.SetActive(true);
        OrbText.text = "";
        OrbUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (FadeTime > 0)
        {
            FadeTime -= Time.deltaTime * FadeSpeed;
        }
        // If Text Timer is Below 0 Item Pick up text is Null
        if (FadeTime < 0)
        {
            FadeTime = 0;
            OrbGateText.text = "";

        }
        // Cap the Fade timer to 5 if Player collects multiple Items
        if (FadeTime >= 5)
        {
            FadeTime = 5;
        }
        if (Count >= ArmorOrb)
        {
            Gate.transform.Translate(Vector3.down * Time.deltaTime * DoorOpenSpeed);
            
            // clamp door
            if (Gate.transform.position.y < DoorheightMin)
            {
                Vector3 newPosition = Gate.transform.position;
                newPosition.y = DoorheightMin;
                Gate.transform.position = newPosition;
            }
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ArmorOrb"))
        {
            OrbUI.SetActive(true);
            Count++;
            other.gameObject.SetActive(false);
            audioSrc.clip = ArmorOrbPickup;
            audioSrc.Play();
            SetCountOrb();
            if (Count >= ArmorOrb)
            {
                OrbGateText.text = "Armor Gate Open";
                FadeTime += 5;
            }
        }

    }
    void SetCountOrb()
    {
        OrbText.text = Count.ToString() + " /6";
    }
}
