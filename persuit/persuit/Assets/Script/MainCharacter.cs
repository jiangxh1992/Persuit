using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Singleton<MainCharacter> {

	// Use this for initialization
    public float upForce = 500f;
    public float horForce = 100f;
    public float moveSpeed = 5.0f;
    Rigidbody2D mRigidbody = null;
    StateManager mStateManager = null;
    Animator mAnimator = null;

    int mMoveDir = 0;
	void Start () {
        moveSpeed = 5.0f;
        mStateManager = new StateManager();
        mAnimator = GameObject.Find("rendernode").GetComponent<Animator>();

        mRigidbody = GetComponent<Rigidbody2D>();
        // 跳
        InputEventControlller.Ins.OnUpArrowDown += () =>
        {
            mRigidbody.velocity = Vector2.zero;
            if (mStateManager.mCurState == HeaderProto.PCharState.PCharStateRun)
            {
                mRigidbody.AddForce((Vector2.up*2 + new Vector2(transform.right.x, transform.right.y)).normalized * upForce, ForceMode2D.Force);
            }
            else {
                mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            }
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJump);
        };
        // left
        InputEventControlller.Ins.OnLeftDown += () =>
        {
            mMoveDir = 1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            mRigidbody.velocity = Vector2.zero;
            if (mStateManager.mCurState == HeaderProto.PCharState.PCharStateJump)
            {
                mRigidbody.AddForce(Vector2.left * horForce, ForceMode2D.Force);
            }
            else {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
            }
        };
        // right
        InputEventControlller.Ins.OnRightDown += () =>
        {
            mMoveDir = 1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            mRigidbody.velocity = Vector2.zero;
            if (mStateManager.mCurState == HeaderProto.PCharState.PCharStateJump)
            {
                mRigidbody.AddForce(Vector2.right * horForce, ForceMode2D.Force);
            }
            else
            {
                mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
            }
        };

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
            mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(1,0) * Time.deltaTime * moveSpeed * mMoveDir);
        if(transform.position.x >6){
            psPlatformManager.Ins.isFrontLayerMoving = true;
        }
	}

    void OnCollisionEnter(Collision collision) {
    }

    // 0:toIdle 1:toRun 2:toJump
    public void SetAnimationSate(int val) {
        mAnimator.SetInteger("AnimState", val);
    }
}
