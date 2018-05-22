using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psGlobalDatabase : Singleton<psGlobalDatabase> {

    public MainCharacter mainChar = null; // 主角
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
    public int mMoveDir = 0;
    public bool isInFinalArea = false; // 是否到达关底


    public string curLevel = "Wellcome"; // 当前场景
    public Camera MainCamera = null;
    public psNpcManager curNpc = null;
	// Use this for initialization
	void Start () {
        // 跳
        InputEventControlller.Ins.OnUpArrowDown += () =>
        {
            if (jumpCount > maxJump) return;
            ++jumpCount;

            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJump);
        };
        // left
        InputEventControlller.Ins.OnLeftDown += () =>
        {
            mMoveDir = 1;
            mainChar.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // right
        InputEventControlller.Ins.OnRightDown += () =>
        {
            mMoveDir = 1;
            mainChar.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // 按键松开
        InputEventControlller.Ins.OnLeftUp += () =>
        {
            mMoveDir = 0;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnRightUp += () =>
        {
            mMoveDir = 0;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnUpArrowUp += () =>
        {
            //mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 重置主角
    public void ResetMainChar(){
        mainChar.transform.localPosition = new Vector3(-6,5,0);
        mainChar.gameObject.SetActive(false);
    }
}