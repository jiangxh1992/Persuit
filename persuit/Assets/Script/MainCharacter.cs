using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : Singleton<MainCharacter>
{

    // Use this for initialization
    [Tooltip("跳跃力度")]
    public float upForce = 500f;
    [Tooltip("左右移动速度")]
    public float moveSpeed = 5.0f;
    [Tooltip("多级跳次数")]
    public int maxJump = 2;
    public int jumpCount = 0;

    Rigidbody2D mRigidbody = null;
    public StateManager mStateManager = null;
    public Animator mAnimator = null;
    public AudioSource mAudioSource = null;
    public AudioClip[] sounds;
    int mMoveDir = 0;

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
            if (jumpCount >= maxJump) return;
            ++jumpCount;

            mRigidbody.velocity = Vector2.zero;
            mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJumpUp);
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
        //Debug.Log("speed:"+mRigidbody.velocity.y);
        transform.Translate(new Vector2(1, 0) * Time.deltaTime * moveSpeed * mMoveDir); // 主角左右移动

        //  场景移动检测
        if (transform.position.x > 6)
        {
            psPlatformManager.Ins.isFrontLayerMoving = true;
        }
         // 跳到最高点
        if (mStateManager.mCurState == HeaderProto.PCharState.PCharStateJumpUp && mRigidbody.velocity.y < 0) {
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJumpDown);
        }

        // 跌落检测
        if (transform.position.y < -10)
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateDead);

        //Debug.Log("curstate:" + (int)mStateManager.mCurState);
    }
    // 主角碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayLandEffect();
        string colliderName = collision.gameObject.name;
        if (colliderName.Substring(0, 8) == "platform") // 落地
        {
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "finalTrigger") {
            SceneManager.LoadScene("GameLevel2");
        }
        else
            other.gameObject.GetComponent<psNpcManager>().OnWakeUp();
    }

    public void OnDead() {
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }
    #endregion

    void PlayLandEffect() {
        mAudioSource.clip = sounds[1];
        mAudioSource.Play();
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
