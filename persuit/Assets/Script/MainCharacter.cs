using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    // Use this for initialization
    public Rigidbody2D mRigidbody = null;
    public StateManager mStateManager = null;
    public Animator mAnimator = null;
    public AudioSource mAudioSource = null;
    public AudioClip[] sounds =null;
    float mainPosX = 0;
    #region lifecycle
    void Start()
    {
        mStateManager = new StateManager();
        mAnimator = transform.Find("rendernode").GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mainPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("screenwidth:" + Screen.width + "positionX:" + Camera.main.WorldToScreenPoint(transform.position).x);
        //Debug.Log("speed:"+mRigidbody.velocity.y);
        if(psGlobalDatabase.Ins.mMoveDir != 0)
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * psGlobalDatabase.Ins.moveSpeed); // 主角左右移动
        //Debug.Log("curstate:" + (int)mStateManager.mCurState);
        if (Mathf.Abs(transform.position.x - mainPosX) > 0.01f)
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
        if (colliderName.Length >=8 &&colliderName.Substring(0, 8) == "platform") // 落地
        {
            PlayLandEffect();
            psGlobalDatabase.Ins.jumpCount = 0;
            if (psGlobalDatabase.Ins.mMoveDir == 0)
            {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
            }
            else {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
            }
            if (!psGlobalDatabase.Ins.isGameStart)
                psGlobalDatabase.Ins.isGameStart = true;
        }
    }
    // 触发器
    void OnTriggerEnter2D(Collider2D other)
    {
        string colliderName = other.gameObject.name;
        if (colliderName == "finalTrigger") // 场景切换
        { 
            psGlobalDatabase.Ins.ResetMainChar();
            string curLevel = psGlobalDatabase.Ins.curLevel;
            string nextLevel = "GameLevel" + (int.Parse(curLevel.Substring(curLevel.Length - 1))+1);
            psSceneManager.LoadSceneProgress(nextLevel);
        }
        else if (colliderName == "finalArea") // 镜头不跟随区域
        {
            psGlobalDatabase.Ins.isInFinalArea = true;
        }
        else if (colliderName.Length >= 6 && colliderName.Substring(0, 6) == "killer") // 死亡
        {
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateDead);
        }
        else if (colliderName.Length >= 3 && colliderName.Substring(0, 3) == "npc") // npc
        {
            other.gameObject.GetComponent<psNpcManager>().OnWakeUp();
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

    void PlayLandEffect() {
        if (mAudioSource != null) {
            mAudioSource.clip = sounds[1];
            mAudioSource.Play();
        }
    }


    #region public interface
    // 0:toIdle 1:toRun 2:toJump
    public void SetAnimationSate(string param, bool val)
    {
        mAnimator.SetBool(param, val);
    }
    #endregion
}
