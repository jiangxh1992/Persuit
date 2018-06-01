using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Rigidbody2D mRigidbody = null;
    public StateManager mStateManager = null;
    public Animator mAnimator = null;
    public AudioSource mAudioSource = null;
    public AudioClip[] sounds =null;
    float mainPosX = 0;

    public GameObject effect_dust = null;   // 尘土特效
    public GameObject effect_enter = null;  // 入场特效
    #region lifecycle
    void Start()
    {
        mStateManager = new StateManager();
        mAnimator = transform.Find("rendernode").GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mainPosX = transform.position.x;

        effect_dust = transform.Find("effect_dust").gameObject;
        effect_enter = transform.Find("effect_enter").gameObject;
        effect_enter.SetActive(false);
        StartCoroutine(EnterEffect());
    }

    void Update()
    {
        // 主角左右移动
        if(psGlobalDatabase.Ins.mMoveDir != 0)
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * psGlobalDatabase.Ins.moveSpeed);
        // 检测主角阻挡
        if (Mathf.Abs(transform.position.x - mainPosX) > 0.1f)
        {
            mainPosX = transform.position.x;
            psGlobalDatabase.Ins.isBlocked = false;
        }
        else {
            psGlobalDatabase.Ins.isBlocked = true;
        }
    }
    // 主角碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.gameObject.name;
        // 落地
        if (colliderName.Length >=8 &&colliderName.Substring(0, 8) == "platform")
        {
            PlayEffect(1);
            psGlobalDatabase.Ins.jumpCount = 0;
            if (psGlobalDatabase.Ins.mMoveDir == 0)
            {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
            }
            else {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
            }
        }
    }
    // 触发器
    void OnTriggerEnter2D(Collider2D other)
    {
        string colliderName = other.gameObject.name;
        if(colliderName == "startArea"){
            if (!psGlobalDatabase.Ins.isGameStart) {
                psGlobalDatabase.Ins.isGameStart = true;
            }
        }
        else if (colliderName == "finalTrigger") // 场景切换
        {
            StartCoroutine(EnterNextLevel());
        }
        else if (colliderName == "finalArea") // 镜头不跟随区域
        {
            psGlobalDatabase.Ins.isInFinalArea = true;
        }
        else if (colliderName.Length >= 6 && colliderName.Substring(0, 6) == "killer") // 死亡
        {
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateDead);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        string colliderName = other.gameObject.name;
        if (colliderName == "finalArea") {
            psGlobalDatabase.Ins.isInFinalArea = false;
        }
    }
    #endregion


    // 音效
    public void PlayEffect(int index, float delay = 0.0f) {
        if (mAudioSource == null) return;
        if (index == 100)
        {
            mAudioSource.Stop();
            return;
        }
        //落水
        if (index == 2 && psGlobalDatabase.Ins.curLevel != "GameLevel1") return;
        StartCoroutine(DelayPlayEffect(index,delay));
    }
    IEnumerator DelayPlayEffect(int index, float delay) {
        yield return new WaitForSeconds(delay);
        if (index == 0)
        { // 脚步
            if(mStateManager.mCurState != HeaderProto.PCharState.PCharStateRun) index = 100;
            mAudioSource.loop = true;
        }
        else if (index == 1)
        { // 落地
            mAudioSource.loop = false;
        }
        else if (index == 2)
        { //落水
            mAudioSource.loop = false;
        }
        if (index != 100) {
            mAudioSource.clip = sounds[index];
            mAudioSource.Play();
        }
        yield return 0;
    }

    // 下一关
    IEnumerator EnterNextLevel() {
        yield return new WaitForSeconds(0.3f);
        psGlobalDatabase.Ins.ResetMainChar();
        string curLevel = psGlobalDatabase.Ins.curLevel;
        string nextLevel = "GameLevel" + (int.Parse(curLevel.Substring(curLevel.Length - 1)) + 1);
        psSceneManager.LoadSceneProgress(nextLevel);
        yield return 0;
    }

    // 入场动画
    IEnumerator EnterEffect() {
        yield return new WaitForSeconds(0.3f);
        effect_enter.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        effect_enter.SetActive(false);
        yield return 0;
    }
    // 播放动画
    public void SetAnimationSate(string param, bool val)
    {
        mAnimator.SetBool(param, val);
    }
}
