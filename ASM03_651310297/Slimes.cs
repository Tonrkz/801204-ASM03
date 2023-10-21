using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Slimes : Monsters {
        public Slimes() {
            name = "Slime";
            HP = 20;
            maxHP = 20;
            ATK = 2;
            DEF = 2;
            AGI = 2;
            EXP = 5;
            gold = 10;
            isEscape = false;
        }
    }
}