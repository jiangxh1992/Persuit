using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psCameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.IsGameLevel() && psGlobalDatabase.Ins.mainChar != null && !psGlobalDatabase.Ins.isInFinalArea && !psGlobalDatabase.Ins.isBlocked) {
            Vector3 mainPos = psGlobalDatabase.Ins.mainChar.transform.position;
            if (psGameLevelManager.Ins != null && psGameLevelManager.Ins.GameLevelType == 2)
            {
                if(transform.position.x != mainPos.x || transform.position.y != mainPos.y)
                    transform.position = new Vector3(mainPos.x, mainPos.y, transform.position.z);
            }
            else if (psGameLevelManager.Ins != null && psGameLevelManager.Ins.GameLevelType == 1)
            {
                if (transform.position.x != mainPos.x)
                    transform.position = new Vector3(mainPos.x, transform.position.y, transform.position.z);
            }
        }
	}
}