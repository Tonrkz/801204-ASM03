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

        Players(string a) {
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
            swordID = 0;
            shieldID = 0;
            sword = new Swords(0, "None", 0, 0, 0, 0);
            shield = new Shields(0, "None", 0, 0, 0, 0);
        }

        Random aRandom = new Random();

        public void LevelUp(int top) {
            if (EXP >= maxEXP) {
                EXP -= maxEXP;
                maxEXP += 15;
                level++;
                sword.Dequip();
                shield.Dequip();
                if (level > 255) {
                    level = 255;
                }
                maxHP += 5;
                HP = maxHP;
                ATK += 2;

                if (ATK > 255) {
                    ATK = 255;
                }
                DEF += 2;
                if (DEF > 255) {
                    DEF = 255;
                }
                AGI += 2;
                if (AGI > 255) {
                    AGI = 255;
                }
                sword.Equip();
                shield.Equip();
                Console.SetCursorPosition((Console.WindowWidth - "You leveled up!".Length) / 2, top);
                Console.WriteLine("You leveled up!");
            }
        }

        public void ChooseBattleAction(Monsters aMonster) {
            Console.Write("Input 1 to 'Attack'.\nInput 2 to 'Escape'.\nInput 3 to view your status.\nInput: ");
            string input = Console.ReadLine();
            if (input == "1" || input.Contains("Attack", StringComparison.OrdinalIgnoreCase)) {
                Attack(aMonster);
            }
            else if (input == "2" || input.Contains("Escape", StringComparison.OrdinalIgnoreCase)) {
                Escape(aMonster);
            }
            else if (input == "3" || input.Contains("Status", StringComparison.OrdinalIgnoreCase)) {
                Console.WriteLine();
                Status();
                Map.Instance.ShowBattle(aMonster.name);
                ChooseBattleAction(aMonster);
            }
            else {
                Console.WriteLine("\nInvalid input!");
                GameManager.Instance.PressEnterToContinue();
                Map.Instance.ShowBattle(aMonster.name);
                ChooseBattleAction(aMonster);
            }
        }

        public void Attack(Monsters aMonster) {
            int deltaAGI = AGI - aMonster.AGI;
            int rng = aRandom.Next(0, 100);
            int damage = 0;
            rng += deltaAGI;
            if (rng < 10) {
                Console.WriteLine("\nYou missed!");
                GameManager.Instance.PressEnterToContinue();
            }
            else if (rng > 90) {
                rng = aRandom.Next(-5, 5);
                Console.WriteLine("\nCritical hit!");
                rng = ATK * rng / 100;
                damage = (ATK + rng) * 2;
                rng = aRandom.Next(0, 11) / 100;
                damage -= aMonster.DEF * rng;
                if (damage < 0) {
                    damage = 0;
                }
                aMonster.HP -= damage;
                Console.WriteLine($"\nYou dealt {damage} damage to {aMonster.name}!");
                GameManager.Instance.PressEnterToContinue();
            }
            else {
                rng = aRandom.Next(-5, 5);
                rng = ATK * rng / 100;
                damage = ATK + rng;
                rng = aRandom.Next(25, 51) / 100;
                damage -= aMonster.DEF * rng;
                if (damage < 0) {
                    damage = 0;
                }
                aMonster.HP -= damage;
                Console.WriteLine($"\nYou dealt {damage} damage to {aMonster.name}!");
                GameManager.Instance.PressEnterToContinue();
            }
        }

        public void Escape(Monsters aMonster) {
            int deltaAGI = AGI - aMonster.AGI;
            int rng = aRandom.Next(1, 101);
            rng += deltaAGI;
            if (rng < 50) {
                Console.WriteLine("\nYou failed to escape!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = false;
            }
            else {
                Console.WriteLine("\nYou escaped!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = true;
            }
        }

        public void Dead(Monsters aMonster) {
            String[] words = new String[] { $"You were killed by the {aMonster.name}", "Dragon still remains.", "Waiting for hero to try him." };
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, 41);
            GameManager.Instance.PressEnterToContinue();
        }

        public void Status() {
            Console.WriteLine($"Name: {name}\nHP: {HP}/{maxHP}\nLevel: {level}\nEXP: {EXP}/{maxEXP}\nATK: {ATK}\nDEF: {DEF}\nAGI: {AGI}\nGold: {gold}\n\nSword: {sword.name}\nShield: {shield.name}\n");
            GameManager.Instance.PressEnterToContinue();
        }

        public String MoveEnterStatusSaveExit() {
            Console.Write("Input adjacent place number to move into and enter it.\nInput 'Enter' to enter current place.\nInput 'Status' to view status menu.\nInput 'Save' to save game.\nInput 'Exit' to quit game\nInput: ");
            string input = Console.ReadLine();
            if (input == "1" || input.Contains("Town", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 0]) {
                    position = 0;
                    return "Enter";
                }
            }
            else if (input == "2" || input.Contains("Plain", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 1]) {
                    position = 1;
                    return "Enter";
                }
            }
            else if (input == "3" || input.Contains("Forest", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 2]) {
                    position = 2;
                    return "Enter";
                }
            }
            else if (input == "4" || input.Contains("Deep", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 3]) {
                    position = 3;
                    return "Enter";
                }
            }
            else if (input == "5" || input.Contains("Volcano", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 4]) {
                    position = 4;
                    return "Enter";
                }
            }
            else if (input == "6" || input.Contains("Castle", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 5]) {
                    position = 5;
                    return "Enter";
                }
            }
            else if (input.Contains("Enter", StringComparison.OrdinalIgnoreCase) || input == "") {
                return "Enter";
            }
            else if (input.Contains("Stat", StringComparison.OrdinalIgnoreCase)) {
                return "Status";
            }
            else if (input.Contains("Save", StringComparison.OrdinalIgnoreCase)) {
                return "Save";
            }
            else if (input.Contains("Exit", StringComparison.OrdinalIgnoreCase)) {
                return "Exit";
            }
            else {
                return "Invalid";
            }
            return "Invalid";
        }

        public void SetAllDefault() {
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
            swordID = 0;
            shieldID = 0;
            sword = new Swords(0, "None", 0, 0, 0, 0);
            shield = new Shields(0, "None", 0, 0, 0, 0);
        }
    }
}