using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadAfter : MonoBehaviour {

    public string scene_to_load;

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(scene_to_load);
	}
	
}
