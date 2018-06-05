using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psButterFly : MonoBehaviour {
    Vector3 tarPos = Vector3.zero;
    float initZ = 0;
    float left = 10.0f;
    float right = 30.0f;
    float top = 10.0f;
    float bottom = 10.0f;
	// Use this for initialization
	void Start () {
        initZ = transform.position.z;
        //left = transform.position.x -left;
        //right = transform.position.x + right;
        //bottom = transform.position.y - bottom;
        //top = transform.position.y + top;

        tarPos = new Vector3(Random.Range(left, right), Random.Range(bottom, top), initZ);
        ChangeDirection();
        //iTween.MoveTo(gameObject, tarPos, 10.0f);
        StartCoroutine(RandomTarPos());
	}
	
	// Update is called once per frame
	void Update () {
        float targetY = Mathf.Lerp(transform.position.y,tarPos.y,0.002f);
        float targetX = Mathf.Lerp(transform.position.x,tarPos.x,0.002f);
        transform.position = new Vector3(targetX, targetY, initZ);
        
	}
    IEnumerator RandomTarPos() {
        while (true) {
            if (Random.Range(0, 1.0f) > 0.5) {
                Vector3 cameraPos = psGameLevelManager.Ins.gameCamera.transform.position;
                tarPos = new Vector3(Random.Range(cameraPos.x - left, cameraPos.x + right), Random.Range(cameraPos.y - bottom, cameraPos.y + top), initZ);
                ChangeDirection();
            }
            yield return new WaitForSeconds(3.0f);
        }
        yield return 0;
    }

    void ChangeDirection() {
        if (tarPos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
