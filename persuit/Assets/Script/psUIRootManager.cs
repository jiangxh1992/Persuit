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
    // 操作按钮
    public GameObject btn_left = null;
    public GameObject btn_right = null;
    public GameObject btn_jump = null;
    // 结算界面
    public GameObject gameoverPnl = null;
    public GameObject btn_home = null;
    public GameObject btn_restart = null;
    public GameObject npcChatBtn = null;

    // 加载界面
    public GameObject ProgressUI = null;
	// Use this for initialization
	void Start () {
        WellComeUI = transform.Find("WellComeUI").gameObject;

        MenuUI = transform.Find("MenuUI").gameObject;

        GameUI = transform.Find("GameUI").gameObject;
        npcDialog = transform.Find("GameUI/DialogUI/npcDialog").gameObject;
        npcDialog.SetActive(false);
        btn_left = transform.Find("GameUI/BottomUI/btn_left").gameObject;
        btn_right = transform.Find("GameUI/BottomUI/btn_right").gameObject;
        btn_jump = transform.Find("GameUI/BottomUI/btn_jump").gameObject;
        gameoverPnl = transform.Find("GameUI/GameOverPnl").gameObject;
        gameoverPnl.SetActive(false);
        btn_home = gameoverPnl.transform.Find("btn_home").gameObject;
        btn_restart = gameoverPnl.transform.Find("btn_restart").gameObject;
        npcChatBtn = transform.Find("GameUI/DialogUI/npcChatBtn").gameObject;

        ProgressUI = transform.Find("ProgressUI").gameObject;

        npcChatBtn.GetComponent<Button>().onClick.AddListener(OpenNpcDialog);

        HideAllUIs();
	}

    void OpenNpcDialog() {
        npcDialog.SetActive(!npcDialog.activeSelf);
    }
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.curNpc != null && npcChatBtn.activeSelf) {
            Vector3 npcPos = psGlobalDatabase.Ins.curNpc.transform.position;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(npcPos.x + 1.0f, npcPos.y + 1.5f, 0));
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
        ProgressUI.SetActive(false);
    }
}
