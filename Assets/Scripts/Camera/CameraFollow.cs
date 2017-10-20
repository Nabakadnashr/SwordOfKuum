using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    private Camera my_camera;

    private float BOUND_LEFT;
    private float BOUND_RIGHT;
    private float BOUND_UP;

	// Use this for initialization
	void Start () {
        my_camera = GetComponent<Camera>();

        BOUND_LEFT = -73f;
        BOUND_RIGHT = 73f;
        BOUND_UP = 84;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        if (this.transform.position.x < BOUND_LEFT) {
            this.transform.position = new Vector3(BOUND_LEFT, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > BOUND_RIGHT) {
            this.transform.position = new Vector3(BOUND_RIGHT, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y > BOUND_UP) {
            this.transform.position = new Vector3(this.transform.position.x, BOUND_UP, this.transform.position.z);
        }

    }
}
