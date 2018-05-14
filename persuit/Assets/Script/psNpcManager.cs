﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psNpcManager : MonoBehaviour {
    public Animator mAnimator = null;

	// Use this for initialization
	void Start () {
		mAnimator = transform.FindChild("rendernode").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnWakeUp() {
        mAnimator.SetInteger("NpcState",1);
        StartCoroutine(DelayChangeToIdle(0.2f));
    }
    IEnumerator DelayChangeToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        mAnimator.SetInteger("NpcState", 0);
        yield return 0;
    }
}