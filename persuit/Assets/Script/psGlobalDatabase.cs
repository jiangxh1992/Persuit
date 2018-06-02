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
    public bool isBlocked = false; // 是否被阻挡
    public bool isGameStart = false; // 游戏开始
    public bool isFistLogin = true; // 是否第一次进入主页面

    public string curLevel = "Wellcome"; // 当前场景
    public Camera MainCamera = null;
    public psNpcManager curNpc = null;
    public Sprite[] music_sprite;
    public Sprite monster_bullet = null;
	// Use this for initialization
	void Start () {
        // 跳
        InputEventControlller.Ins.OnUpArrowDown += () =>
        {
            if (!isGameStart) return;
            if (jumpCount > maxJump) return;
            ++jumpCount;

            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateJump);
        };
        // left
        InputEventControlller.Ins.OnLeftDown += () =>
        {
            if (!isGameStart) return;
            mMoveDir = -1;
            mainChar.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // right
        InputEventControlller.Ins.OnRightDown += () =>
        {
            if (!isGameStart) return;
            mMoveDir = 1;
            mainChar.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            mainChar.mRigidbody.velocity = Vector2.zero;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateRun);
        };
        // 按键松开
        InputEventControlller.Ins.OnLeftUp += () =>
        {
            if (!isGameStart) return;
            mMoveDir = 0;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnRightUp += () =>
        {
            if (!isGameStart) return;
            mMoveDir = 0;
            mainChar.mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
        InputEventControlller.Ins.OnUpArrowUp += () =>
        {
            if (!isGameStart) return;
            //mStateManager.ChangeState(HeaderProto.PCharState.PCharStateIdle);
        };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 重置主角
    public void ResetMainChar(){
        mainChar.gameObject.SetActive(false);
    }

    public bool IsGameLevel() {
        if (curLevel.Length >= 9 && curLevel.Substring(0,9) == "GameLevel") {
            return true;
        }
        return false;
    }
}