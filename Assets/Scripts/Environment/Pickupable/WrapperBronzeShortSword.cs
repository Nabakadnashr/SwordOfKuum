using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperBronzeShortSword : WrapperPickupable {

    void Awake() {
        this.GetComponent<Pickup>().set_pickup_object(new BronzeShortSword());
    }

    /*override
    public void pickup_action() {
        //this.add_me_to_inventory();
        //Destroy(this.gameObject);
    }*/
    /*
    private void add_me_to_inventory() {
        Inventory inv = MonoBehaviour.FindObjectOfType<Inventory>();
        inv.add_weapon(new BronzeShortSword());
    }*/
}
