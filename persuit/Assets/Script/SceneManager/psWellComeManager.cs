using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    public Transform text = null;
	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.WellComeUI.SetActive(true);
        StartCoroutine(EnterGame());
	}
	
	// Update is called once per frame
	void Update () {
        text.position += new Vector3(0,0.3f,0);
	}

    IEnumerator EnterGame() {
        yield return new WaitForSeconds(15.0f);
        psSceneManager.LoadScene("Menu");
        yield return 0;
    }
}
