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

        public void PressEnterToContinue() {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
