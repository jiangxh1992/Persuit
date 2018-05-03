using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour {

    private float offset;
    public float speed = 0.1f;
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        if(psPlatformManager.Ins.isFrontLayerMoving){
            offset += speed * Time.deltaTime * (GetComponent<Renderer>().material.mainTextureScale.x / 1.5f);
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
