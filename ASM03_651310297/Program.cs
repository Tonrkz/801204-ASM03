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

            switch (GameManager.Instance.StartScreen()) {
                case "1":
                    String name = GameManager.Instance.CreatePlayerScreen();
                    Console.WriteLine("Are you sure with that name? (Y/n): ");
                    if (Console.ReadLine().Contains("y", StringComparison.OrdinalIgnoreCase) || Console.ReadLine() == null) {
                        Players aPlayer = new Players(name);
                    }
                    break;
                case "2":
                    break;
                case "3":
                    gameRunning = false;
                    Console.Clear();
                    break;
                default:
                    goto case "1";
            }

            while (gameRunning) {
                Map.Instance.UpdateMap();
                Map.Instance.ShowMap();
                GameManager.Instance.PressEnterToContinue();
            }
        }
    }
}