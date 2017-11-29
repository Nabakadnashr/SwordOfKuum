using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    SpriteRenderer my_renderer;

    [SerializeField]
    private Sprite[] my_sprites = new Sprite[8];
    private Random sprite_r;

	// Use this for initialization

    void Awake() {
        my_renderer = GetComponent<SpriteRenderer>();
        sprite_r = new Random();

        my_renderer.sprite = my_sprites[(int)Random.Range(0, 7)];
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
