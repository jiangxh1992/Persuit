using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psMenuManager : MonoBehaviour {
    public Button btn_start = null;
	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.MenuUI.SetActive(true);
		btn_start = psUIRootManager.Ins.MenuUI.transform.Find("btn_start").GetComponent<Button>();
        btn_start.onClick.AddListener(StartNewGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame() {
        psSceneManager.LoadSceneProgress("GameLevel1");
    }
}