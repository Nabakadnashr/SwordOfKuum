using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaPoint : MonoBehaviour {

    public enum POINT_STATE {
        ACTIVE,
        INACTIVE
    };

    private POINT_STATE my_state;

    private Color active_colour = new Color(1f, 1f, 1f);
    private Color inactive_color = new Color(42/255f, 28/255f, 48/255f);

    private Image my_image;

    private float fade_rate;

    private bool shaking;
    private float timer_shake;
    private float shake_time;

    private Vector2 original_pos;
    private System.Random r;

    // Use this for initialization
    void Start () {
        my_image = GetComponent<Image>();
        my_state = POINT_STATE.ACTIVE;
        fade_rate = 0.35f;

        shaking = false;
        timer_shake = 0f;
        shake_time = 0.5f;

        original_pos = this.transform.position;
        r = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
        switch (my_state) {
            case POINT_STATE.ACTIVE: {
                    if (!my_image.color.Equals(active_colour)) {
                        my_image.color = fade_to(active_colour);
                    }
                } break;
            case POINT_STATE.INACTIVE: {
                    if (!my_image.color.Equals(inactive_color)) {
                        my_image.color = fade_to(inactive_color);
                    }
                }break;
        }

        if (shaking) {
            timer_shake += Time.deltaTime;

            this.transform.position = get_shake_pos();

            if (timer_shake > shake_time) {
                timer_shake = 0f;
                shaking = false;
                this.transform.position = original_pos;
            }
        }

	}

    public void activate() {
        my_state = POINT_STATE.ACTIVE;
    }

    public void deactivate() {
        my_state = POINT_STATE.INACTIVE;
    }

    private Color fade_to(Color c) {
        return Color.Lerp(my_image.color, c, fade_rate);
    }

    public bool get_active() {
        return my_state == POINT_STATE.ACTIVE;
    }

    public void shake() {
        shaking = true;
    }

    private Vector2 get_shake_pos() {
        r = new System.Random();
        Vector2 new_pos = original_pos + new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));

        return new_pos;
    }

}
