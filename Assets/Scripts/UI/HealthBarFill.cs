using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFill : MonoBehaviour {

    [SerializeField]
    private Image HP_bar_fill;
    [SerializeField]
    private Image HP_bar_under;

    private float timer_bar_under_delay;
    private float bar_under_delay_amount;

	// Use this for initialization
	void Start () {
        timer_bar_under_delay = 0f;
        bar_under_delay_amount = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

        if (get_hp_ratio() != HP_bar_fill.fillAmount) {
            HP_bar_fill.fillAmount = get_hp_ratio();
        }

        if (HP_bar_fill.fillAmount < HP_bar_under.fillAmount) {
            if (timer_bar_under_delay < bar_under_delay_amount) {
                timer_bar_under_delay += Time.deltaTime;
            }
            else {
                float fill_diff = Mathf.Abs(HP_bar_fill.fillAmount - HP_bar_under.fillAmount);
                HP_bar_under.fillAmount -= 0.05f * (fill_diff);

                if (fill_diff < 0.01f) {
                    bar_under_reset();
                }
            }
        }
        if (HP_bar_fill.fillAmount > HP_bar_under.fillAmount) {
            bar_under_reset();
        }
		
	}

    private float get_hp_ratio() {
        return HeroHealth.get_health() / HeroHealth.get_max_health();
    }

    private void bar_under_reset() {
        HP_bar_under.fillAmount = HP_bar_fill.fillAmount;
        timer_bar_under_delay = 0f;
    }
}
