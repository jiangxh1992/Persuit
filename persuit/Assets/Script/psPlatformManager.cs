using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psPlatformManager : Singleton<psPlatformManager> {
    public bool isFrontLayerMoving = false;
    float moveSpeed = 5f;
    float moveDistance = 5;
    float movedDistance = 0;

    public float deslevel1 = -163.0f;
    public float deslevel2 = -225.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isFrontLayerMoving) {
            float move = Time.deltaTime * moveSpeed;
            transform.Translate(new Vector2(-1, 0) * move);
            movedDistance += move;
            if (movedDistance > moveDistance) {
                isFrontLayerMoving = false;
                movedDistance = 0;
            }
        }
	}

    public void OnFrontLayerMove(){

    }
}
