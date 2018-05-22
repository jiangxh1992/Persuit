using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psMenuManager : MonoBehaviour {
    public Button btn_start = null;
    public Button btn_load = null;
    public GameObject loadPnl = null;
    public Button level1 = null;
    public Button level2 = null;
    public Button level3 = null;
    public Button quit = null;
	// Use this for initialization
	void Start () {
		btn_start = psUIRootManager.Ins.MenuUI.transform.Find("btn_start").GetComponent<Button>();
        btn_start.onClick.AddListener(StartNewGame);

        btn_load = psUIRootManager.Ins.MenuUI.transform.Find("load").GetComponent<Button>();
        btn_load.onClick.AddListener(Load);

        loadPnl = psUIRootManager.Ins.MenuUI.transform.Find("loadPnl").gameObject;
        level1 = loadPnl.transform.Find("level1").GetComponent<Button>();
        level2 = loadPnl.transform.Find("level2").GetComponent<Button>();
        level3 = loadPnl.transform.Find("level3").GetComponent<Button>();
        quit = loadPnl.transform.Find("quit").GetComponent<Button>();
        level1.onClick.AddListener(EnterLevel1);
        level2.onClick.AddListener(EnterLevel2);
        level3.onClick.AddListener(EnterLevel3);
        quit.onClick.AddListener(QuitLevelSel);

        psUIRootManager.Ins.HideAllUIs();
        loadPnl.SetActive(false);
        psUIRootManager.Ins.MenuUI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame() {
        psSceneManager.LoadSceneProgress("GameLevel1");
    }
    public void Load()
    {
        loadPnl.SetActive(true);
    }

    public void EnterLevel1() {
        psSceneManager.LoadSceneProgress("GameLevel1");
    }
    public void EnterLevel2()
    {
        psSceneManager.LoadSceneProgress("GameLevel2");
    }
    public void EnterLevel3()
    {
        psSceneManager.LoadSceneProgress("GameLevel3");
    }
    public void QuitLevelSel() {
        loadPnl.SetActive(false);
    }
}