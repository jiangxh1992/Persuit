using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class psNpcManager : MonoBehaviour, IPointerClickHandler{
    public Animator mAnimator = null;
    public float WeakTime = 0.2f;
    bool isWakeup = false;

	// Use this for initialization
	void Start () {
		mAnimator = transform.Find("rendernode").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            checkKick();
        }
	}
    // 点击
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("npc clicked!!!");
        //if(isWakeup)
        //    psUIRootManager.Ins.npcDialog.SetActive(!psUIRootManager.Ins.npcDialog.activeSelf);
    }
    void checkKick()
    {
        RaycastHit hit;//射线投射碰撞信息  
        // 从鼠标所在的位置发射  
        Vector2 screenPosition = Input.mousePosition;//当前鼠标的位置  
        var ray = Camera.main.ScreenPointToRay(screenPosition);  //从当前屏幕鼠标位置发出一条射线  
        if (Physics.Raycast(ray, out hit))//判断是否点击到实例物体上  
        {
            if (isWakeup && hit.transform == transform)
                psUIRootManager.Ins.npcDialog.SetActive(!psUIRootManager.Ins.npcDialog.activeSelf);
        }
    } 

    public void OnWakeUp() {
        if (isWakeup) return;
        isWakeup = true;
        psGlobalDatabase.Ins.curNpc = this;
        mAnimator.SetInteger("NpcState",1);
        StartCoroutine(DelayChangeToIdle(WeakTime));
    }
    IEnumerator DelayChangeToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        mAnimator.SetInteger("NpcState", 0);
        psUIRootManager.Ins.npcChatBtn.SetActive(true);
        yield return 0;
    }
}
