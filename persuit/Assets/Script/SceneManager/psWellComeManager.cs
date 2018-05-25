using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    public Transform text = null;
    bool isMasking = false;
    public Image mask = null;
	// Use this for initialization
	void Start () {
        StartCoroutine(EnterGame());
	}
	
	// Update is called once per frame
	void Update () {
        text.position += new Vector3(0,0.3f,0);
        if (isMasking)
            mask.color += new Color(0, 0, 0,0.01f);
	}

    IEnumerator EnterGame() {
        yield return new WaitForSeconds(12.0f);
        isMasking = true;
        yield return new WaitForSeconds(3.0f);
        psSceneManager.LoadScene("Menu");
        yield return 0;
    }
}