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
                psGlobalDatabase.Ins.mainChar.SetAnimationSate(0); break;
            case HeaderProto.PCharState.PCharStateJumpUp:
                psGlobalDatabase.Ins.mainChar.SetAnimationSate(2);
                psGlobalDatabase.Ins.mainChar.ChangeToIdleAfterDelay(0.1f);
                break;
            case HeaderProto.PCharState.PCharStateJumpDown:
                break;
            case HeaderProto.PCharState.PCharStateRun:
                psGlobalDatabase.Ins.mainChar.SetAnimationSate(1);break;
            case HeaderProto.PCharState.PCharStateDead:
                //psGlobalDatabase.Ins.mainChar.SetAnimationSate(100);
                psGameLevelManager.Ins.OnGameOver();
                break;
            default: 
                break;
        }
    }
}
