using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public interface Users {
        public Swords sword { get; set; }
        public int swordID { get; set; }
        public Shields shield { get; set; }
        public int shieldID { get; set; }
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }
        public Byte level { get; set; }
        public int EXP { get; set; }
        public int maxEXP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int AGI { get; set; }
        public int gold { get; set; }
        public Byte position { get; set; }
        public bool isEscape { get; set; }

        public void LevelUp(int top);
        public void ChooseBattleAction(Monsters aMonster);
        public void Attack(Monsters aMonster);
        public void Escape(Monsters aMonster);
        public void Dead(Monsters aMonster, bool dragonDead);
        public void Status();
        public String MoveEnterStatusSaveExit();
        public void SetAllDefault();
    }
}