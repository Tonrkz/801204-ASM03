using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Phoenixes : Monsters {
        public Phoenixes() {
            name = "Phoenix";
            HP = 1000;
            maxHP = 1000;
            ATK = 150;
            DEF = 150;
            AGI = 150;
            EXP = 1000;
            gold = 1000;
            isEscape = false;
        }
    }
}