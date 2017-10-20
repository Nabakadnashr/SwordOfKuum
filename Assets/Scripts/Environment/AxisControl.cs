using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisControl : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    private SpriteRenderer player_renderer;

    private BoxCollider2D my_collider;

    private SpriteRenderer my_renderer;

	// Use this for initialization
	void Start () {
        my_collider = GetComponent<BoxCollider2D>();
        player_renderer = player.GetComponent<SpriteRenderer>();
        my_renderer = GetComponent<SpriteRenderer>();

        set_y();
	}
	
	// Update is called once per frame
	void Update () {
        set_y();
        /*
        if (player.transform.position.y < my_collider.bounds.center.y) {
            if (player_renderer.sortingOrder < my_renderer.sortingOrder) {
                my_renderer.sortingOrder = player_renderer.sortingOrder - 1;
            }
        }

        if (player.transform.position.y > my_collider.bounds.center.y) {
            if (player_renderer.sortingOrder > my_renderer.sortingOrder) {
                my_renderer.sortingOrder = player_renderer.sortingOrder + 1;
            }
        }
        */
	}

    private void set_y() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, ZSetting.get_z(this.my_collider));
    }
}
