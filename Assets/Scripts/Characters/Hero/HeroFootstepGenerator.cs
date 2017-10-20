using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFootstepGenerator : MonoBehaviour {

    public enum STEP {
        LEFT,
        RIGHT
    }

    [SerializeField]
    private GameObject footstep_object;

    [SerializeField]
    private Vector2 left_pos;
    [SerializeField]
    private Vector2 right_pos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void make_footstep(STEP st) {
        GameObject obj = Instantiate(footstep_object);
        switch (st) {
            case STEP.LEFT: {
                    obj.transform.position = this.transform.position + (Vector3)left_pos;
                }break;
            case STEP.RIGHT: {
                    obj.transform.position = this.transform.position + (Vector3)right_pos;
                }break;
        }
    }
}
