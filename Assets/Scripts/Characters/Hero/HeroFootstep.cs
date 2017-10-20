using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFootstep : MonoBehaviour {

    private SpriteRenderer my_renderer;
    private float life_time;
    private float life_timer;

    private Color my_color;

	// Use this for initialization
	void Start () {
        my_renderer = GetComponent<SpriteRenderer>();
        life_time = 3f;
        life_timer = 0f;
        my_color = my_renderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        life_timer += Time.deltaTime;
        my_color.a = 1f - (life_timer / life_time);
        my_renderer.color = my_color;
        if (life_timer > life_time) {
            Destroy(gameObject);
        }
	}
}
