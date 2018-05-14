using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroller : MonoBehaviour {

    private float offset;
    public float speed = 0.1f;
    Material mat = null;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        if(psPlatformManager.Ins.isFrontLayerMoving){
            offset = speed * Time.time;
            mat.mainTextureOffset = new Vector2(offset,0);
        }
    }
}
