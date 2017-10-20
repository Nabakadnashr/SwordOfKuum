using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAudio : MonoBehaviour {

    AudioSource my_source;

    //FOOTSTEP SOUNDS
    [SerializeField]
    private AudioClip[] footsteps = new AudioClip[4];
    private System.Random foot_r = new System.Random();

    //SWORD SOUNDS
    [SerializeField]
    private AudioClip[] sword_swings = new AudioClip[4];
    private System.Random sword_r = new System.Random();

    // Use this for initialization
    void Start () {
        my_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play_footstep() {
        my_source.PlayOneShot(footsteps[foot_r.Next(footsteps.Length)]);
    }

    public void play_sword_swing() {
        my_source.PlayOneShot(sword_swings[sword_r.Next(sword_swings.Length)]);
    }
}
