﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class psGameLevelManager : Singleton<psGameLevelManager> {
    public int GameLevelType = 1; // 1:横轴 2：纵轴
    public float MainCharScale = 0.3f;
    public Vector3 MainCharInitPos = Vector3.zero;
    public float MoveSpeed = 5.0f;
    public float UpForce = 500.0f;
    public Camera camera = null;

	void Start () {
        InitUI();
        CreateMainChar();
        InitUIEvent();
	}
    void InitUI() {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.GameUI.SetActive(true);
        psUIRootManager.Ins.GameUI.transform.Find("BottomUI").gameObject.SetActive(true);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI").gameObject.SetActive(true);
    }
    void InitUIEvent() {
        psUIRootManager.Ins.gameoverPnl.transform.Find("btn_home").GetComponent<Button>().onClick.AddListener(Home);
        psUIRootManager.Ins.gameoverPnl.transform.Find("btn_restart").GetComponent<Button>().onClick.AddListener(Restart);
        psUIRootManager.Ins.GameUI.transform.Find("DialogUI/npcChatBtn").GetComponent<Button>().onClick.AddListener(OpenNpcDialog);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/diamond").GetComponent<Button>().onClick.AddListener(Diamond);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/bible").GetComponent<Button>().onClick.AddListener(OpenBible);
        psUIRootManager.Ins.bibleDialog.transform.Find("Panel/close").GetComponent<Button>().onClick.AddListener(OpenBible);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/stop").GetComponent<Button>().onClick.AddListener(Stop);
        psUIRootManager.Ins.stopPnl.transform.Find("btn_resume").GetComponent<Button>().onClick.AddListener(Resume);
    }

    void Home()
    {
        psSceneManager.LoadSceneProgress("Menu");
    }
    void Restart()
    {
        psSceneManager.LoadScene(psGlobalDatabase.Ins.curLevel);
    }
    void Diamond() { 
    }
    void OpenBible() {
        psUIRootManager.Ins.bibleDialog.SetActive(!psUIRootManager.Ins.bibleDialog.activeSelf);
    }
    void Stop() {
        psUIRootManager.Ins.stopPnl.SetActive(true);
    }
    void OpenNpcDialog()
    {
        psUIRootManager.Ins.npcDialog.SetActive(!psUIRootManager.Ins.npcDialog.activeSelf);
    }
    void Resume() {
        psUIRootManager.Ins.stopPnl.SetActive(false);
    }

    // 创建主角
    void CreateMainChar() {
        if (psGlobalDatabase.Ins.mainChar == null) {
            Object obj = Resources.Load("Prefab/MainCharacter", typeof(GameObject));
            GameObject gameobj = Instantiate(obj) as GameObject;
            psGlobalDatabase.Ins.mainChar = gameobj.GetComponent<MainCharacter>();
        }
        psGlobalDatabase.Ins.mainChar.gameObject.SetActive(true);
        psGlobalDatabase.Ins.mainChar.transform.localScale = new Vector3(MainCharScale, MainCharScale, MainCharScale);
        psGlobalDatabase.Ins.mainChar.transform.position = MainCharInitPos;
        psGlobalDatabase.Ins.mainChar.transform.parent = psPlatformManager.Ins.transform;

        psGlobalDatabase.Ins.moveSpeed = MoveSpeed;
        psGlobalDatabase.Ins.mMoveDir = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGameOver()
    {
        psGlobalDatabase.Ins.mMoveDir = 0;
        psGlobalDatabase.Ins.ResetMainChar();
        psUIRootManager.Ins.gameoverPnl.SetActive(true);
        psGlobalDatabase.Ins.isGameStart = false;
    }
}
