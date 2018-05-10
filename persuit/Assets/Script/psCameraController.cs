using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psCameraController : MonoBehaviour {
    public float cameraTopY = 8;
    public float cameraBottomY = -5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool isCharacterActive = MainCharacter.Ins.mStateManager.mCurState == HeaderProto.PCharState.PCharStateJumpDown || MainCharacter.Ins.mStateManager.mCurState == HeaderProto.PCharState.PCharStateJumpUp || MainCharacter.Ins.mStateManager.mCurState == HeaderProto.PCharState.PCharStateRun;
        bool isInsideBorder = transform.position.y < cameraTopY && transform.position.y > cameraBottomY;
		if (isCharacterActive && isInsideBorder) 
		{
			transform.position = new Vector3 (transform.position.x, MainCharacter.Ins.transform.position.y, transform.position.z);
			if (this.transform.position.y > cameraTopY) 
			{
				this.transform.position = new Vector3 (transform.position.x, cameraTopY - 0.01f, transform.position.z);
			} 
			else if (this.transform.position.y < cameraBottomY) 
			{
				this.transform.position = new Vector3 (transform.position.x, cameraBottomY + 0.01f, transform.position.z);
			}
		}
	}
}
