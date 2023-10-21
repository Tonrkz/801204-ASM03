using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Equipments {
        Byte ID { get; set; }
        public String name { get; set; }
        public int ATK { get; }
        public int DEF { get; }
        public int price { get; }
        public int AGI { get; }

        public Equipments(Byte ID = 0, String name = "None", int ATK = 0, int DEF = 0, int AGI = 0, int price = 0) {
            this.ID = ID;
            this.name = name;
            this.ATK = ATK;
            this.DEF = DEF;
            this.AGI = AGI;
            this.price = price;
        }

        public void Equip() {
            Players.Instance.ATK += ATK;
            Players.Instance.DEF += DEF;
            Players.Instance.AGI += AGI;
            if (this is Swords) {
                Players.Instance.swordID = ID;
            }
            else if (this is Shields) {
                Players.Instance.shieldID = ID;
            }
        }

        public void Dequip() {
            Players.Instance.ATK -= ATK;
            Players.Instance.DEF -= DEF;
            Players.Instance.AGI -= AGI;
            if (this is Swords) {
                Players.Instance.swordID = 0;
            }
            else if (this is Shields) {
                Players.Instance.shieldID = 0;
            }
        }
    }
}