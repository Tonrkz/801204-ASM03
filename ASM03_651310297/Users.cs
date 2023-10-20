using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Users {
        private Swords sword;
        private Shields shield;
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

        public Users(string n = "Player")
        {
            this.name = n;
            this.HP = 10;
            this.maxHP = 10;
            this.level = 1;
            this.EXP = 0;
            this.maxEXP = 5;
            this.ATK = 10;
            this.DEF = 10;
            this.AGI = 10;
            this.gold = 0;
            this.position = 0;
        }
    }
}