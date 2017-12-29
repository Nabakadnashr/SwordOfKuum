using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

    public enum GAME_STAGES {
        MENU,
        IN_GAME
    };

    private GAME_STAGES stage;

    [SerializeField]
    private AudioMixer my_mixer;

    private AudioSource my_source;

    [SerializeField]
    private AudioClip warriors_of_light;
    [SerializeField]
    private AudioClip solar_tanjent;
    [SerializeField]
    private AudioClip _33hz;
    [SerializeField]
    private AudioClip hog_jank_ii;

    private float quiet_time;
    private float timer;

    private AudioClip[] play_tracks;
    private int tracks_ind;

    void Awake() {
        stage = GAME_STAGES.MENU;

        my_source = GetComponent<AudioSource>();
        quiet_time = 3f;
        timer = 0f;

        play_tracks = new AudioClip[] { hog_jank_ii, _33hz, solar_tanjent, warriors_of_light };
        tracks_ind = 0;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        switch (stage) {
            case GAME_STAGES.MENU: {

                }
                break;
            case GAME_STAGES.IN_GAME: {
                    if (!my_source.isPlaying) {
                        timer += Time.deltaTime;
                        if (timer >= quiet_time) {
                            my_source.PlayOneShot(play_tracks[(tracks_ind++) % 2]);
                            timer = 0f;
                        }
                    }
                }
                break;
        }
		
	}

    public void change_stage(GAME_STAGES st) {
        stage = st;
        switch (st) {
            case GAME_STAGES.MENU: {

                }break;
            case GAME_STAGES.IN_GAME: {
                    timer = quiet_time;
                }break;
        }
    }
}
