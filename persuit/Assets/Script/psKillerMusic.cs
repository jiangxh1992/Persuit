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
        transform.position -= new Vector3(0,0.05f,0);
        float targetX = Mathf.Lerp(transform.position.x,psGlobalDatabase.Ins.mainChar.transform.position.x, 0.02f);
        transform.position = new Vector3(targetX,transform.position.y,transform.position.z);
        if(transform.position.y < -30.0f)
            Destroy(gameObject);
	}
}
