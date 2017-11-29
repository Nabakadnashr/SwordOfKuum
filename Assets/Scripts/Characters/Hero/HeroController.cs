using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public enum DIRECTION {
        LEFT = -1,
        RIGHT = 1,
        UP = 2,
        DOWN = -2
    }

    public enum STATE {
        DEAD,
        ACTIVE,
        LOCKED,
    }

    private DIRECTION facing;
    private STATE my_state;

    private Rigidbody2D my_rigid;
    private Collider2D my_collider;

    private float velocity_x;
    private float velocity_y;
    private float acceleration;

    private float walk_speed;

    private int input_x;
    private int input_y;

	// Use this for initialization
	void Start () {
        facing = DIRECTION.RIGHT;
        my_state = STATE.ACTIVE;

        my_rigid = GetComponent<Rigidbody2D>();
        my_collider = GetComponent<Collider2D>();

        velocity_x = 0f;
        velocity_y = 0f;
        acceleration = 1.5f;

        walk_speed = 8f;

        input_x = 0;
        input_y = 0;
	}
	
	// Update is called once per frame
	void Update () {

        input_x = (int)Input.GetAxisRaw("Horizontal");
        input_y = (int)Input.GetAxisRaw("Vertical");

        this.transform.position = new Vector3(transform.position.x, transform.position.y, ZSetting.get_z(my_collider));

        if (my_state != STATE.DEAD){

            if (my_state != STATE.LOCKED) {

                float walk_speed_aux = walk_speed;

                facing = this.determine_direction();

                velocity_x += acceleration * input_x;
                velocity_y += acceleration * input_y;

                if (input_x * input_y != 0) {
                    walk_speed_aux = walk_speed * Mathf.Cos(Mathf.PI / 4f);
                }

                if (Mathf.Abs(velocity_x) > walk_speed_aux) {
                    velocity_x = Mathf.Sign(velocity_x) * walk_speed_aux;
                }

                if (Mathf.Abs(velocity_y) > walk_speed_aux) {
                    velocity_y = Mathf.Sign(velocity_y) * walk_speed_aux;
                }

            }
            else {



            }

        }
        else {



        }

        if (velocity_x != 0) {
            velocity_x = damp_velocity(velocity_x);
        }
        if (velocity_y != 0) {
            velocity_y = damp_velocity(velocity_y);
        }

        set_scale(facing);

        //transform.position += (Vector3)new Vector2(velocity_x, velocity_y);

	}

    void FixedUpdate() {
        my_rigid.velocity = new Vector2(velocity_x, velocity_y);
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        foreach (ContactPoint2D p in coll.contacts) {
            if (p.normal.x != 0) {
                velocity_x = 0f;
            }
            if (p.normal.y != 0) {
                velocity_y = 0f;
            }
        }
    }

    private DIRECTION determine_direction() {
        DIRECTION d = facing;
        if (input_x < 0) {
            d = DIRECTION.LEFT;
        }
        if (input_x > 0) {
            d = DIRECTION.RIGHT;
        }
        if (input_y < 0) {
            d = DIRECTION.DOWN;
        }
        if (input_y > 0) {
            d = DIRECTION.UP;
        }

        return d;
    }

    private void set_scale(DIRECTION dir) {
        int s = (int)Mathf.Sign((int)dir);
        Vector3 scale = this.transform.localScale;
        scale.x = s;
        this.transform.localScale = scale;
    }

    private float damp_velocity(float vel) {
        float new_vel;
        float drag = this.acceleration / 2f;

        int temp_sing = (int)Mathf.Sign(vel);
        new_vel = vel - temp_sing * drag;
        if (temp_sing != (int)Mathf.Sign(new_vel)) {
            return 0;
        }
        else {
            return new_vel;
        }
    }

    public STATE get_state() {
        return this.my_state;
    }

    public void set_state(STATE st) {
        this.my_state = st;
    }

    public float get_velocity_x() {
        return this.velocity_x;
    }

    public void set_velocity_x(float val) {
        this.velocity_x = val;
    }

    public float get_velocity_y() {
        return this.velocity_y;
    }

    public void set_velocity_y(float val) {
        this.velocity_y = val;
    }

    public int get_input_x() {
        return this.input_x;
    }

    public int get_input_y() {
        return this.input_y;
    }

    public DIRECTION get_facing() {
        return this.facing;
    }
}
