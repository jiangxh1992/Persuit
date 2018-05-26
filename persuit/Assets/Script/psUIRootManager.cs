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
        bibleDialog = transform.Find("GameUI/BiblePnl").gameObject;
        gameoverPnl = transform.Find("GameUI/GameOverPnl").gameObject;
        npcChatBtn = transform.Find("GameUI/DialogUI/npcChatBtn").gameObject;
        stopPnl = transform.Find("GameUI/StopPnl").gameObject;

        ProgressUI = transform.Find("ProgressUI").gameObject;

        HideAllUIs();
	}

    
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.curNpc != null && npcChatBtn.activeSelf) {
            Vector3 npcPos = psGlobalDatabase.Ins.curNpc.transform.position;
            Vector3 screenPos = psGameLevelManager.Ins.camera.WorldToScreenPoint(new Vector3(npcPos.x, npcPos.y + 0.5f, 0));
            npcChatBtn.transform.position = new Vector3(screenPos.x,screenPos.y,0);
        }
        /*
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;//射线投射碰撞信息  
            // 从鼠标所在的位置发射  
            Vector2 screenPosition = Input.mousePosition;//当前鼠标的位置  
            var ray = psGameLevelManager.Ins.camera.ScreenPointToRay(screenPosition);  //从当前屏幕鼠标位置发出一条射线  
            if (Physics.Raycast(ray, out hit))//判断是否点击到实例物体上  
            {
                if (hit.transform.gameObject.name == "npc")
                {
                }
            }
        }
         * */
	}

    void checkClick()
    {
    } 

    public void HideAllUIs(){
        WellComeUI.SetActive(false);
        MenuUI.SetActive(false);
        GameUI.SetActive(false);
        gameoverPnl.SetActive(false);
        npcDialog.SetActive(false);
        npcChatBtn.SetActive(false);
        stopPnl.SetActive(false);
        ProgressUI.SetActive(false);
    }
}
