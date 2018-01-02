using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbHeadController : MonoBehaviour {

    private enum STATE {
        REST,
        WALK
    }

    private STATE my_state;

    private EnemyMovement my_movement;

    private Random my_random;

    private float timer_rest;
    private float rest_time;

    private float timer_walk;
    private float walk_time;

    private float acceleration;

    Vector2 dir;

    void Awake() {
        my_state = STATE.REST;

        my_movement = this.GetComponent<EnemyMovement>();
        my_movement.set_max_speed(2f);

        timer_rest = 0f;
        rest_time = 1.5f;

        timer_walk = 0f;
        walk_time = 1f;

        acceleration = 0.2f;

        dir = Vector2.zero;
    }

	// Use this for initialization
	void Start () {
        my_movement.set_acceleration(this.acceleration);
    }

    // Update is called once per frame
    void Update () {

        switch (my_state) {
            case STATE.REST: {
                    timer_rest += Time.deltaTime;
                    if (timer_rest >= rest_time) {
                        timer_rest = 0f;
                        dir = my_movement.get_direction((int)Random.Range(0, 7));
                        my_state = STATE.WALK;
                        Debug.Log("TIME TO WALK");
                    }
                }break;
            case STATE.WALK: {
                    timer_walk += Time.deltaTime;

                    my_movement.move(dir);

                    if (timer_walk >= walk_time) {
                        timer_walk = 0f;
                        my_state = STATE.REST;
                        Debug.Log("WHEW, TIME TO REST");
                    }
                }break;
        }
		
	}
}
