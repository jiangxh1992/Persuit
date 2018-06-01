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
    bool isMoving = false;
    Vector3 tarPos = Vector3.zero;
    Vector3 initPos = Vector3.zero;
    float areaX = 5.0f;
    float areaY = 8.0f;

	void Start () {
		mAnimator = transform.Find("rendernode").GetComponent<Animator>();
        initPos = transform.position;
        tarPos = new Vector3(initPos.x,initPos.y+areaY,initPos.z);
	}
	
	void Update () {
        if(isMoving){
            float targetY = Mathf.Lerp(transform.position.y,tarPos.y,0.005f);
            float targetX = Mathf.Lerp(transform.position.x,tarPos.x,0.005f);
            transform.position = new Vector3(targetX, targetY, initPos.z);
        }
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
        if (psGlobalDatabase.Ins.curLevel == "GameLevel3") {
            isMoving = true;
            StartCoroutine(RandomTarPos());
            StartCoroutine(Shot());
        }
        yield return 0;
    }

    IEnumerator Shot() {
        psGameLevelManager.Ins.gameCamera.GetComponent<psCameraController>().TargetPosYOffset = 3.0f;
        yield return new WaitForSeconds(3.0f);
        while (isShot) {
            GameObject music = psGameLevelManager.Ins.CreateMusicBullet();
            music.transform.parent = psGlobalDatabase.Ins.curNpc.transform;
            music.transform.localPosition = Vector3.zero;
            music.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
        yield return 0;
    }

    IEnumerator RandomTarPos()
    {
        while (true)
        {
            if (Random.Range(0, 1.0f) > 0.5)
            {
                tarPos = new Vector3(Random.Range(initPos.x - areaX, initPos.x + areaX), Random.Range(initPos.y - areaY/2, initPos.y + areaY), initPos.z);
            }
            yield return new WaitForSeconds(2.0f);
        }
        yield return 0;
    }
}
