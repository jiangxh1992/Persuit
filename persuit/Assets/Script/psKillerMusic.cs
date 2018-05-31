using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psKillerMusic : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.music_sprite[Random.Range(0, psGlobalDatabase.Ins.music_sprite.Length)];
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0,0.1f,0);
        if(transform.position.y < -30.0f)
            Destroy(gameObject);
	}
}
