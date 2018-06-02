using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psKillerMusic : MonoBehaviour {

    public HeaderProto.PKillerType mType = HeaderProto.PKillerType.PKillerTypeMusic;
	// Use this for initialization
	void Start () {
        if (mType == HeaderProto.PKillerType.PKillerTypeMusic)
        {
            GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.music_sprite[Random.Range(0, psGlobalDatabase.Ins.music_sprite.Length)];
        }
        else {
            GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.monster_bullet;
        }
	}
	
    // Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0,0.1f,0);
        float targetX = Mathf.Lerp(transform.position.x,psGlobalDatabase.Ins.mainChar.transform.position.x, 0.02f);
        transform.position = new Vector3(targetX,transform.position.y,transform.position.z);
        if(transform.position.y < -30.0f)
            Destroy(gameObject);
	}
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }

    public void SetKillerType(HeaderProto.PKillerType type){
        if (mType == HeaderProto.PKillerType.PKillerTypeMonster) {
            GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.monster_bullet;
        }
    }
}
