using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    public float duration = 12.0f;
    public Transform text = null;
    public float scrollSpeed = 50.0f;
    bool isMaskingShow = true;
    bool isMaskingFade = false;
    public Image mask = null;

	void Start () {
        psUIRootManager.Ins.WellComeUI.SetActive(true);
        text = psUIRootManager.Ins.WellComeUI.transform.Find("Scroll View/Viewport/Content/Text");
        mask = psUIRootManager.Ins.transform.Find("mask").GetComponent<Image>();
        mask.gameObject.SetActive(true);
        psUIRootManager.Ins.transform.Find("logo").gameObject.SetActive(false);
        StartCoroutine(EnterMenu());
	}
	
	void Update () {
        text.Translate(new Vector2(0, 1) * Time.deltaTime * scrollSpeed);
        if (isMaskingFade)
            mask.color += new Color(0, 0, 0,0.01f);
        if (!isMaskingFade && isMaskingShow && mask.color.a >0)
            mask.color -= new Color(0, 0, 0, 0.05f);
	}

    IEnumerator EnterMenu()
    {
        yield return new WaitForSeconds(duration);
        isMaskingFade = true;
        yield return new WaitForSeconds(1.5f);
        psGlobalDatabase.Ins.isFistLogin = false;
        gameObject.AddComponent<psMenuManager>();
        Destroy(this);
        yield return 0;
    }
}