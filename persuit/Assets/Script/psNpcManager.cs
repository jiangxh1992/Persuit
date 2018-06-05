using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class psNpcManager : MonoBehaviour{
    public HeaderProto.PNpcType mType = HeaderProto.PNpcType.PNpcTypeNormal; // npc类型
    public HeaderProto.PKillerType mKillerType = HeaderProto.PKillerType.PKillerTypeMusic;
    public float areaX = 5.0f;
    float areaY = 5.0f;

    Animator mAnimator = null;

    float EffectTime = 3.0f;
    float WeakTime = 0.2f;
    bool isWakeup = false;

    bool isMoving = false;
    bool isAttack = false;

    Vector3 tarPos = Vector3.zero;
    Vector3 initPos = Vector3.zero;

    int attackDuration = 20; // 攻击时间

	void Start () {
		mAnimator = transform.Find("rendernode").GetComponent<Animator>();
        initPos = transform.position;
        tarPos = new Vector3(initPos.x,initPos.y+areaY,initPos.z);
	}
	
	void Update () {
        // 开始移动
        if(isMoving){
            float targetY = Mathf.Lerp(transform.position.y,tarPos.y,0.005f);
            float targetX = Mathf.Lerp(transform.position.x,tarPos.x,0.005f);
            transform.position = new Vector3(targetX, targetY, initPos.z);
        }

        // 攻击倒计时
        if (mType == HeaderProto.PNpcType.PNpcTypeAttack && isAttack) {
            psUIRootManager.Ins.gameText.GetComponent<Text>().text = attackDuration.ToString();
        }
	}
    // 触发器
    void OnTriggerEnter2D(Collider2D other) {
        if (mType == HeaderProto.PNpcType.PNpcTypeMonster) {
            OnWakeUp();
        }
    }

    // 唤醒
    public void OnWakeUp() {
        if (isWakeup) return;
        isWakeup = true;
        if (mType == HeaderProto.PNpcType.PNpcTypeNormal || mType == HeaderProto.PNpcType.PNpcTypeAttack) {
            psGlobalDatabase.Ins.curNpc = this;
            StartCoroutine(DelayWakeUp());
            return;
        }
        if (mType == HeaderProto.PNpcType.PNpcTypeMonster) {
            isMoving = true;
            StartCoroutine(Shot());
            StartCoroutine(MonsterMoving());
        }
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
        // HeaderProto.PNpcType.PNpcTypeAttack:
        if (mType == HeaderProto.PNpcType.PNpcTypeAttack) {
            psUIRootManager.Ins.npcChatBtn.SetActive(false);
            psGameLevelManager.Ins.gameCamera.GetComponent<psCameraController>().TargetPosYOffset = 3.0f;
            isMoving = true;
            isAttack = true;
            StartCoroutine(RandomTarPos());
            StartCoroutine(Shot());
        }
        yield return 0;
    }

    IEnumerator Shot() {
        yield return new WaitForSeconds(3.0f);
        isAttack = true;
        mAnimator.Play("attack");
        float delay = (mType == HeaderProto.PNpcType.PNpcTypeAttack) ? 1.5f : 2.0f;
        while (isAttack) {
            GameObject music = psGameLevelManager.Ins.CreateMusicBullet(mKillerType);
            music.transform.parent = this.transform;
            music.transform.localPosition = new Vector3(0,-1.0f,0);
            music.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
        yield return 0;
    }
    
    IEnumerator RandomTarPos()
    {
        if (mType == HeaderProto.PNpcType.PNpcTypeAttack)
            psUIRootManager.Ins.gameText.SetActive(true);
        while (isAttack)
        {
            if (Random.Range(0, 1.0f) > 0.7)
            {
                tarPos = new Vector3(Random.Range(initPos.x - areaX, initPos.x + areaX), Random.Range(initPos.y - areaY/2, initPos.y + areaY), initPos.z);
            }
            yield return new WaitForSeconds(1.0f);
            if (mType == HeaderProto.PNpcType.PNpcTypeAttack) {
                --attackDuration;
                if (attackDuration < 0)
                {
                    isAttack = false;
                    mAnimator.Play("idle");
                    tarPos = initPos;
                    psUIRootManager.Ins.npcChatBtn.SetActive(true);
                    psUIRootManager.Ins.gameText.GetComponent<Text>().text = "";
                    psUIRootManager.Ins.GameUI.transform.Find("TopUI/win").gameObject.SetActive(true);
                }
            }
        }
        yield return 0;
    }
    IEnumerator MonsterMoving() {
        tarPos = initPos;
        tarPos.x += areaX;
        yield return new WaitForSeconds(2.0f);
        while (isMoving) {
            tarPos.x = transform.position.x > initPos.x ? initPos.x - areaX : initPos.x + areaX;
            yield return new WaitForSeconds(4.0f);
        }
    }
}
