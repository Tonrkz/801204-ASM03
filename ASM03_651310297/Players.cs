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

        public override String MoveEnterStatusSaveExit() {
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
            else if (input.Contains("Enter", StringComparison.OrdinalIgnoreCase)) {
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