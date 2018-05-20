using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class psGameLevelManager : Singleton<psGameLevelManager> {
    // 操作按钮
    public psButton btn_left = null;
    public Button btn_right = null;
    public Button btn_jump = null;

    // 结算界面
    public GameObject gameoverPnl = null;
    public Button btn_home = null;
    public Button btn_restart = null;

	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.GameUI.SetActive(true);
        btn_left = psUIRootManager.Ins.GameUI.transform.Find("BottomUI/btn_left").GetComponent<psButton>();
        btn_right = psUIRootManager.Ins.GameUI.transform.Find("BottomUI/btn_right").GetComponent<Button>();
        btn_jump = psUIRootManager.Ins.GameUI.transform.Find("BottomUI/btn_jump").GetComponent<Button>();

        gameoverPnl = psUIRootManager.Ins.GameUI.transform.Find("GameOverPnl").gameObject;
        gameoverPnl.SetActive(false);
        btn_home = gameoverPnl.transform.Find("btn_home").GetComponent<Button>();
        btn_restart = gameoverPnl.transform.Find("btn_restart").GetComponent<Button>();

        CreateMainChar();
        InitUIEvent();
	}

    void InitUIEvent() {
        btn_home.onClick.AddListener(Home);
        btn_restart.onClick.AddListener(Restart);
    }

    void Home()
    {
        psSceneManager.LoadSceneProgress("Menu");
    }
    void Restart()
    {
        psSceneManager.LoadScene(psGlobalDatabase.Ins.curLevel);
    }

    void CreateMainChar() {
        if (psGlobalDatabase.Ins.mainChar == null) {
            Object obj = Resources.Load("Prefab/MainCharacter", typeof(GameObject));
            GameObject gameobj = Instantiate(obj) as GameObject;
            psGlobalDatabase.Ins.mainChar = gameobj.GetComponent<MainCharacter>();
        }
        psGlobalDatabase.Ins.ResetMainChar();
        psGlobalDatabase.Ins.mainChar.gameObject.SetActive(true);
        psGlobalDatabase.Ins.mainChar.transform.parent = psPlatformManager.Ins.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameOver()
    {
        psGlobalDatabase.Ins.ResetMainChar();
        gameoverPnl.SetActive(true);
    }
}
