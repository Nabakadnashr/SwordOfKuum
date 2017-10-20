using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static float get_z(Collider2D coll) {
        float factor = 0.01f;
        Vector2 position = coll.bounds.center;

        return position.y * factor;
    }
}
