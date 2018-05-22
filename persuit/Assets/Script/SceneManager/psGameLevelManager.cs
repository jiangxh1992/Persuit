using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class psGameLevelManager : Singleton<psGameLevelManager> {
    public int GameLevelType = 1; // 1:横轴 2：纵轴

	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.GameUI.SetActive(true);

        CreateMainChar();
        InitUIEvent();
	}

    void InitUIEvent() {
        psUIRootManager.Ins.btn_home.GetComponent<Button>().onClick.AddListener(Home);
        psUIRootManager.Ins.btn_restart.GetComponent<Button>().onClick.AddListener(Restart);
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
        psGlobalDatabase.Ins.mMoveDir = 0;
        psGlobalDatabase.Ins.ResetMainChar();
        psUIRootManager.Ins.gameoverPnl.SetActive(true);
    }
}
