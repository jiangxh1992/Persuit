using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psCameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.IsGameLevel() && psGlobalDatabase.Ins.mainChar != null) {
            Vector3 mainPos = psGlobalDatabase.Ins.mainChar.transform.position;
            if (psGameLevelManager.Ins != null && !psGlobalDatabase.Ins.isBlocked && !psGlobalDatabase.Ins.isInFinalArea)
            {
                transform.position = new Vector3(mainPos.x, transform.position.y, transform.position.z);
            }
            if (psGameLevelManager.Ins.GameLevelType == 2)
            {
                transform.position = new Vector3(transform.position.x, mainPos.y, transform.position.z);
            }
        }
	}
}