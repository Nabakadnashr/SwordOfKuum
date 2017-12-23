using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeGeneric : MonoBehaviour {

    public enum STATE {
        ACTIVE,
        INACTIVE
    }

    private Canvas the_canvas;

    private STATE my_state;

    private TextMeshProUGUI notice_text;

    private Vector2 active_point;// = new Vector2(0f, 50f - 275f);
    private Vector2 inactive_point;// = new Vector2(0f, -50f - 275f);
    private float rate;

    private Vector2 initial_point;

    private float timer;
    private float active_time;

    void Awake() {
        the_canvas = FindObjectOfType<Canvas>();
        active_point = new Vector2(0, 50f);
        inactive_point = new Vector2(0, -50f);

        my_state = STATE.ACTIVE;
        this.transform.localPosition = inactive_point;
        notice_text = this.GetComponentInChildren<TextMeshProUGUI>();
        rate = 0.15f;

        active_time = 3f;
        timer = 0f;
    }

	void Update () {

        switch (my_state) {

            case STATE.ACTIVE: {
                    if (!transform.localPosition.Equals(active_point)) {
                        transform.localPosition = VectorLerp.vector_lerp(this.transform.localPosition, active_point, rate);
                    }

                    timer += Time.deltaTime;

                    if (timer >= active_time) {
                        timer = 0f;
                        my_state = STATE.INACTIVE;
                    }
                }
                break;
            case STATE.INACTIVE: {

                    Debug.Log(transform.localPosition + " | " + inactive_point);

                    if (!((Vector2)transform.localPosition).Equals(inactive_point)) {
                        transform.localPosition = VectorLerp.vector_lerp(this.transform.localPosition, inactive_point, rate);
                    }
                    else {
                        Destroy(this.gameObject);
                    }
                }
                break;

        }
		
	}

    public void set_text(string new_text) {
        this.notice_text.text = new_text;
    }

    IEnumerator move_notice(Vector2 end_point) {
        Debug.Log("A");
        this.transform.localPosition = VectorLerp.vector_lerp(this.transform.localPosition, end_point, this.rate);
        yield return null;
    }

}
