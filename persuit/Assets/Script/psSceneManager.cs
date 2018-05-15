using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class psSceneManager : Singleton<psSceneManager> {

    public string nextLevel = null; //下一个要加载的场景
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadSceneProgress(string nextlevel) {
        nextLevel = nextlevel;
        SceneManager.LoadScene("Progress");
    }

    public static void LoadScene(string level) {
        SceneManager.LoadScene(level);
    }
}
