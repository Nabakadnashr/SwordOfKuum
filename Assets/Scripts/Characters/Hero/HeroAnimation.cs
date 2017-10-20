using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour {

    private Animator my_animator;

    private HeroController control_script;

    private string anim;

	// Use this for initialization
	void Start () {
        my_animator = GetComponent<Animator>();
        control_script = GetComponent<HeroController>();

        anim = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (control_script.get_state() != HeroController.STATE.DEAD) {
            if (control_script.get_state() != HeroController.STATE.LOCKED) {

                anim = "Idle";

                if (control_script.get_input_x() != 0) {
                    anim = "Run_H";
                }
                if (control_script.get_input_y() != 0) {
                    anim = "Run_V";
                }

            }
            else {

            }
        }
        else {

        }

        animate(anim);
	}

    private void animate(string a) {
        this.my_animator.Play(a);
    }

    public void set_anim(string a) {
        this.anim = a;
    }
}
