using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psUIRootManager : Singleton<psUIRootManager> {

    public GameObject WellComeUI = null;
    public GameObject MenuUI = null;
    public GameObject GameUI = null;
    public GameObject ProgressUI = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideAllUIs(){
        WellComeUI.SetActive(false);
        MenuUI.SetActive(false);
        GameUI.SetActive(false);
        ProgressUI.SetActive(false);
    }
}
