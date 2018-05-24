using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroller : MonoBehaviour {

    private float offset;
    public float speed = 0.1f;
    Material mat = null;
    float timeTick = 0.0f;
    float mainPosX = 0;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
        mainPosX = psGlobalDatabase.Ins.mainChar.transform.position.x;
    }

    void Update()
    {
        float curX = psGlobalDatabase.Ins.mainChar.transform.position.x;
        if (Mathf.Abs(curX - mainPosX) > 0.1f)
        {
            mainPosX = curX;
            timeTick += 0.02f * psGlobalDatabase.Ins.mMoveDir;
            offset = speed * timeTick;
            mat.mainTextureOffset = new Vector2(offset, 0);
        }

        Vector3 mainPos = Camera.main.transform.position;
        transform.position = new Vector3(mainPos.x, transform.position.y, transform.position.z);
    }
}
