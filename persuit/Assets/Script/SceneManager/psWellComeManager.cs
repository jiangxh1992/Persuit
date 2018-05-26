﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    public Transform text = null;
    public float scrollSpeed = 50.0f;
    bool isMasking = false;
    public Image mask = null;

	void Start () {
        psUIRootManager.Ins.WellComeUI.SetActive(true);
        text = psUIRootManager.Ins.WellComeUI.transform.Find("Scroll View/Viewport/Content/Text");
        mask = psUIRootManager.Ins.transform.Find("mask").GetComponent<Image>();
        StartCoroutine(EnterMenu());
	}
	
	void Update () {
        text.Translate(new Vector2(0, 1) * Time.deltaTime * scrollSpeed);
        if (isMasking)
            mask.color += new Color(0, 0, 0,0.01f);
	}

    IEnumerator EnterMenu()
    {
        yield return new WaitForSeconds(2.0f);
        isMasking = true;
        yield return new WaitForSeconds(1.5f);
        psGlobalDatabase.Ins.isFistLogin = false;
        gameObject.AddComponent<psMenuManager>();
        Destroy(this);
        yield return 0;
    }
}