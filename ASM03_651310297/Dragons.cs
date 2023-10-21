using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Dragons : Monsters {
        Random aRandom = new Random();
        public Dragons() {
            name = "Dragon";
            HP = 1000;
            ATK = 100;
            DEF = 100;
            AGI = 100;
            EXP = 500;
            gold = 500;
            isEscape = false;
        }

        public void Escape() {
            int deltaAGI = AGI - Players.Instance.AGI;
            int rng = aRandom.Next(0, 50);
            int tempATK = ATK * (ATK + (rng / 100));
            Console.WriteLine("It burns you with its flame!\n");
            rng = aRandom.Next(1, 101);
            rng += deltaAGI;
            if (rng < 15) {
                Console.WriteLine("Missed!");
                GameManager.Instance.PressEnterToContinue();
            }
            else if (rng > 89) {
                rng = aRandom.Next(-5, 5);
                Console.WriteLine("Critical hit!\n");
                Players.Instance.HP -= (tempATK + (tempATK * rng / 100)) * 2;
                Console.WriteLine($"It dealt {(tempATK + (tempATK * rng / 100)) * 2} damage to you!");
                GameManager.Instance.PressEnterToContinue();
            }
            else {
                rng = aRandom.Next(-5, 5);
                Players.Instance.HP -= tempATK + (tempATK * rng / 100);
                Console.WriteLine($"It dealt {tempATK + (tempATK * rng / 100)} damage to you!");
                GameManager.Instance.PressEnterToContinue();
            }
        }
    }
}