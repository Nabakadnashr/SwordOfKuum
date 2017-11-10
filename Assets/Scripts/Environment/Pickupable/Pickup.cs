using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField]
    private GameObject my_glow;

    private InteractivePrompt my_prompt;

    private float timer;
    private float time;

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
                    gameObject.SendMessage("pickup_action");
                }
            }
        }
        
    }
}
