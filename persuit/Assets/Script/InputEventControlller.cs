using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventControlller : Singleton<InputEventControlller> {

    public delegate void InputDelegate();

    public InputDelegate OnLeftDown = new InputDelegate(() => { });
    public InputDelegate OnRightDown = new InputDelegate(() => { });
    public InputDelegate OnLeftUp = new InputDelegate(() => { });
    public InputDelegate OnRightUp = new InputDelegate(() => { });
    public InputDelegate OnUpArrowDown = new InputDelegate(() => { });
    public InputDelegate OnUpArrowUp = new InputDelegate(() => { });

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (psGlobalDatabase.Ins.mainChar == null || !psGlobalDatabase.Ins.isGameStart) return;
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
