using System.Collections;
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
		if (MainCharacter.Ins.mStateManager.mCurState == HeaderProto.PCharState.PCharStateJumpUp)
		{
            transform.position += new Vector3(0, cameraJumpSpeed, 0);
		}
        else if(transform.position.y > initPosY)
        {
            transform.position -= new Vector3(0, cameraJumpSpeed, 0);
        }
	}
}
