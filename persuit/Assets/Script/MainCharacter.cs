using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Singleton<MainCharacter> {

	// Use this for initialization
    public float upForce = 500f;
    public float horForce = 100f;
    public float moveSpeed = 5.0f;
    Rigidbody2D mRigidbody = null;
    public HeaderProto.PCharState mCurState = HeaderProto.PCharState.PCharStateIdle;
	void Start () {
        moveSpeed = 5.0f;



        mRigidbody = GetComponent<Rigidbody2D>();
        InputEventControlller.Ins.OnUp += () =>
        {
            mRigidbody.velocity = Vector2.zero;
            if (mCurState == HeaderProto.PCharState.PCharStateRun)
            {
                mRigidbody.AddForce((Vector2.up*2 + new Vector2(transform.right.x, transform.right.y)).normalized * upForce, ForceMode2D.Force);
            }
            else {
                mRigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            }
            mCurState = HeaderProto.PCharState.PCharStateJump;
        };
        InputEventControlller.Ins.OnLeftDown += () =>
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            mRigidbody.velocity = Vector2.zero;
            if (mCurState == HeaderProto.PCharState.PCharStateJump) {
                mRigidbody.AddForce(Vector2.left * horForce, ForceMode2D.Force);
            }
            else {
                mCurState = HeaderProto.PCharState.PCharStateRun;
            }
        };
        InputEventControlller.Ins.OnRightDown += () =>
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            mRigidbody.velocity = Vector2.zero;
            if (mCurState == HeaderProto.PCharState.PCharStateJump)
            {
                mRigidbody.AddForce(Vector2.right * horForce, ForceMode2D.Force);
            }
            else
            {
                mCurState = HeaderProto.PCharState.PCharStateRun;
            }
        };
        InputEventControlller.Ins.OnLeftUp += () =>
        {
            mCurState = HeaderProto.PCharState.PCharStateIdle;
        };
        InputEventControlller.Ins.OnRightUp += () =>
        {
            mCurState = HeaderProto.PCharState.PCharStateIdle;
        };
	}
	
	// Update is called once per frame
	void Update () {
        if (mCurState == HeaderProto.PCharState.PCharStateRun) {
            transform.Translate(new Vector2(1,0) * Time.deltaTime * moveSpeed);
        }
	}

    void OnCollisionEnter(Collision collision) {
        mCurState = HeaderProto.PCharState.PCharStateIdle;
    }
}
