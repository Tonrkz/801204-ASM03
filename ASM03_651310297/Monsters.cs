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

        public Byte ATK { get; set; }

        public Byte DEF { get; set; }

        public Byte AGI { get; set; }

        public int EXP { get; set; }

        public int gold { get; set; }

        public void Attack() {
            int deltaAGI = AGI - Players.Instance.AGI;
            int rng = aRandom.Next(0, 100);
            Console.WriteLine("It attacks!\n");
            rng += deltaAGI;
            if (rng < 15) {
                Console.WriteLine("Missed!");
                GameManager.Instance.PressEnterToContinue();
            }
            else {
                rng = aRandom.Next(-5, 5);
                Players.Instance.HP -= ATK + (ATK * rng / 100);
                Console.WriteLine($"It dealt {ATK + (ATK * rng / 100)} damage to you!");
                GameManager.Instance.PressEnterToContinue();
            }
        }

        public void Escape() {

        }

        public void Dead() {
            String[] words = new String[] { "You defeated the slime!", $"You gained {EXP} EXP and {gold} gold!" };
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
            if (Players.Instance.EXP >= Players.Instance.maxEXP) {
                Players.Instance.LevelUp(y);
            }
            GameManager.Instance.PressEnterToContinue();
        }
    }
}