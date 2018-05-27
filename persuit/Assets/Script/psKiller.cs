using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psKiller : MonoBehaviour {
    [Tooltip("出现周期")]
    public int period = 200;
    [Tooltip("真正发生概率")]
    [Range(0,1.0f)]
    public float probability = 0.8f;
    [Tooltip("停留时间")]
    public float stayTime = 2.0f;
    [Tooltip("上升高度")]
    public float height = 1.0f;
    [Tooltip("动画速度")]
    public float moveSpeed = 0.01f;
    public AudioSource mAudioSource = null;

    float timeTick = 0;
    float initHeight = 0;
    int isAction = 0;
    bool isVisible = false;

	void Start () {
        initHeight = transform.position.y;
        mAudioSource = GetComponent<AudioSource>();
	}

    void OnBecameVisible()
    {
        isVisible = true;
    }
    void OnBecameInvisible()
    {
        isVisible = false;
    }
	
	void Update () {
        if (!isVisible) return;
        if (isAction == 0) {
            timeTick++; // 蓄力
            if (timeTick >= period)
            {
                timeTick = 0;
                float random = Random.Range(0, 1.0f);
                if (random <= probability)
                {
                    isAction = 1; // 开始网上冒
                    timeTick = 0;
                    mAudioSource.Play();
                }
            }
        }

        if (isAction == 1) { // 上
            if (transform.position.y < initHeight + height)
            {
                transform.position += new Vector3(0, moveSpeed, 0);
            }
            else {
                StartCoroutine(DelayDown());
            }
        }

        if (isAction == 2) {
            if (transform.position.y > initHeight)
            {
                transform.position -= new Vector3(0, moveSpeed, 0);
            }
            else
            {
                isAction = 0;// 蓄力
            }
        }
	}

    IEnumerator DelayDown() {
        yield return new WaitForSeconds(stayTime);
        isAction = 2;// 下
        yield return 0;
    }
}

