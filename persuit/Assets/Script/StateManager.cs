using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager {

    public HeaderProto.PCharState mCurState = HeaderProto.PCharState.PCharStateIdle;
	// Use this for initialization

    public void ChangeState(HeaderProto.PCharState nextState) {
        mCurState = nextState;
        // change animation here
        switch (nextState) { 
            case HeaderProto.PCharState.PCharStateIdle:
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("idle");
                psGlobalDatabase.Ins.mainChar.effect_dust.SetActive(false);
                psGlobalDatabase.Ins.mainChar.PlayEffect(100);
                break;
            case HeaderProto.PCharState.PCharStateJump:
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("jump");
                psGlobalDatabase.Ins.mainChar.effect_dust.SetActive(false);
                psGlobalDatabase.Ins.mainChar.PlayEffect(100);
                break;
            case HeaderProto.PCharState.PCharStateRun:
                psGlobalDatabase.Ins.mainChar.effect_dust.SetActive(true);
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("run");
                psGlobalDatabase.Ins.mainChar.PlayEffect(0,0.5f);
                break;
            case HeaderProto.PCharState.PCharStateDead:
                
                break;
            default: 
                break;
        }
    }
}
