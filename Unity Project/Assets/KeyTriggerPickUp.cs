using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyTriggerPickUp : MonoBehaviour {

    public GameObject Gate;
    public int Count;
    public int KeyCount;
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public float DoorheightMin;
    public AudioSource audioSrc; //Only need one audio source.
    public AudioClip KeyPickup;
    public Text KeyText;
    public Text KeyGateText;
    public GameObject KeyUI;
    public float FadeTime;
    public float FadeSpeed;


    // Use this for initialization
    void Start () {

        Gate.SetActive(true);
        KeyText.text = "";
        KeyUI.SetActive(false);
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
            KeyGateText.text = "";

        }
        // Cap the Fade timer to 5 if Player collects multiple Items
        if (FadeTime >= 5)
        {
            FadeTime = 5;
        }

        if (Count >= KeyCount)
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
        if (other.gameObject.CompareTag("KeyCount"))
        {
            KeyUI.SetActive(true);
            Count++;
            other.gameObject.SetActive(false);
            audioSrc.clip = KeyPickup;
            audioSrc.Play();
            SetCountKey();
            if (Count >= KeyCount)
            {
                KeyGateText.text = "Boss Gate Open";
                FadeTime += 5;
            }

        }

    }
    void SetCountKey()
    {
        KeyText.text = Count.ToString() + " /7";
    }
}
