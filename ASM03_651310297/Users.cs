using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Users {
        public Swords sword { get; set; }
        public Shields shield { get; set; }
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }
        public int level { get; set; }
        public int EXP { get; set; }
        public int maxEXP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int AGI { get; set; }
        public int gold { get; set; }
        public int position { get; set; }

        public void Attack() {
            throw new System.NotImplementedException();
        }

        public void Run() {
            throw new System.NotImplementedException();
        }

        public abstract String MoveEnterStatusSaveExit();

        public Users(String a = "Player")
        {
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