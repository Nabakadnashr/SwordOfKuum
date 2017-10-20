using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPressPlay() {
        SceneManager.LoadScene("test1");
        MusicController mc = FindObjectOfType<MusicController>();
        mc.change_stage(MusicController.GAME_STAGES.IN_GAME);
    }
}
