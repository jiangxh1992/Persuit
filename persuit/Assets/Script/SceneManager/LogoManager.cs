using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour {
    public GameObject Logo = null;
    public float logoDelay = 1.0f;
	void Start () {
        if (!psGlobalDatabase.Ins.isFistLogin) {
            Destroy(this);
            return;
        }
        Logo.SetActive(true);
        StartCoroutine(EnterWellCome());
	}
    IEnumerator EnterWellCome() {
        yield return new WaitForSeconds(logoDelay);
        psSceneManager.LoadScene("Menu");
        Logo.gameObject.SetActive(false);
        yield return 0;
    }
}
