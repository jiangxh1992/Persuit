using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    // Use this for initialization
    [Tooltip("跳跃力度")]
    public float upForce = 500f;
    [Tooltip("左右移动速度")]
    public float moveSpeed = 5.0f;
    [Tooltip("多级跳次数")]
    public int maxJump = 2;
    public int jumpCount = 0;
    [Tooltip("角色所在屏幕位置比例")]
    [Range(0.0f, 1.0f)]
    public float mainCharPosRatio = 0.5f;

    Rigidbody2D mRigidbody = null;
    public StateManager mStateManager = null;
    public Animator mAnimator = null;
    public AudioSource mAudioSource = null;
    public AudioClip[] sounds =null;
    int mMoveDir = 0;
    bool isInFinalArea = false; // 是否到达关底

    #region lifecycle
    void Start()
    {
        moveSpeed = 5.0f;
        mStateManager = new StateManager();
        mAnimator = transform.Find("rendernode").GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();

        mRigidbody = GetComponent<Rigidbody2D>();
        // 跳
        InputEventControlller.Ins.OnUpArrowDown += () =>
        {
            if (jumpCount > maxJump) return;
            ++jumpCount;

            mRigidbody.velocity = Vector2.zero;
            mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJump);
        };
        // left
        InputEventControlller.Ins.OnLeftDown += () =>
        {
            mMoveDir = 1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            mRigidbody.velocity = Vector2.zero;
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // right
        InputEventControlller.Ins.OnRightDown += () =>
        {
            mMoveDir = 1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            mRigidbody.velocity = Vector2.zero;
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // 按键松开
        InputEventControlller.Ins.OnLeftUp += () =>
        {
            mMoveDir = 0;
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnRightUp += () =>
        {
            mMoveDir = 0;
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnUpArrowUp += () =>
        {
            //mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("screenwidth:" + Screen.width + "positionX:" + Camera.main.WorldToScreenPoint(transform.position).x);
        //Debug.Log("speed:"+mRigidbody.velocity.y);
        transform.Translate(new Vector2(1, 0) * Time.deltaTime * moveSpeed * mMoveDir); // 主角左右移动

        //  场景移动检测
        if (psGameLevelManager.Ins.GameLevelType == 1 && !isInFinalArea && Camera.main.WorldToScreenPoint(transform.position).x > Screen.width * mainCharPosRatio)
        {
            psPlatformManager.Ins.isFrontLayerMoving = true;
        }

        //Debug.Log("curstate:" + (int)mStateManager.mCurState);
    }
    // 主角碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.gameObject.name;
        if (colliderName.Length >=8 &&colliderName.Substring(0, 8) == "platform") // 落地
        {
            PlayLandEffect();
            jumpCount = 0;
            if (mMoveDir == 0)
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
        if (colliderName == "finalTrigger") // 场景切换
        { 
            psGlobalDatabase.Ins.ResetMainChar();
            psSceneManager.LoadSceneProgress("GameLevel2");
        }
        else if (colliderName == "finalArea") // 关底
        {
            isInFinalArea = true;
            psPlatformManager.Ins.isFrontLayerMoving = false;
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
    #endregion

    void PlayLandEffect() {
        if (mAudioSource != null) {
            mAudioSource.clip = sounds[1];
            mAudioSource.Play();
        }
    }




    #region public interface
    // 0:toIdle 1:toRun 2:toJump
    public void SetAnimationSate(int val)
    {
        
        mAnimator.SetInteger("AnimState", val);
    }
    public void ChangeToIdleAfterDelay(float delay){
        StartCoroutine(DelayChangeToIdle(delay));
    }
    IEnumerator DelayChangeToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAnimationSate(0);
        yield return 0;
    }
    #endregion
}
