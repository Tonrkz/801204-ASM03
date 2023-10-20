using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Monsters {
        public int HP { get; set; }

        public int ATK { get; set; }

        public int DEF { get; set; }

        public int AGI { get; set; }

        public int EXP { get; set; }

        public int gold { get; set; }

        public void Attack() {
            throw new System.NotImplementedException();
        }
    }
}