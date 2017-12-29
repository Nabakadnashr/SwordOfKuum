using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class WrapperBookTwelve: WrapperPickupable {

    void Awake() {
        this.GetComponent<Pickup>().set_pickup_object(new BookOfTwelve());
    }

}
