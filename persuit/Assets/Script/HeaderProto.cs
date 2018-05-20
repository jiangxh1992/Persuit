using System.Collections;
using System.Collections.Generic;

public class HeaderProto {
    public enum PCharState{
        PCharStateIdle = 0,
        PCharStateRun = 1,
        PCharStateJumpUp = 2,
        PCharStateJumpDown = 3,
        PCharStateDead = 4,
    }

    public enum PGameLevelType {
        PGameLevelTypeLevel1 = 1,
        PGameLevelTypeLevel2 = 2,

        PGameLevelTypeWellCome = 10,
        PGameLevelTypeMenu = 11,
    }
}
