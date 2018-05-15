using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psWellComeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(EnterGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator EnterGame() {
        yield return new WaitForSeconds(2.0f);
        psSceneManager.LoadScene("Menu");
        yield return 0;
    }
}
