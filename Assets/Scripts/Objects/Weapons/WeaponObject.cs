using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : ObjectGeneric {

    protected int damage;
    protected int stamina_cost;

    public int get_damage() {
        return damage;
    }
    public int get_stamina_cost() {
        return stamina_cost;
    }

    override
    public void add_to_inventory() {
        
    }

    override
    public void object_execute() {

    }

}
