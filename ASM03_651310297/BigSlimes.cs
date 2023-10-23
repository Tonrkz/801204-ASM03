using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class BigSlimes : Monsters {
        public BigSlimes() {
            name = "Big Slime";
            HP = 200;
            maxHP = 200;
            ATK = 50;
            DEF = 50;
            AGI = 50;
            EXP = 250;
            gold = 250;
            isEscape = false;
        }
    }
}