using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psGlobalDatabase : Singleton<psGlobalDatabase> {

    public MainCharacter mainChar = null; // 主角
    public string curLevel = "Wellcome"; // 当前场景
    public Camera MainCamera = null;
    public psNpcManager curNpc = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 重置主角
    public void ResetMainChar(){
        mainChar.transform.localPosition = new Vector3(-6,5,0);
        mainChar.gameObject.SetActive(false);
    }
}