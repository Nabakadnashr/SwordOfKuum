using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public enum STATE {
        ACTIVE,
        INACTIVE
    };

    private KeyCode category_inc = KeyCode.RightArrow;
    private KeyCode category_dec = KeyCode.LeftArrow;

    private KeyCode item_inc = KeyCode.DownArrow;
    private KeyCode item_dec = KeyCode.UpArrow;

    private CanvasGroup inventory_group;

    private STATE my_state;

    private float fade_rate;

    [SerializeField]
    private RectTransform item_list;

    [SerializeField]
    private Text item_description;

    [SerializeField]
    private GameObject category_pointer;

    [SerializeField]
    private GameObject item_pointer;

    [SerializeField]
    private GameObject active_pointer;

    [SerializeField]
    private GameObject item_entry;

    private Dictionary<Inventory.OBJECT_TYPE, Vector2> category_points = new Dictionary<Inventory.OBJECT_TYPE, Vector2>() {
        { Inventory.OBJECT_TYPE.WEAPON, new Vector2(-42f, 160f) },
        { Inventory.OBJECT_TYPE.CLOTHING, new Vector2(0.5f, 160f) },
        { Inventory.OBJECT_TYPE.WRITING, new Vector2(35f, 160f) }
    };

    private Vector2 item_start_point = new Vector2(90, 40);
    private float item_buffer = 15f;

    private Vector2 item_pointer_start_point = new Vector2(-59.5f, 40f);
    private Vector2 active_pointer_start_point = new Vector2(-51.5f, 40f);

    private Dictionary<int, GameObject> my_item_list = new Dictionary<int, GameObject>();
    private Inventory inv;

    private Vector2 category_pointer_position;
    private Vector2 item_pointer_position;
    private Vector2 active_pointer_position;

    private float pointer_move_rate;

	// Use this for initialization
	void Start () {
        inventory_group = GetComponent<CanvasGroup>();
        my_state = STATE.INACTIVE;
        fade_rate = 0.2f;

        inventory_group.alpha = 0f;

        inv = FindObjectOfType<Inventory>();

        category_pointer_position = category_points[Inventory.OBJECT_TYPE.WEAPON];
        item_pointer_position = item_pointer_start_point;
        active_pointer_position = active_pointer_start_point;
        pointer_move_rate = 0.2f;

        build_list();
	}
	
	// Update is called once per frame
	void Update () {

        if (!category_pointer.transform.localPosition.Equals(category_points[inv.get_category_index_raw()])) {
            category_pointer.transform.localPosition = vector_lerp(category_pointer.transform.localPosition, category_pointer_position, pointer_move_rate);
        }
        if (!item_pointer.transform.localPosition.Equals(item_pointer_position)) {
            item_pointer.transform.localPosition = vector_lerp(item_pointer.transform.localPosition, item_pointer_position, pointer_move_rate);
        }
        if (!active_pointer.transform.localPosition.Equals(active_pointer_position)) {
            active_pointer.transform.localPosition = vector_lerp(active_pointer.transform.localPosition, active_pointer_position, pointer_move_rate);
        }

        switch (my_state) {
            case STATE.ACTIVE: {
                    if (inventory_group.alpha != 1f) {
                        inventory_group.alpha = fade(inventory_group.alpha, 1f, fade_rate);
                    }

                    if (Input.GetKeyDown(category_inc)) {
                        inv.increase_category_index();
                        category_pointer_position = category_points[inv.get_category_index_raw()];
                        build_list();
                        update_pointers();
                    }

                    if (Input.GetKeyDown(category_dec)) {
                        inv.decrease_category_index();
                        category_pointer_position = category_points[inv.get_category_index_raw()];
                        build_list();
                        update_pointers();
                    }

                    if (Input.GetKeyDown(item_inc)) {
                        inv.increase_item_index(inv.get_category_index_raw());
                        item_pointer_position = item_pointer_start_point + new Vector2(0f, inv.get_item_index(inv.get_category_index_raw()) * item_buffer * -1);
                        item_description.text = get_curr_description();
                    }

                    if (Input.GetKeyDown(item_dec)) {
                        inv.decrease_item_index(inv.get_category_index_raw());
                        item_pointer_position = item_pointer_start_point + new Vector2(0f, inv.get_item_index(inv.get_category_index_raw()) * item_buffer * -1);
                        item_description.text = get_curr_description();
                    }

                    if (Input.GetKeyDown(HeroKeys.INTERACT_KEY)) {
                        inv.make_active_item(inv.get_item_index(inv.get_category_index_raw()), inv.get_category_index_raw());
                        active_pointer_position = active_pointer_start_point + new Vector2(0f, inv.get_item_index(inv.get_category_index_raw()) * item_buffer * -1);
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

    private Vector2 vector_lerp(Vector2 start_point, Vector2 end_point, float rate) {
        float d = Mathf.Abs( start_point.magnitude - end_point.magnitude );

        if (d < 0.0001) {
            return end_point;
        }
        else {
            return Vector2.Lerp(start_point, end_point, this.pointer_move_rate);
        }
    }

    private string get_curr_description() {
        return inv.get_items(inv.get_category_index_raw())[inv.get_item_index(inv.get_category_index_raw())].get_description();
    }

    private void update_pointers() {
        item_pointer_position = item_pointer_start_point + new Vector2(0f, inv.get_item_index(inv.get_category_index_raw()) * item_buffer * -1);

        List<ObjectGeneric> l = inv.get_items(inv.get_category_index_raw());
        ObjectGeneric ao = inv.get_active_generic(inv.get_category_index_raw());

        for (int i = 0; i < l.Count; i++) {
            if (l[i] == ao) {
                active_pointer_position = active_pointer_start_point + new Vector2(0f, i * item_buffer * -1);
                break;
            }
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
        HeroController hc = FindObjectOfType<HeroController>();
        if (new_state == STATE.ACTIVE) {
            HeroAnimation ha = FindObjectOfType<HeroAnimation>();
            hc.set_state(HeroController.STATE.LOCKED);
            ha.set_anim("Idle");

            if (this.my_item_list.Count != inv.get_items(inv.get_category_index_raw()).Count) {
                this.build_list();
            }
        }
        else {
            hc.set_state(HeroController.STATE.ACTIVE);
        }
    }

    private void clear_item_list() {
        foreach (KeyValuePair<int, GameObject> o in this.my_item_list) {
            Destroy(o.Value);
        }

        this.my_item_list.Clear();
    }

    private void build_list() {
        if (this.my_item_list.Count > 0) {
            this.clear_item_list();
        }

        Inventory inv = FindObjectOfType<Inventory>();

        List<ObjectGeneric> list = inv.get_items(inv.get_category_index_raw());

        for (int i = 0; i < list.Count; i++) {
            Vector2 item_pos = this.item_start_point + new Vector2(0f, this.item_buffer * i * -1);
            GameObject o = Instantiate(item_entry, item_list);
            o.transform.localPosition = item_pos;
            Text item_name = o.GetComponent<Text>();
            item_name.text = list[i].get_name();

            this.my_item_list.Add(i, o);
        }

        this.item_description.text = list[inv.get_item_index(inv.get_category_index_raw())].get_description();
        
    }
}
