using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psUIRootManager : Singleton<psUIRootManager> {

    // 欢迎界面
    public GameObject WellComeUI = null;

    // 菜单界面
    public GameObject MenuUI = null;

    // 游戏界面
    public GameObject GameUI = null;
    public GameObject npcDialog = null;
    public GameObject bibleDialog = null;
    public GameObject stopPnl = null;
    public GameObject toolPnl = null;
    public GameObject gameBtn = null;

    // 结算界面
    public GameObject gameoverPnl = null;
    public GameObject btn_home = null;
    public GameObject btn_restart = null;
    public GameObject npcChatBtn = null;
    public GameObject gameText = null;

    // 加载界面
    public GameObject ProgressUI = null;
	// Use this for initialization
	void Start () {
        WellComeUI = transform.Find("WellComeUI").gameObject;
        MenuUI = transform.Find("MenuUI").gameObject;

        GameUI = transform.Find("GameUI").gameObject;
        npcDialog = transform.Find("GameUI/DialogUI/npcDialog").gameObject;
        bibleDialog = transform.Find("GameUI/BiblePnl").gameObject;
        gameoverPnl = transform.Find("GameUI/GameOverPnl").gameObject;
        npcChatBtn = transform.Find("GameUI/DialogUI/npcChatBtn").gameObject;
        gameText = transform.Find("GameUI/TopUI/Text").gameObject;
        stopPnl = transform.Find("GameUI/StopPnl").gameObject;
        toolPnl = transform.Find("GameUI/ToolPnl").gameObject;

        ProgressUI = transform.Find("ProgressUI").gameObject;

        HideAllUIs();
	}

    
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.curNpc != null && npcChatBtn.activeSelf) {
            Vector3 npcPos = psGlobalDatabase.Ins.curNpc.transform.position;
            Vector3 screenPos = psGameLevelManager.Ins.gameCamera.WorldToScreenPoint(new Vector3(npcPos.x+0.3f, npcPos.y + 1.0f, 0));
            npcChatBtn.transform.position = new Vector3(screenPos.x,screenPos.y,0);
        }
	}

    public void HideAllUIs(){
        WellComeUI.SetActive(false);
        MenuUI.SetActive(false);
        GameUI.SetActive(false);
        gameoverPnl.SetActive(false);
        npcDialog.SetActive(false);
        npcChatBtn.SetActive(false);
        gameText.SetActive(false);
        stopPnl.SetActive(false);
        toolPnl.SetActive(false);
        ProgressUI.SetActive(false);
    }
}
