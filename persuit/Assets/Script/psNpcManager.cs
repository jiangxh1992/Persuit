using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class psNpcManager : MonoBehaviour{
    public Animator mAnimator = null;
    public float EffectTime = 3.0f;
    public float WeakTime = 0.2f;
    bool isWakeup = false;
    public int item = 0;
    bool isShot = true;

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
        mAnimator.Play("weak");
        yield return new WaitForSeconds(WeakTime);
        mAnimator.Play("idle");
        psUIRootManager.Ins.npcChatBtn.SetActive(true);
        if(psGlobalDatabase.Ins.curLevel == "GameLevel3")
            StartCoroutine(Shot());
        yield return 0;
    }

    IEnumerator Shot() {
        while (isShot) {
            GameObject music = psGameLevelManager.Ins.CreateMusicBullet();
            music.transform.parent = psGlobalDatabase.Ins.curNpc.transform;
            music.transform.localPosition = Vector3.zero;
            music.SetActive(true);
            yield return new WaitForSeconds(2.0f);
        }
        yield return 0;
    }
}
