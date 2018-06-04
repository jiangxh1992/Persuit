using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psKillerMusic : MonoBehaviour {

    public HeaderProto.PKillerType mType = HeaderProto.PKillerType.PKillerTypeMusic;
	// Use this for initialization
	void Start () {
        if (mType == HeaderProto.PKillerType.PKillerTypeMusic)
        {
            transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.music_sprite[Random.Range(0, psGlobalDatabase.Ins.music_sprite.Length)];
        }
        else {
            transform.Find("sprite").GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.monster_bullet;
        }
	}
	
    // Update is called once per frame
	void Update () {
        //transform.position -= new Vector3(0,0.1f,0);
        float targetX = psGlobalDatabase.Ins.mainChar.transform.position.x;
        targetX = targetX > transform.position.x ? 0.05f : -0.05f;
        transform.position += new Vector3(targetX,0,0);
        if(transform.position.y < -30.0f)
            Destroy(gameObject);
	}
    void OnCollisionEnter2D(Collision2D collision) {
        string objname = collision.gameObject.name;
        StartCoroutine(OnBoom());
        if (objname == "MainCharacter(Clone)") {
            psGlobalDatabase.Ins.mainChar.OnDead();
        }
        else if (objname == "platform") { 
        }
    }

    IEnumerator OnBoom() {
        transform.Find("sprite").gameObject.SetActive(false);
        transform.Find("effect").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void SetKillerType(HeaderProto.PKillerType type){
        if (mType == HeaderProto.PKillerType.PKillerTypeMonster) {
            GetComponent<SpriteRenderer>().sprite = psGlobalDatabase.Ins.monster_bullet;
        }
    }
}
