using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPoints : MonoBehaviour {

    [SerializeField]
    private GameObject stamina_point;

    [SerializeField]
    private AudioClip my_clip;
    private AudioSource my_source;

    private Vector2 starting_point = new Vector2(13f, -22f);
    private float point_length = 10f;
    private float buffer_length = 1.5f;

    private Dictionary<int, GameObject> my_points = new Dictionary<int, GameObject>();

	// Use this for initialization
	void Start () {
        set_points();
        my_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void set_points() {
        int num_of_points = HeroStamina.get_max_stamina();

        if (my_points.Count != 0) {
            foreach (KeyValuePair<int, GameObject> d in my_points) {
                Destroy(d.Value);
            }

            my_points.Clear();
        }

        for (int i = 0; i < num_of_points; i++) {
            GameObject point = Object.Instantiate(stamina_point, this.gameObject.transform);
            my_points.Add(i, point);
            point.transform.localPosition = starting_point + new Vector2((point_length + buffer_length) * i, 0f);
        }
    }

    public void update_points() {
        int stamina = HeroStamina.get_stamina();
        int max_stamina = HeroStamina.get_max_stamina();

        for (int i = 0; i < max_stamina; i++) {
            StaminaPoint s = my_points[i].GetComponent<StaminaPoint>();
            if (i < stamina) {
                if (!s.get_active()) {
                    s.activate();
                }
            }
            else {
                if (s.get_active()) {
                    s.deactivate();
                }
            }
        }
    }

    public void shake_points() {
        my_source.PlayOneShot(my_clip);
        foreach (KeyValuePair<int, GameObject> d in my_points) {
            StaminaPoint sp = d.Value.GetComponent<StaminaPoint>();
            sp.shake();
        }
    }

}
