using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Monsters {
        public delegate void MonsterAction();
        public MonsterAction monsterAction;
        public Monsters() {
            monsterAction = Attack;
        }
        Random aRandom = new Random();
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }

        public Byte ATK { get; set; }

        public Byte DEF { get; set; }

        public Byte AGI { get; set; }

        public int EXP { get; set; }

        public int gold { get; set; }
        public bool isEscape { get; set; }

        public void ChooseBattleAction() {
            if (HP <= 0) {
                monsterAction = Dead;
                return;
            }
            int rng = aRandom.Next(1, 101);
            if (rng < 26 && HP <= maxHP / 2) {
                monsterAction = Escape;
            }
            else {
                monsterAction = Attack;
            }
        }

        public void Attack() {
            int deltaAGI = AGI - Players.Instance.AGI;
            int rng = aRandom.Next(0, 100);
            int damage = 0;
            Console.WriteLine("It attacks!\n");
            rng += deltaAGI;
            if (rng < 15) {
                Console.WriteLine("Missed!");
                GameManager.Instance.PressEnterToContinue();
            }
            else if (rng > 89) {
                rng = aRandom.Next(-5, 5);
                Console.WriteLine("Critical hit!\n");
                damage = (ATK + (ATK * rng / 100)) * 2;
                rng = aRandom.Next(0, 11) / 100;
                damage -= Players.Instance.DEF * rng;
                if (damage < 0) {
                    damage = 0;
                }
                Players.Instance.HP -= damage;
                Console.WriteLine($"It dealt {damage} damage to you!");
                GameManager.Instance.PressEnterToContinue();
            }
            else {
                rng = aRandom.Next(-5, 5);
                damage = ATK + (ATK * rng / 100);
                rng = aRandom.Next(25, 51) / 100;
                damage -= Players.Instance.DEF * rng;
                if (damage < 0) {
                    damage = 0;
                }
                Players.Instance.HP -= damage;
                Console.WriteLine($"It dealt {damage} damage to you!");
                GameManager.Instance.PressEnterToContinue();
            }
        }

        public virtual void Escape() {
            int deltaAGI = AGI - Players.Instance.AGI;
            int rng = aRandom.Next(1, 101);
            rng += deltaAGI;
            if (rng < 75) {
                Console.WriteLine("It failed to escape!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = false;
            }
            else {
                Console.WriteLine("It escaped!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = true;
            }
        }

        public void Dead() {
            String[] words = new String[] { $"You defeated the {name}!", $"You gained {EXP} EXP and {gold} gold!" };
            Players.Instance.EXP += EXP;
            Players.Instance.gold += gold;
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            while (Players.Instance.EXP >= Players.Instance.maxEXP) {
                Players.Instance.LevelUp(y);
            }
            Console.SetCursorPosition(0, 41);
            GameManager.Instance.PressEnterToContinue();
        }
    }
}