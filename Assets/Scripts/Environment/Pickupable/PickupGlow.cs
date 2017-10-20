using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGlow : MonoBehaviour {

    private Vector2 scale_goal = new Vector2(3f, 3f);
    private float scale_rate;

    private SpriteRenderer my_renderer;

	// Use this for initialization
	void Start () {
        my_renderer = GetComponent<SpriteRenderer>();

        scale_rate = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = Vector2.Lerp(this.transform.localScale, scale_goal, scale_rate);
        Color my_c = my_renderer.color;
        my_c.a = 1f - (this.transform.localScale.magnitude / scale_goal.magnitude);
        my_renderer.color = my_c;

        if (my_renderer.color.a < 0.001f) {
            Destroy(this.gameObject);
        }
	}
}
