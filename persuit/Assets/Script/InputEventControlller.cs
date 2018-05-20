using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventControlller : Singleton<InputEventControlller> {

    public delegate void InputInputDelegate();

    public InputInputDelegate OnLeftDown;
    public InputInputDelegate OnRightDown;
    public InputInputDelegate OnLeftUp;
    public InputInputDelegate OnRightUp;
    public InputInputDelegate OnUpArrowDown;
    public InputInputDelegate OnUpArrowUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.mainChar == null || psGlobalDatabase.Ins.mainChar.mStateManager.mCurState == HeaderProto.PCharState.PCharStateDead) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            OnLeftDown();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            OnRightDown();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnLeftUp();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnRightUp();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            OnUpArrowDown();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            OnUpArrowUp();
        }
	}
}
