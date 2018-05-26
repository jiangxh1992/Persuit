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
                break;
            case HeaderProto.PCharState.PCharStateJump:
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("jump");
                //psGlobalDatabase.Ins.mainChar.ChangeToIdleAfterDelay(0.5f);
                break;
            case HeaderProto.PCharState.PCharStateRun:
                psGlobalDatabase.Ins.mainChar.effect_dust.SetActive(true);
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("run");
                break;
            case HeaderProto.PCharState.PCharStateDead:
                psGlobalDatabase.Ins.mainChar.mAnimator.Play("dead");
                psGlobalDatabase.Ins.mainChar.effect_dust.SetActive(false);
                psGameLevelManager.Ins.OnGameOver();
                break;
            default: 
                break;
        }
    }
}
