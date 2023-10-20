using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Players : Users {
        public Players(string n = "Player") {
            this.name = n;
            this.HP = 100;
            this.maxHP = 100;
            this.level = 1;
            this.EXP = 0;
            this.maxEXP = 100;
            this.ATK = 10;
            this.DEF = 10;
            this.AGI = 10;
            this.gold = 0;
            this._position = 0;
        }
    }
}