using System.Collections;
using System.Collections.Generic;

public class HeaderProto {
    public enum PCharState{
        PCharStateIdle = 0,
        PCharStateRun = 1,
        PCharStateJump = 2,
        PCharStateDead = 3,
    }

    public enum PGameLevelType {
        PGameLevelTypeLevel1 = 1,
        PGameLevelTypeLevel2 = 2,

        PGameLevelTypeWellCome = 10,
        PGameLevelTypeMenu = 11,
    }

    public enum PNpcType {
        PNpcTypeNormal = 1,
        PNpcTypeAttack = 2,
        PNpcTypeMonster = 3,
    }

    public enum PKillerType {
        PKillerTypeMusic = 1,
        PKillerTypeMonster = 2,
    }
}
