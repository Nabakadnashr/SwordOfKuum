using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStamina : MonoBehaviour {

    private static float STAMINA;
    private static int MAX_STAMINA;

    private static float timer_renew;
    private float renew_delay;
    private float renew_rate;

    private StaminaPoints stamina_ui;

    void Awake() {
        MAX_STAMINA = 3;
        STAMINA = MAX_STAMINA;

        renew_delay = 1f;
        renew_rate = 2f;
    }

	// Use this for initialization
	void Start () {
        timer_renew = 0f;
        stamina_ui = Object.FindObjectOfType<StaminaPoints>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(STAMINA);

        if (Input.GetKeyDown(KeyCode.J)) {
            set_max_stamina(get_max_stamina() + 1);
        }

		if (STAMINA < MAX_STAMINA) {
            if (timer_renew < renew_delay) {
                timer_renew += Time.deltaTime;
            }
            else {
                increase_stamina(renew_rate * Time.deltaTime);
            }
        }
        else {
            timer_renew = 0f;
        }

	}

    public static void decrease_stamina(float amount) {
        STAMINA -= amount;

        if (STAMINA < 0) {
            STAMINA = 0f;
        }

        timer_renew = 0f;

        StaminaPoints points_obj = Object.FindObjectOfType<StaminaPoints>();
        points_obj.update_points();
    }

    public static void increase_stamina(float amount) {
        int st_temp = get_stamina();
        STAMINA += amount;

        if (STAMINA > MAX_STAMINA) {
            STAMINA = MAX_STAMINA;
        }
        if (st_temp < get_stamina()) {
            StaminaPoints points_obj = Object.FindObjectOfType<StaminaPoints>();
            points_obj.update_points();
        }
    }

    public static void set_max_stamina(int amount) {
        MAX_STAMINA = amount;
        STAMINA = MAX_STAMINA;
        StaminaPoints points_obj = Object.FindObjectOfType<StaminaPoints>();
        points_obj.set_points();
    }

    public static int get_stamina() {
        return (int)STAMINA;
    }

    public static int get_max_stamina() {
        return MAX_STAMINA;
    }
}
