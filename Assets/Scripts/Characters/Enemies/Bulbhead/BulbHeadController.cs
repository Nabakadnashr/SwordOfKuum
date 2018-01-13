using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbHeadController : MonoBehaviour {

    public enum STATE {
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
    private float change_dir_chance;

    private float acceleration;

    Vector2 dir;

    void Awake() {

        my_state = STATE.REST;

        my_movement = this.GetComponent<EnemyMovement>();
        my_movement.set_max_speed(2f);

        rest_time = 2.5f;
        timer_rest = Random.Range(0f, rest_time);

        walk_time = 7.5f;
        timer_walk = 0f;
        change_dir_chance = 0.01f;

        acceleration = 0.2f;

        dir = Vector2.zero;
    }

	// Use this for initialization
	void Start () {
        my_movement.set_acceleration(this.acceleration);
        my_movement.set_facing(EnemyMovement.FACING_DIRECTION.RIGHT);
    }

    // Update is called once per frame
    void Update () {

        switch (my_state) {
            case STATE.REST: {
                    timer_rest += Time.deltaTime;
                    if (timer_rest >= rest_time) {
                        timer_rest = 0f;
                        change_state(STATE.WALK);
                    }
                }break;
            case STATE.WALK: {
                    timer_walk += Time.deltaTime;

                    my_movement.move(dir);

                    if (Random.Range(0f, 1f) < change_dir_chance) {
                        dir = my_movement.get_direction_random();
                        Debug.Log("CHANGE");
                    }

                    if (timer_walk >= walk_time) {
                        timer_walk = 0f;
                        change_state(STATE.REST);
                    }
                }break;
        }
		
	}

    private void change_state(STATE new_state) {
        this.my_state = new_state;
        switch (new_state) {
            case STATE.REST: {
                    
                }break;
            case STATE.WALK: {
                    this.dir = my_movement.get_direction_random();
                }break;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        foreach (ContactPoint2D p in coll.contacts) {
            if (p.normal.x != 0 && Mathf.Sign(p.normal.x) != Mathf.Sign(dir.x)) {
                this.dir = new Vector2(-1 * this.dir.x, this.dir.y);
            }
            if (p.normal.y != 0 && Mathf.Sign(p.normal.y) != Mathf.Sign(dir.y)) {
                this.dir = new Vector2(this.dir.x, -1 * this.dir.y);
            }
        }
    }

    public Vector2 get_direction() {
        return this.dir;
    }

    public STATE get_state() {
        return this.my_state;
    }
}
