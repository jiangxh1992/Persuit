﻿using System.Collections;
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
        if (psGameLevelManager.Ins == null || psGlobalDatabase.Ins.mainChar == null) return;

        if (!psGlobalDatabase.Ins.isBlocked && !psGlobalDatabase.Ins.isInFinalArea && psGlobalDatabase.Ins.isGameStart)
        {
            // 纹理滚动
            timeTick += 0.01f * psGlobalDatabase.Ins.mMoveDir;
            offset = speed * timeTick;
            mat.mainTextureOffset = new Vector2(offset, 0);

            Vector3 cameraPos = psGlobalDatabase.Ins.mainChar.transform.position;//psGameLevelManager.Ins.camera.transform.position;
            transform.position = new Vector3(cameraPos.x, transform.position.y, transform.position.z);
        }
    }
}
