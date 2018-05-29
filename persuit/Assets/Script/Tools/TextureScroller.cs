using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroller : MonoBehaviour {

    private float offset;
    public int speed = 1;
    Material mat = null;
    float timeTick = 0.0f;
    
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        //mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        if (psGameLevelManager.Ins == null || psGlobalDatabase.Ins.mainChar == null) return;

        float curX = psGlobalDatabase.Ins.mainChar.transform.position.x;
        if (!psGlobalDatabase.Ins.isBlocked && !psGlobalDatabase.Ins.isInFinalArea && psGlobalDatabase.Ins.isGameStart)
        {
            // 纹理滚动
            timeTick += psGlobalDatabase.Ins.mMoveDir;
            offset = 0.001f * speed * timeTick;
            mat.mainTextureOffset = new Vector2(offset, 0);

            Vector3 cameraPos = psGlobalDatabase.Ins.mainChar.transform.position;//psGameLevelManager.Ins.camera.transform.position;
            transform.position = new Vector3(cameraPos.x, transform.position.y, transform.position.z);
        }
    }
}
