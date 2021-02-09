using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour {

    public float scrollSpeedX = 0.5F;
    public float scrollSpeedY = 0.5f;
    [HideInInspector]
    public Renderer rend;

    // Use this for initialization
    void Start() {

        rend = GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update() {

        float offset = Time.time * scrollSpeedX;
        float offset2 = Time.time * scrollSpeedY;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset2));

    }
}
