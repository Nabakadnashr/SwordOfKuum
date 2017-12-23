using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private ObjectGeneric my_pickup_object;

    [SerializeField]
    private GameObject notice_holder;

    [SerializeField]
    private GameObject my_glow;

    [SerializeField]
    private GameObject my_notice;

    private InteractivePrompt my_prompt;

    private float timer;
    private float time;

    private const string default_text = "You've found :\n\n";

	// Use this for initialization
	void Start () {
        my_prompt = GetComponent<InteractivePrompt>();
        timer = 0f;
        time = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > time) {
            Instantiate(my_glow, this.transform);
            timer = 0f;
        }

        if (my_prompt.is_active()) {
            if (Input.GetKeyDown(HeroKeys.INTERACT_KEY)) {
                HeroController hc = FindObjectOfType<HeroController>();
                if (hc.get_state() == HeroController.STATE.ACTIVE) {
                    this.my_pickup_object.add_to_inventory();
                    //GameObject n_o = Instantiate(my_notice, FindObjectOfType<Canvas>().transform);
                    Instantiate(my_notice, notice_holder.transform).GetComponent<NoticeGeneric>().set_text(default_text + this.my_pickup_object.get_name());
                    Destroy(this.gameObject);
                }
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            this.my_prompt.set_active(InteractivePrompt.STATE.ACTIVE);
        }
    }

    public void OnTriggerExit2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            this.my_prompt.set_active(InteractivePrompt.STATE.INACTIVE);
        }
    }

    public void set_pickup_object(ObjectGeneric obj) {
        this.my_pickup_object = obj;
    }
}
