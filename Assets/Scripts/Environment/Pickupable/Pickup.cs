using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField]
    private GameObject my_glow;

    private float timer;
    private float time;

	// Use this for initialization
	void Start () {
        timer = 0f;
        time = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > time) {
            Instantiate(my_glow, this.transform);
            timer = 0f;
        }
    }
}
