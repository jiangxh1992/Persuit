using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    public Image mask = null;
	// Use this for initialization
	void Start () {
        psUIRootManager.Ins.WellComeUI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (mask.color.a > 0)
        {
            mask.color -= new Color(0.08f, 0.06f, 0.05f, 0.01f);
        }
        else {
            StartCoroutine(EnterGame());
        }
	}

    IEnumerator EnterGame() {
        yield return new WaitForSeconds(2.0f);
        psSceneManager.LoadScene("Menu");
        yield return 0;
    }
}
