using System.Runtime.InteropServices;

namespace ASM03_651310297 {
    internal class Program {
        static bool gameRunning = true;

        static void Main(string[] args) {
            GameManager.Instance.MaximizePleaseScreen();
            GameManager.Instance.PressEnterToContinue();

            GameManager.Instance.StoryScreen();
            GameManager.Instance.PressEnterToContinue();

            Thread.Sleep(500);

            while (gameRunning) {
                switch (GameManager.Instance.StartScreen()) {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        gameRunning = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}