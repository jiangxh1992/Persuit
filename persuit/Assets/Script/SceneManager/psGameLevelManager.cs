using System.Collections;
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

    bool npcDialog = false;
    public GameObject[] dialogs = new GameObject[2];


	void Start () {
        psUIRootManager.Ins.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        InitUI();
        CreateMainChar();
        InitUIEvent();
	}
    void InitUI() {
        psUIRootManager.Ins.HideAllUIs();
        psUIRootManager.Ins.GameUI.SetActive(true);
        psUIRootManager.Ins.GameUI.transform.Find("BottomUI").gameObject.SetActive(true);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI").gameObject.SetActive(true);
        dialogs[0] = psUIRootManager.Ins.npcDialog.transform.Find("dialog0").gameObject;
        dialogs[1] = psUIRootManager.Ins.npcDialog.transform.Find("dialog1").gameObject;
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
        Time.timeScale = 0;
    }
    void Resume()
    {
        psUIRootManager.Ins.stopPnl.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void OpenNpcDialog()
    {
        //psUIRootManager.Ins.npcDialog.SetActive(!psUIRootManager.Ins.npcDialog.activeSelf);
        GameObject obj = psUIRootManager.Ins.npcDialog;
        obj.SetActive(true);
        if (npcDialog)
        {
            // 隐藏
            iTween.MoveTo(obj, new Vector3(obj.transform.position.x, 1300.0f, obj.transform.position.z), 1.0f);
            psGlobalDatabase.Ins.isGameStart = true;
            OpenToolPnl();
        }
        else {
            // 显示
            iTween.MoveTo(obj,new Vector3(obj.transform.position.x, 860.0f,obj.transform.position.z),1.0f);
            StartCoroutine(DialogShit());
            psGlobalDatabase.Ins.isGameStart = false;
        }
        npcDialog = !npcDialog;
    }
    IEnumerator DialogShit() {
        dialogs[0].SetActive(true);
        dialogs[1].SetActive(false);
        yield return new WaitForSeconds(2.0f);
        //iTween.ColorTo(dialogs[0],new Color(255.0f,255.0f,255.0f,0.0f),2.0f);
        //iTween.ColorTo(dialogs[1],new Color(255.0f,255.0f,255.0f,1.0f),3.0f);
        dialogs[0].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        dialogs[1].SetActive(true);
        yield return 0;
    }
    void OpenToolPnl() {
        psUIRootManager.Ins.toolPnl.SetActive(true);
    }
    void CloseToolPnl()
    {
        psUIRootManager.Ins.toolPnl.SetActive(false);
        StartCoroutine(BlinkObj(psUIRootManager.Ins.GameUI.transform.Find("TopUI/diamond").gameObject));
    }
    IEnumerator BlinkObj(GameObject obj) {
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return 0;
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
        psGlobalDatabase.Ins.mainChar.transform.Find("effect_enter").gameObject.SetActive(true);

        psGlobalDatabase.Ins.moveSpeed = MoveSpeed;
        psGlobalDatabase.Ins.mMoveDir = 0;
    }
	
	// Update is called once per frame
	void Update () {
       if (Input.GetMouseButtonDown(0)) {
           Debug.Log("");
           RaycastHit hit;
           Vector2 screenPosition = Input.mousePosition;
           var ray = psGameLevelManager.Ins.camera.ScreenPointToRay(screenPosition);  //从当前屏幕鼠标位置发出一条射线  
           if (Physics.Raycast(ray, out hit))//判断是否点击到实例物体上  
           {
               if (hit.transform.gameObject.name == "npc")
               {
                   psGlobalDatabase.Ins.curNpc.transform.Find("effect_wakeup").gameObject.SetActive(true);
               }
           }
       }

	}

    public void OnGameOver()
    {
        psGlobalDatabase.Ins.mMoveDir = 0;
        psGlobalDatabase.Ins.isGameStart = false;
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(2.0f);
        psGlobalDatabase.Ins.ResetMainChar();
        psUIRootManager.Ins.gameoverPnl.SetActive(true);
        yield return 0;
    }
}