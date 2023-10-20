using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Equipments {
        public int ATK { get; }

        public int DEF { get; }

        public int price { get; }

        public bool buyAble { get; set; }
    }
}