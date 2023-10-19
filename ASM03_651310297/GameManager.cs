using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM03_651310297 {
    internal class GameManager {
        static readonly GameManager instance = new GameManager();
        public static GameManager Instance {
            get { return instance; }
        }

        public void MaximizePleaseScreen() {
            Console.WindowWidth = 200;
            Console.WindowHeight = 70;
            Console.BufferWidth = 250;
            Console.BufferHeight = 100;
            Console.Clear();
            Console.WriteLine("For a proper game displaying.\nThis game has to play with maximized or fullscreen window only.\nPlease enter fullscreen or maximized mode in your console app.\nThank you.");
        }

        public void PressEnterToContinue() {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
