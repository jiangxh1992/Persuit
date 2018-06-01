using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class psWellComeManager : MonoBehaviour {
    float duration = 6.0f;
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
        psUIRootManager.Ins.WellComeUI.transform.Find("button_grey").GetComponent<Button>().onClick.AddListener(StartEnterMenu);
        StartCoroutine(StartScrollText());
	}
	
	void Update () {
        text.Translate(new Vector2(0, 1) * Time.deltaTime * scrollSpeed);
        if (isMaskingFade)
            mask.color += new Color(0, 0, 0,0.01f);
        if (!isMaskingFade && isMaskingShow && mask.color.a >0)
            mask.color -= new Color(0, 0, 0, 0.05f);
	}

    IEnumerator StartScrollText() {
        yield return new WaitForSeconds(duration);
        StartCoroutine(EnterMenu());
    }
    void StartEnterMenu() {
        StartCoroutine(EnterMenu());
    }

    IEnumerator EnterMenu()
    {
        isMaskingFade = true;
        yield return new WaitForSeconds(1.5f);
        psGlobalDatabase.Ins.isFistLogin = false;
        gameObject.AddComponent<psMenuManager>();
        Destroy(this);
        yield return 0;
    }
}