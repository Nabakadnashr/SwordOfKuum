using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperBronzeShortSword : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickup_action() {
        this.add_me_to_inventory();
        Destroy(this.gameObject);
    }

    private void add_me_to_inventory() {
        Inventory inv = FindObjectOfType<Inventory>();
        inv.add_weapon(new BronzeShortSword());
    }
}
