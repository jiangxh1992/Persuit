using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(GetComponent<BoxCollider2D>());
        transform.Find("sprite").gameObject.SetActive(false);
        transform.Find("effect").gameObject.SetActive(true);
    }
}
