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
                    instance = new Players();
                }
                return instance;
            }
        }

        /*public override String MoveEnterStatusSaveExit() {
            Console.Write("Input adjacent place number to move into\nInput 'Enter' to visit place\nInput 'Status' to view status menu.\nInput 'Save' to save game.\nInput 'Exit' to quit game\nInput: ");
            string input = Console.ReadLine();
            if (input == "1" || input.Contains("Town", StringComparison.OrdinalIgnoreCase)) {
                if (Convert.ToInt32(input) - 1 ==  position) {
                    return "Enter";
                }
                else if (Map.Instance.adjacencyMatrix[position,0]) {

                }
            }
        }*/

        public Players(string n = "Player") {
            this.name = n;
            this.HP = 100;
            this.maxHP = 100;
            this.level = 1;
            this.EXP = 0;
            this.maxEXP = 100;
            this.ATK = 10;
            this.DEF = 10;
            this.AGI = 10;
            this.gold = 0;
            this.position = 0;
        }
    }
}