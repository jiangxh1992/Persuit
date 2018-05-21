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

    float timeTick = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timeTick++;
        if (timeTick >= period) {
            timeTick = 0;
            float random = Random.Range(0, 1.0f);
            if (random <= probability)
                OnAction();
        }
	}
    void OnAction() {
        transform.position += new Vector3(0,height,0);
        StartCoroutine(ReverseAction());
    }

    IEnumerator ReverseAction() {
        yield return new WaitForSeconds(stayTime);
        transform.position -= new Vector3(0, height, 0);
        yield return 0;
    }
}
