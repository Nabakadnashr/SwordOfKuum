using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {

    private HeroController control_script;
    private HeroAnimation anim_script;

    private string[] attack_suffix = new string[2] {"A", "B"};
    private int attack_ind;
    private string attack_state;

    private float attack_push;
    private bool in_attack;

    private int attack_cost;
    private int attack_damage;

    private bool attack_queue;
    private int attack_stack;

    private Inventory inv;

    void Awake() {
        inv = FindObjectOfType<Inventory>();
        inv.add_listener(this.gameObject);
    }

	// Use this for initialization
	void Start () {
        inv.add_listener(this.gameObject);

        control_script = GetComponent<HeroController>();
        anim_script = GetComponent<HeroAnimation>();

        attack_ind = 0;
        attack_state = attack_suffix[0];

        attack_push = 5f;
        in_attack = false;

        attack_queue = false;

        attack_stack = 0;

        listener_update();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(attack_stack);

		if (control_script.get_state() != HeroController.STATE.DEAD) {
            if (Input.GetKeyDown(HeroKeys.ATTACK_KEY)) {
                on_attack();
            }

            if (in_attack) {
                anim_script.set_anim(adjust_anim("Attack"));
            }
        }
        else {

        }
	}

    private void on_attack() {

        if (HeroStamina.get_stamina() < (attack_cost * (attack_stack + 1))) {
            StaminaPoints sps = Object.FindObjectOfType<StaminaPoints>();
            if (HeroStamina.get_stamina() == 0) {
                sps.shake_points();
            }
            //attack_stack--;
            return;
        }

        if (control_script.get_state() != HeroController.STATE.LOCKED) {
            in_attack = true;
            attack_queue = false;
            control_script.set_state(HeroController.STATE.LOCKED);
            attack_ind = 0;
            attack_stack++;
        }
        else {
            if (in_attack) {
                if (!attack_queue) {
                    attack_stack++;
                }
                attack_queue = true;
            }

        }

    }

    private string adjust_anim(string a) {
        switch (control_script.get_facing()) {
            case HeroController.DIRECTION.DOWN: {
                    return (a + "_DOWN_" + attack_suffix[attack_ind]);
                }
            case HeroController.DIRECTION.UP: {
                    return (a + "_UP_" + attack_suffix[attack_ind]);
                }
            default: {
                    return (a + "_H_" + attack_suffix[attack_ind]);
                }
        }
    }

    public void exit_attack() {
        this.in_attack = false;
        this.control_script.set_state(HeroController.STATE.ACTIVE);
        attack_stack = 0;
    }

    public void attack_next() {
        if (attack_queue) {
            attack_ind = (attack_ind + 1) % attack_suffix.Length;
            attack_queue = false;
        }
    }

    public void push() {

        if (this.control_script.get_facing() == HeroController.DIRECTION.DOWN || this.control_script.get_facing() == HeroController.DIRECTION.UP) {
            this.control_script.set_velocity_y(this.attack_push * (int)Mathf.Sign((int)this.control_script.get_facing()));
        }
        else {
            this.control_script.set_velocity_x(this.attack_push * (int)this.control_script.get_facing());
        }

        this.gameObject.SendMessage("play_sword_swing");

        if (attack_stack > 0) {
            HeroStamina.decrease_stamina(attack_cost);
            attack_stack--;
        }

    }

    public void listener_update() {
        WeaponObject w = inv.get_active_weapon();
        attack_cost = w.get_stamina_cost();
        attack_damage = w.get_damage();
    }
}
