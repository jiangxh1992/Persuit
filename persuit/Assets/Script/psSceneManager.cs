using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class psSceneManager {

    public static void LoadSceneProgress(string nextlevel) {
        psGlobalDatabase.Ins.curLevel = nextlevel;
        SceneManager.LoadScene("Progress");
    }

    public static void LoadScene(string level) {
        psGlobalDatabase.Ins.curLevel = level;
        SceneManager.LoadScene(level);
    }
}
