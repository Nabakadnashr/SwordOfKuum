using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingObject : ObjectGeneric {

    protected float resistance;

    public float get_resistance() {
        return resistance;
    }

    override
    public void add_to_inventory() {
        MonoBehaviour.FindObjectOfType<Inventory>().add_clothing(this);
    }

    override
    public void object_execute() {

    }

}
