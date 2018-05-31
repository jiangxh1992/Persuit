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
    public Camera gameCamera = null;

    int curItem = 0;
    public GameObject[] dialogs = new GameObject[2];


	void Start () {
        psUIRootManager.Ins.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        InitUI();
        CreateMainChar();
        InitUIEvent();
	}
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("");
            RaycastHit hit;
            Vector2 screenPosition = Input.mousePosition;
            var ray = psGameLevelManager.Ins.gameCamera.ScreenPointToRay(screenPosition);  //从当前屏幕鼠标位置发出一条射线  
            if (Physics.Raycast(ray, out hit))//判断是否点击到实例物体上  
            {
                string name = hit.transform.gameObject.name;
                if (hit.transform.gameObject.name == "npc")
                {
                    hit.transform.parent.GetComponent<psNpcManager>().OnWakeUp();
                }
                else if(name.Length >= 12 && name.Substring(0,11) == "grass_bible"){
                    curItem = int.Parse(name.Substring(11, name.Length - 11));
                    GameObject item = hit.transform.Find("item").gameObject;
                    hit.transform.Find("effect").gameObject.SetActive(true);
                    iTween.MoveTo(item, new Vector3(item.transform.position.x, item.transform.position.y + 3.0f, item.transform.position.z), 2.0f);
                }
                else if (name.Length >= 4 && name == "item") {
                    Vector3 des = gameCamera.ScreenToWorldPoint(psUIRootManager.Ins.GameUI.transform.Find("TopUI/bible").position);
                    iTween.MoveTo(hit.transform.gameObject,des,3.0f);
                    StartCoroutine(BileCollect(hit.transform.gameObject));
                }
            }
        }
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
        psUIRootManager.Ins.GameUI.transform.Find("DialogUI/npcDialog").GetComponent<Button>().onClick.AddListener(DialogShit);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/diamond").GetComponent<Button>().onClick.AddListener(OpenTool);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/bible").GetComponent<Button>().onClick.AddListener(OpenBible);
        psUIRootManager.Ins.bibleDialog.transform.Find("Panel/close").GetComponent<Button>().onClick.AddListener(OpenBible);
        psUIRootManager.Ins.GameUI.transform.Find("TopUI/stop").GetComponent<Button>().onClick.AddListener(Stop);
        psUIRootManager.Ins.stopPnl.transform.Find("btn_resume").GetComponent<Button>().onClick.AddListener(Resume);
        psUIRootManager.Ins.stopPnl.transform.Find("btn_home").GetComponent<Button>().onClick.AddListener(Home);
        psUIRootManager.Ins.toolPnl.transform.Find("close").GetComponent<Button>().onClick.AddListener(OpenTool);
    }


    void Home()
    {
        psSceneManager.LoadSceneProgress("Menu");
    }
    void Restart()
    {
        psSceneManager.LoadScene(psGlobalDatabase.Ins.curLevel);
    }
    void OpenBible() {
        psUIRootManager.Ins.bibleDialog.SetActive(!psUIRootManager.Ins.bibleDialog.activeSelf);
    }
    void OpenTool() {
        psUIRootManager.Ins.toolPnl.SetActive(!psUIRootManager.Ins.toolPnl.activeSelf);
    }
    IEnumerator BileCollect(GameObject item) {
        yield return new WaitForSeconds(2.5f);
        Destroy(item);
        StartCoroutine(BlinkObj(psUIRootManager.Ins.GameUI.transform.Find("TopUI/bible").gameObject));
        yield return new WaitForSeconds(0.6f);
        OpenBible();
        yield return new WaitForSeconds(1.5f);
        psUIRootManager.Ins.bibleDialog.transform.Find(string.Format("Panel/bible/item{0}/effect", curItem)).gameObject.SetActive(true);
        yield return 0;
    }
    IEnumerator DiamondCollect(GameObject item) {
        yield return new WaitForSeconds(0.5f);
        CloseNpcDialog();
        yield return new WaitForSeconds(0.5f);
        item.SetActive(true);
        Vector3 des = gameCamera.ScreenToWorldPoint(psUIRootManager.Ins.GameUI.transform.Find("TopUI/diamond").position);
        iTween.MoveTo(item, des, 3.0f);
        yield return new WaitForSeconds(2.5f);
        Destroy(item);
        StartCoroutine(BlinkObj(psUIRootManager.Ins.GameUI.transform.Find("TopUI/diamond").gameObject));
        yield return new WaitForSeconds(0.6f);
        OpenTool();
        yield return new WaitForSeconds(1.5f);
        curItem = int.Parse(psGlobalDatabase.Ins.curLevel.Substring(psGlobalDatabase.Ins.curLevel.Length - 1)) - 1;
        psUIRootManager.Ins.toolPnl.transform.Find(string.Format("diamond/item{0}", curItem)).gameObject.SetActive(true);
        
    }
    void Stop() {
        psUIRootManager.Ins.stopPnl.SetActive(true);
        //Time.timeScale = 0;
    }
    void Resume()
    {
        psUIRootManager.Ins.stopPnl.SetActive(false);
        //Time.timeScale = 1.0f;
    }
    void OpenNpcDialog()
    {
        GameObject obj = psUIRootManager.Ins.npcDialog;
        obj.SetActive(true);
        // 显示
        iTween.MoveTo(obj,new Vector3(obj.transform.position.x, 660.0f,obj.transform.position.z),1.0f);
        psGlobalDatabase.Ins.isGameStart = false;
    }
    void CloseNpcDialog() {
        GameObject obj = psUIRootManager.Ins.npcDialog;
        // 隐藏
        iTween.MoveTo(obj, new Vector3(obj.transform.position.x, 1300.0f, obj.transform.position.z), 1.0f);
        psGlobalDatabase.Ins.isGameStart = true;
    }
    void DialogShit() {
        dialogs[0].SetActive(!dialogs[0].activeSelf);
        dialogs[1].SetActive(!dialogs[1].activeSelf);

        if (psGlobalDatabase.Ins.curNpc.transform.Find("diamond"))
        {
            GameObject obj = psGlobalDatabase.Ins.curNpc.transform.Find("diamond").gameObject;
            StartCoroutine(DiamondCollect(obj));
        }
        else {
            CloseNpcDialog();
        }
    }
    IEnumerator BlinkObj(GameObject obj) {
        obj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
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

    public void OnGameOver()
    {
        psGlobalDatabase.Ins.mMoveDir = 0;
        psGlobalDatabase.Ins.isGameStart = false;
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(1.0f);
        psGlobalDatabase.Ins.ResetMainChar();
        psUIRootManager.Ins.gameoverPnl.SetActive(true);
        yield return 0;
    }
}