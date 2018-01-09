using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbHeadAnimation : MonoBehaviour {

    private Animator my_animator;
    private EnemyMovement my_movement;
    private BulbHeadController my_controller;

    private string my_animation;

	// Use this for initialization
	void Start () {
        my_animator = GetComponent<Animator>();
        my_movement = GetComponent<EnemyMovement>();
        my_controller = GetComponent<BulbHeadController>();

        my_animation = "Idle";
	}
	
	// Update is called once per frame
	void Update () {

        switch (my_controller.get_state()) {
            case BulbHeadController.STATE.REST: {
                    my_animation = "Idle";
                }break;
            case BulbHeadController.STATE.WALK: {
                    my_animation = "Walk";
                }break;
        }


        animate(my_animation);
        set_scale(my_movement.get_facing());
	}

    private void set_scale(EnemyMovement.FACING_DIRECTION dir) {
        int s = (int)Mathf.Sign((int)dir);
        Vector3 new_scale = this.transform.localScale;
        new_scale.x = s;
        this.transform.localScale = new_scale;
    }

    private void animate(string anim) {

        if (anim == "Walk") {
            switch (my_movement.get_facing()) {
                case EnemyMovement.FACING_DIRECTION.UP: {
                        anim += "_VU";
                    }break;
                case EnemyMovement.FACING_DIRECTION.DOWN: {
                        anim += "_VD";
                    }break;
                default: {
                        anim += "_H";
                    }break;
            }
        }

        this.my_animator.Play(anim);

    }
}
