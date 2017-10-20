using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour {

    private static float HEALTH;
    private static float MAX_HEALTH;

    void Awake() {
        MAX_HEALTH = 100f;
        HEALTH = MAX_HEALTH;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R)) {
            decrease_health(10);
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            increase_health(10);
        }
		
	}

    public static void decrease_health(float amount) {
        HEALTH -= amount;
        if (HEALTH < 0f) {
            HEALTH = 0f;
        }
    }

    public static void increase_health(float amount) {
        HEALTH += amount;
        if (HEALTH > MAX_HEALTH) {
            HEALTH = MAX_HEALTH;
        }
    }

    public static void set_max_health(float amount) {
        MAX_HEALTH = amount;
        HEALTH = MAX_HEALTH;
    }

    public static float get_health() {
        return HEALTH;
    }

    public static float get_max_health() {
        return MAX_HEALTH;
    }
}
