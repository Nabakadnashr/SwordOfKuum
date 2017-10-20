using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public enum OBJECT_TYPE {
        WEAPON,
        CLOTHING,
        WRITING,
        HEALTH
    }

    private List<GameObject> my_listeners = new List<GameObject>();

    private int cursor_index_weapon;
    private int cursor_index_clothing;
    private int cursor_index_writing;
    private int cursor_index_health;

    private OBJECT_TYPE cursor_type;

    private Dictionary<OBJECT_TYPE, List<ObjectGeneric>> my_inventory = new Dictionary<OBJECT_TYPE, List<ObjectGeneric>>() {
        { OBJECT_TYPE.WEAPON, new List<ObjectGeneric>() },
        { OBJECT_TYPE.CLOTHING, new List<ObjectGeneric>() },
        { OBJECT_TYPE.WRITING, new List<ObjectGeneric>() },
        { OBJECT_TYPE.HEALTH, new List<ObjectGeneric>() }
    };

    private Dictionary<OBJECT_TYPE, ObjectGeneric> inventory_active_items = new Dictionary<OBJECT_TYPE, ObjectGeneric>() {
        { OBJECT_TYPE.WEAPON, null },
        { OBJECT_TYPE.CLOTHING, null },
        { OBJECT_TYPE.WRITING, null },
        { OBJECT_TYPE.HEALTH, null }
    };

    void Awake() {

        cursor_index_weapon = 0;
        cursor_index_clothing = 0;
        cursor_index_writing = 0;
        cursor_index_health = 0;

        cursor_type = OBJECT_TYPE.WEAPON;

        add_weapon(new BronzeLongknife());

        make_active_weapon(cursor_index_weapon);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public WeaponObject get_active_weapon() {
        return (WeaponObject)my_inventory[OBJECT_TYPE.WEAPON][0];
    }

    public void add_listener(GameObject obj) {
        if (my_listeners.Contains(obj)) {
            return;
        }
        my_listeners.Add(obj);
    }
    public void update_listeners() {
        foreach (GameObject obj in my_listeners) {
            obj.SendMessage("listener_update");
        }
    }

    public void add_weapon(WeaponObject wobj) {
        my_inventory[OBJECT_TYPE.WEAPON].Add(wobj);
    }

    public void make_active_weapon(int ind) {
        inventory_active_items[OBJECT_TYPE.WEAPON] = my_inventory[OBJECT_TYPE.WEAPON][ind];
    }
}
