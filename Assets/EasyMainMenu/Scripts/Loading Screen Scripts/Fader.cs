using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {
    string sceneToLoad;
	// Use this for initialization
	void Start () {
		
	}

    /// <summary>
    /// Call this method whenever you want to load a scene with a fader :)
    /// </summary>
    /// <param name="sceneName">Scene to Load</param>
    public void FadeIntoLevel(string sceneName) {

        SceneManager.LoadSceneAsync(sceneName).allowSceneActivation = false ;

        sceneToLoad = sceneName;

        GetComponent<Animator>().Play("Fader In");
        Invoke("load", 1f);
    }

    void load()
    {
        //finally load scene
        SceneManager.LoadScene(sceneToLoad);

    }
}
