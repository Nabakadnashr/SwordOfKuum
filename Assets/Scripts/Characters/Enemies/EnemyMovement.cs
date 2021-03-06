﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public enum FACING_DIRECTION {
        UP = -1,
        LEFT = -2,
        RIGHT = 2,
        DOWN = 1
    }

    private List<Vector2> DIRECTION_LIST = new List<Vector2> {
        new Vector2(1f, 0f),
        (new Vector2(1f, 1f)).normalized,
        new Vector2(0f, 1f),
        (new Vector2(-1f, 1f)).normalized,
        new Vector2(-1f, 0f),
        (new Vector2(-1f, -1f)).normalized,
        new Vector2(0f, -1f),
        (new Vector2(1f, -1f)).normalized
    };

    private Rigidbody2D my_rigid_body;
    private Collider2D my_collider;

    private FACING_DIRECTION facing;

    private float velocity_x;
    private float velocity_y;

    private float acceleration = 0f;

    private float max_speed;

	// Use this for initialization
	void Start () {
        my_rigid_body = this.GetComponent<Rigidbody2D>();
        my_collider = this.GetComponent<Collider2D>();

        velocity_x = 0f;
        velocity_y = 0f;

        //acceleration = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, ZSetting.get_z(my_collider));

        facing = determine_facing(new Vector2(velocity_x, velocity_y));

        if (velocity_x != 0f) {
            velocity_x = damp_velocity(velocity_x);
        }
        if (velocity_y != 0f) {
            velocity_y = damp_velocity(velocity_y);
        }
		
	}

    void FixedUpdate() {
        my_rigid_body.velocity = new Vector2(velocity_x, velocity_y);
    }

    private float damp_velocity(float velocity) {
        float new_vel;
        float drag = acceleration / 2f;

        int temp_sign = (int)Mathf.Sign(velocity);
        new_vel = velocity - temp_sign * drag;
        if (temp_sign != (int)Mathf.Sign(new_vel)) {
            return 0f;
        }
        else {
            return new_vel;
        }
    }

    private FACING_DIRECTION determine_facing(Vector2 vels) {
        if (vels.y > 0f) {
            return FACING_DIRECTION.UP;
        }
        if (vels.y < 0f) {
            return FACING_DIRECTION.DOWN;
        }
        if (vels.x > 0f) {
            return FACING_DIRECTION.RIGHT;
        }
        if (vels.x < 0f) {
            return FACING_DIRECTION.LEFT;
        }

        return this.facing;
    }

    public void set_max_speed(float amount) {
        this.max_speed = amount;
    }

    public void set_acceleration(float val) {
        this.acceleration = val;
    }

    public void move(Vector2 direction) {
        this.velocity_x += this.acceleration * direction.x;
        this.velocity_y += this.acceleration * direction.y;

        if (new Vector2(this.velocity_x, this.velocity_y).magnitude > this.max_speed) {
            this.velocity_x = this.max_speed * direction.x;
            this.velocity_y = this.max_speed * direction.y;
        }

    }

    public Vector2 get_direction(int index) {
        return this.DIRECTION_LIST[index];
    }

    public Vector2 get_direction_random() {
        return this.DIRECTION_LIST[(int)Random.Range(0, 7)];
    }

    public FACING_DIRECTION get_facing() {
        return this.facing;
    }

    public void set_facing(FACING_DIRECTION dir) {
        this.facing = dir;
    }
}
