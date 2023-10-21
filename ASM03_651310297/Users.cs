﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public abstract class Users {
        Random aRandom = new Random();
        public Swords sword { get; set; }
        public Shields shield { get; set; }
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }
        public Byte level { get; set; }
        public int EXP { get; set; }
        public int maxEXP { get; set; }
        public Byte ATK { get; set; }
        public Byte DEF { get; set; }
        public Byte AGI { get; set; }
        public int gold { get; set; }
        public Byte position { get; set; }
        public bool isEscape { get; set; }

        public void LevelUp() {
            if (EXP >= maxEXP) {
                EXP -= maxEXP;
                maxEXP += 5;
                level++;
                maxHP += 5;
                HP = maxHP;
                ATK += 5;
                DEF += 5;
                AGI += 5;
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
            }
            else {
                Console.WriteLine("Invalid input!");
                GameManager.Instance.PressEnterToContinue();
            }
        }

        public void Attack(Monsters aMonster) {
            int deltaAGI = AGI - aMonster.AGI;
            int rng = aRandom.Next(0, 100);
            rng += deltaAGI;
            if (rng < 10) {
                Console.WriteLine("You missed!");
                GameManager.Instance.PressEnterToContinue();
            }
            else if (rng > 90) {
                rng = aRandom.Next(-2 * level, 3 * level);
                Console.WriteLine("Critical hit!\n");
                aMonster.HP -= (ATK + rng) * 2;
                Console.WriteLine($"You dealt {(ATK + rng) * 2} damage to {aMonster.name}!");
                GameManager.Instance.PressEnterToContinue();
            }
            else {
                rng = aRandom.Next(-2 * level, 3 * level);
                aMonster.HP -= ATK + rng;
                Console.WriteLine($"You dealt {ATK + rng} damage to {aMonster.name}!");
                GameManager.Instance.PressEnterToContinue();
            }
        }

        public void Escape(Monsters aMonster) {
            int deltaAGI = AGI - aMonster.AGI;
            int rng = aRandom.Next(1, 101);
            rng += deltaAGI;
            if (rng < 50) {
                Console.WriteLine("You failed to escape!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = false;
            }
            else {
                Console.WriteLine("You escaped!");
                GameManager.Instance.PressEnterToContinue();
                isEscape = true;
            }
        }

        public void Status() {
            Console.WriteLine($"Name: {name}\nHP: {HP}/{maxHP}\nLevel: {level}\nEXP: {EXP}/{maxEXP}\nATK: {ATK}\nDEF: {DEF}\nAGI: {AGI}\nGold: {gold}");
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
            else if (input == "4" || input.Contains("Volcano", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 3]) {
                    position = 3;
                    return "Enter";
                }
            }
            else if (input == "5" || input.Contains("Castle", StringComparison.OrdinalIgnoreCase)) {
                if (Map.Instance.adjacencyMatrix[position, 4]) {
                    position = 4;
                    return "Enter";
                }
            }
            else if (input.Contains("Enter", StringComparison.OrdinalIgnoreCase) || input == "") {
                return "Enter";
            }
            else if (input.Contains("Status", StringComparison.OrdinalIgnoreCase)) {
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

        public Users(String a = "Player") {
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