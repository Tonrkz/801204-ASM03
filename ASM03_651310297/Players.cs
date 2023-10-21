using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Players : Users {
        private static Players instance;
        public static Players Instance {
            get {
                if (instance == null) {
                    instance = new Players("Player");
                }
                return instance;
            }
        }

        Players(string a) : base(a) {
            name = a;
            HP = 10;
            maxHP = 10;
            level = 1;
            EXP = 0;
            maxEXP = 5;
            ATK = 10;
            DEF = 10;
            AGI = 10;
            gold = 0;
            position = 0;
        }
    }
}