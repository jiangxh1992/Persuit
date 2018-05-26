using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psCameraController : MonoBehaviour {

    Vector3 targetPos = Vector3.zero;
	void Start () {
	}
	
	void Update () {
        if (psGameLevelManager.Ins == null ||psGlobalDatabase.Ins.mainChar == null) return;
        targetPos = psGlobalDatabase.Ins.mainChar.transform.position;

        if (psGlobalDatabase.Ins.isGameStart) {
            
            // 横向移动
            if (!psGlobalDatabase.Ins.isInFinalArea)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x,0.1f), transform.position.y, transform.position.z);
            }

            // 纵向移动
            if (psGameLevelManager.Ins.GameLevelType == 2)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetPos.y,0.1f), transform.position.z);
            }
        }
	}
}