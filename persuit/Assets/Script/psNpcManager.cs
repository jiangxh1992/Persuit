using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class psNpcManager : MonoBehaviour{
    public Animator mAnimator = null;
    public float EffectTime = 3.0f;
    public float WeakTime = 0.2f;
    bool isWakeup = false;

	void Start () {
		mAnimator = transform.Find("rendernode").GetComponent<Animator>();
	}
	
	void Update () {
	}

    public void OnWakeUp() {
        if (isWakeup) return;
        isWakeup = true;
        psGlobalDatabase.Ins.curNpc = this;
        StartCoroutine(DelayWakeUp());
    }
    IEnumerator DelayWakeUp()
    {
        psGlobalDatabase.Ins.curNpc.transform.Find("effect_wakeup").gameObject.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(EffectTime);
        psGlobalDatabase.Ins.curNpc.transform.Find("effect_wakeup").gameObject.SetActive(false);
        mAnimator.SetInteger("NpcState", 1);
        yield return new WaitForSeconds(WeakTime);
        mAnimator.SetInteger("NpcState", 0);
        psUIRootManager.Ins.npcChatBtn.SetActive(true);
        yield return 0;
    }
}
