using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psMenuManager : MonoBehaviour {
    public Button btn_start = null;
    public Button btn_level3 = null;
	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.MenuUI.SetActive(true);
		btn_start = psUIRootManager.Ins.MenuUI.transform.Find("btn_start").GetComponent<Button>();
        btn_start.onClick.AddListener(StartNewGame);

        btn_level3 = psUIRootManager.Ins.MenuUI.transform.Find("level3").GetComponent<Button>();
        btn_level3.onClick.AddListener(Level3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame() {
        psSceneManager.LoadSceneProgress("GameLevel1");
    }
    public void Level3()
    {
        psSceneManager.LoadSceneProgress("GameLevel3");
    }
}