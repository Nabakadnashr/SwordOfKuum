using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectGeneric {

    protected string o_name;
    protected string o_desription;


    public string get_name() {
        return o_name;
    }
    public string get_description() {
        return o_desription;
    }

    public abstract void add_to_inventory();
    public abstract void object_execute();

}
