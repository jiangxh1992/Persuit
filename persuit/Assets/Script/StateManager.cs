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
                MainCharacter.Ins.SetAnimationSate(0); break;
            case HeaderProto.PCharState.PCharStateJumpUp:
                MainCharacter.Ins.SetAnimationSate(2); break;
            case HeaderProto.PCharState.PCharStateJumpDown:
                MainCharacter.Ins.SetAnimationSate(1); break;
            case HeaderProto.PCharState.PCharStateRun:
                MainCharacter.Ins.SetAnimationSate(1);break;
            case HeaderProto.PCharState.PCharStateDead:
                MainCharacter.Ins.SetAnimationSate(100);
                MainCharacter.Ins.OnDead();
                break;
            default: 
                break;
        }
    }
}
