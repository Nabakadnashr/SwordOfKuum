using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingObject : ObjectGeneric {

    protected string contents;

    public string get_contents() {
        return contents;
    }

    override
    public void add_to_inventory() {
        MonoBehaviour.FindObjectOfType<Inventory>().add_writing(this);
    }

    override
    public void object_execute() {

    }

    override
    public string get_description() {
        return this.o_desription + "\n\n" + this.contents;
    }

}
