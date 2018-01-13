using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbHeadAudio : MonoBehaviour {

    [SerializeField]
    private AudioClip[] footstep_sounds = new AudioClip[4];

    private AudioSource my_source;

	// Use this for initialization
	void Start () {
        my_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play_footstep() {

        AudioClip to_play = this.footstep_sounds[(int)(Random.Range(0, this.footstep_sounds.Length - 1))];

        this.my_source.PlayOneShot(to_play);

    }
}
