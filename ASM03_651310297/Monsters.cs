using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Monsters {
        public int HP { get; set; }

        public int ATK { get; }

        public int DEF { get; }

        public int AGI { get; }

        public int EXP { get; }

        public int gold { get; }

        public void Attack() {
            throw new System.NotImplementedException();
        }
    }
}