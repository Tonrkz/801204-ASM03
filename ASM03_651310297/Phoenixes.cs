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
            if (bool.Parse(XMLOperator.Instance.LoadPlayer().Elements("player").Where(x => x.Attribute("name").Value == Players.Instance.name).First().Element("dragonDefeat").Value)) {
                name = "Phoenix";
                HP = 2500;
                maxHP = 2500;
                ATK = 255;
                DEF = 255;
                AGI = 255;
                EXP = 5000;
                gold =5000;
                isEscape = false;
            }
        }
    }
}