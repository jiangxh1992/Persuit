﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame() {
        psSceneManager.Ins.LoadSceneProgress("GameLevel1");
    }
}