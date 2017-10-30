using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BronzeShortSword: WeaponObject {

    public BronzeShortSword() {
        this.o_name = "Bronze Short Sword";
        this.o_desription = "Many a seasoned warrior first tasted the fires of battle with blades like this.";

        this.damage = 5;
        this.stamina_cost = 1;
    }

}
