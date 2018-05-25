using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroller : MonoBehaviour {

    private float offset;
    public float speed = 0.1f;
    Material mat = null;
    float timeTick = 0.0f;
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        Vector3 mainPos = Camera.main.transform.position;
        transform.position = new Vector3(mainPos.x, transform.position.y, transform.position.z);

        float curX = psGlobalDatabase.Ins.mainChar.transform.position.x;
        if (!psGlobalDatabase.Ins.isBlocked && !psGlobalDatabase.Ins.isInFinalArea)
        {
            timeTick += 0.02f * psGlobalDatabase.Ins.mMoveDir;
            offset = speed * timeTick;
            mat.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
