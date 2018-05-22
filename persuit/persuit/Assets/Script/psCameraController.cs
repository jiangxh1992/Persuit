﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psCameraController : MonoBehaviour {
    float initPosY = 0.0f;
    public float cameraJumpSpeed = 0.005f;
	// Use this for initialization
	void Start () {
        initPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (psGlobalDatabase.Ins.mainChar.mStateManager.mCurState == HeaderProto.PCharState.PCharStateJumpUp)
		{
            transform.position += new Vector3(0, cameraJumpSpeed, 0);
		}
        else if(transform.position.y > initPosY)
        {
            transform.position -= new Vector3(0, cameraJumpSpeed, 0);
        }
         * */
        if (psGameLevelManager.Ins != null && psGameLevelManager.Ins.GameLevelType == 2) { 
            Vector3 mainPos = psGlobalDatabase.Ins.mainChar.transform.position;
            transform.position = new Vector3(mainPos.x, mainPos.y, transform.position.z);
        }
	}
}
