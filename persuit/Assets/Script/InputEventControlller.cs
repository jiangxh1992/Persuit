using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventControlller : Singleton<InputEventControlller> {

    public delegate void InputInputDelegate();

    public InputInputDelegate OnLeftDown;
    public InputInputDelegate OnRightDown;
    public InputInputDelegate OnLeftUp;
    public InputInputDelegate OnRightUp;
    public InputInputDelegate OnUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            this.OnLeftDown();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            this.OnRightDown();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.OnLeftUp();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.OnRightUp();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this.OnUp();
        }
	}
}
