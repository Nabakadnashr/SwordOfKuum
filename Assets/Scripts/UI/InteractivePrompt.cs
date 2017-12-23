using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePrompt : MonoBehaviour {

    public enum STATE {
        ACTIVE,
        INACTIVE
    }

    private STATE my_state;

    [SerializeField]
    private Vector2 low_point = new Vector2(0f, 0f);
    [SerializeField]
    private Vector2 high_point = new Vector2(0f, 1f);

    private Color low_color = new Color(1f, 1f, 1f, 0f);
    private Color high_color = new Color(1f, 1f, 1f, 1f);

    private float lerp_rate;

    [SerializeField]
    private GameObject interact_prompt;
    private GameObject prompt_object;
    private SpriteRenderer prompt_renderer;

    // Use this for initialization
    void Start () {
        my_state = STATE.INACTIVE;
        lerp_rate = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {

        switch (my_state) {
            case STATE.ACTIVE: {

                    if (prompt_object == null) {
                        prompt_object = Instantiate(interact_prompt, this.transform);
                        prompt_renderer = prompt_object.GetComponent<SpriteRenderer>();
                    }

                    if (!prompt_object.transform.localPosition.Equals(high_point)) {
                        prompt_object.transform.localPosition = vector_lerp(prompt_object.transform.localPosition, high_point, lerp_rate);
                    }

                    if (!prompt_renderer.color.Equals(high_color)) {
                        prompt_renderer.color = color_lerp(prompt_renderer.color, high_color, lerp_rate);
                    }

                }
                break;
            case STATE.INACTIVE: {

                    if (prompt_object) {
                        if (!prompt_object.transform.localPosition.Equals(low_point)) {
                            prompt_object.transform.localPosition = vector_lerp(prompt_object.transform.localPosition, low_point, lerp_rate);
                        }

                        if (!prompt_renderer.color.Equals(low_color)) {
                            prompt_renderer.color = color_lerp(prompt_renderer.color, low_color, lerp_rate);
                        }
                        else {
                            Destroy(prompt_object);
                        }
                    }

                }
                break;
        }
		
	}

    private Vector2 vector_lerp(Vector2 start_point, Vector2 end_point, float rate) {
        float d = Mathf.Abs(start_point.magnitude - end_point.magnitude);

        if (d < 0.0001) {
            return end_point;
        }
        else {
            return Vector2.Lerp(start_point, end_point, this.lerp_rate);
        }
    }

    private Color color_lerp(Color start_color, Color end_color, float rate) {
        float d = Mathf.Abs(start_color.a - end_color.a);

        if (d < 0.01) {
            return end_color;
        }
        else {
            return Color.Lerp(start_color, end_color, lerp_rate);
        }
    }

    public bool is_active() {
        return this.my_state == STATE.ACTIVE;
    }

    public void set_active(STATE new_state) {
        this.my_state = new_state;
    }

    /*public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            this.my_state = STATE.ACTIVE;
        }
    }

    public void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            this.my_state = STATE.INACTIVE;
        }
    }*/

}
