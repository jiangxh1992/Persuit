using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class psProgressManager : MonoBehaviour
{
    //读取场景的进度，它的取值范围在0 - 1 之间。
    private int progress = 0;

    //进度UI
    public Text sliderText = null;
    public Slider slider = null;

    //异步对象
    public AsyncOperation async = null;

    private void Start()
    {
        slider.value = 0.0f;
        StartCoroutine(loadScene());
    }

    private void Update()
    {
        if (psSceneManager.Ins.nextLevel == null)
            return;
        if (sliderText)
        {
            progress = (int)(async.progress * 100);
            sliderText.text = "正在玩儿命加载..." + progress + "%";
            slider.value = async.progress;
        }
    }

    //注意这里返回值一定是IEnumerator
    private IEnumerator loadScene()
    {
        //异步读取场景
        async = SceneManager.LoadSceneAsync(psSceneManager.Ins.nextLevel);
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;
    }
}