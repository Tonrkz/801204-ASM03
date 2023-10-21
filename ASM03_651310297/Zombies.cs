using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Zombies : Monsters {
        public Zombies() {
            name = "Zombie";
            HP = 80;
            maxHP = 80;
            ATK = 15;
            DEF = 15;
            AGI = 15;
            EXP = 50;
            gold = 50;
            isEscape = false;
        }
    }
}