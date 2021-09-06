using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] float speed; //Speed Move Bg
    float offsetX;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offsetX += CrossPlatformInputManager.GetAxisRaw("Horizontal") * speed;
        mat.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
    }
}
