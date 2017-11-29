using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisControl : MonoBehaviour {

    private BoxCollider2D my_collider;

	// Use this for initialization
	void Start () {
        my_collider = GetComponent<BoxCollider2D>();

        set_y();
	}
	
	// Update is called once per frame
	void Update () {
        set_y();
	}

    private void set_y() {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, ZSetting.get_z(this.my_collider));
    }
}
