using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public enum STATE {
        ACTIVE,
        INACTIVE
    };

    private CanvasGroup inventory_group;

    private STATE my_state;

    private float fade_rate;

	// Use this for initialization
	void Start () {
        inventory_group = GetComponent<CanvasGroup>();
        my_state = STATE.INACTIVE;
        fade_rate = 0.2f;

        inventory_group.alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        switch (my_state) {
            case STATE.ACTIVE: {
                    if (inventory_group.alpha != 1f) {
                        inventory_group.alpha = fade(inventory_group.alpha, 1f, fade_rate);
                    }
                }
                break;
            case STATE.INACTIVE: {
                    if (inventory_group.alpha != 0f) {
                        inventory_group.alpha = fade(inventory_group.alpha, 0f, fade_rate);
                    }
                }break;
        }
		
	}

    private float fade(float start_point, float end_point, float rate) {
        float d = Mathf.Abs(start_point - end_point);

        if (d < 0.1) {
            return end_point;
        }
        else {
            return Mathf.Lerp(start_point, end_point, rate);
        }
    }

    public STATE get_state() {
        return this.my_state;
    }

    public void set_state(STATE new_state) {
        this.my_state = new_state;
    }
}
