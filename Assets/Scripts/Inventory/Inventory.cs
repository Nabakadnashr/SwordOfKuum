using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public enum OBJECT_TYPE {
        WEAPON = 0,
        CLOTHING = 1,
        WRITING = 2,
        HEALTH = 3
    }

    private List<GameObject> my_listeners = new List<GameObject>();

    private OBJECT_TYPE cursor_type;

    private Dictionary<OBJECT_TYPE, int> inventory_cursors = new Dictionary<OBJECT_TYPE, int>() {
        { OBJECT_TYPE.WEAPON, 0 },
        { OBJECT_TYPE.CLOTHING, 0 },
        { OBJECT_TYPE.WRITING, 0 }
    };

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
    };

    void Awake() {

        cursor_type = OBJECT_TYPE.WEAPON;

        add_weapon(new BronzeLongknife());
        //add_weapon(new BronzeShortSword());

        add_clothing(new BoorLeatherRags());

        add_writing(new FriendsLetter());

        make_active_item(inventory_cursors[OBJECT_TYPE.WEAPON], OBJECT_TYPE.WEAPON);
        make_active_item(inventory_cursors[OBJECT_TYPE.CLOTHING], OBJECT_TYPE.CLOTHING);
        make_active_item(inventory_cursors[OBJECT_TYPE.WRITING], OBJECT_TYPE.WRITING);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(HeroKeys.INVENTORY_KEY)) {
            InventoryUI io = FindObjectOfType<InventoryUI>();
            if (io) {
                io.set_state(io.get_state() == InventoryUI.STATE.INACTIVE ? InventoryUI.STATE.ACTIVE : InventoryUI.STATE.INACTIVE);
            }
        }
		
	}

    public OBJECT_TYPE get_category_index_raw() {
        return this.cursor_type;
    }

    public int get_category_index() {
        return (int)this.cursor_type;
    }

    public int increase_category_index() {
        this.cursor_type = (OBJECT_TYPE)(((int)this.cursor_type + 1) % 3);
        return (int)this.cursor_type;
    }

    public int decrease_category_index() {
        int x = (int)this.cursor_type;
        x--;
        if (x < 0) x += 3;
        this.cursor_type = (OBJECT_TYPE)x;
        return (int)this.cursor_type;
    }

    public int get_item_index(OBJECT_TYPE type) {
        return this.inventory_cursors[type];
    }

    public void increase_item_index(OBJECT_TYPE type) {
        int x = this.inventory_cursors[type];
        x = ++x % this.my_inventory[type].Count;
        this.inventory_cursors[type] = x;
    }

    public void decrease_item_index(OBJECT_TYPE type) {
        int x = this.inventory_cursors[type];
        x--;
        if (x < 0) x += this.my_inventory[type].Count;
        this.inventory_cursors[type] = x;
    }

    public void make_active_item(int ind, OBJECT_TYPE type) {
        inventory_active_items[type] = my_inventory[type][ind];
        this.update_listeners();
    }

    public void add_weapon(WeaponObject wobj) {
        my_inventory[OBJECT_TYPE.WEAPON].Add(wobj);
    }

    public WeaponObject get_active_weapon() {
        return (WeaponObject)inventory_active_items[OBJECT_TYPE.WEAPON];
    }

    public void add_clothing(ClothingObject cobj) {
        my_inventory[OBJECT_TYPE.CLOTHING].Add(cobj);
    }

    public ClothingObject get_active_clothing() {
        return (ClothingObject)inventory_active_items[OBJECT_TYPE.CLOTHING];
    }

    public void add_writing(WritingObject wobj) {
        my_inventory[OBJECT_TYPE.WRITING].Add(wobj);
    } 

    public WritingObject get_active_writing() {
        return (WritingObject)inventory_active_items[OBJECT_TYPE.WRITING];
    }

    public ObjectGeneric get_active_generic(OBJECT_TYPE type) {
        return this.inventory_active_items[type];
    }

    public List<ObjectGeneric> get_items(OBJECT_TYPE type) {
        return this.my_inventory[type];
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

}
