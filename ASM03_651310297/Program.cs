using System.Runtime.InteropServices;

namespace ASM03_651310297 {
    internal class Program {
        static bool gameRunning = true;
        delegate String State();
        static String state = "";
        static State ActivateProgramState;
        static Program() {
            Thread.Sleep(100);
            ActivateProgramState = MaximizePleaseScreenState;
        }
        static String MaximizePleaseScreenState() {
            Thread.Sleep(100);
            GameManager.Instance.MaximizePleaseScreen();
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = StoryScreenState;
            return "MaximizePleaseScreenState";
        }
        static String StoryScreenState() {
            Thread.Sleep(100);
            GameManager.Instance.StoryScreen();
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = StartScreenState;
            return "StoryScreenState";
        }
        static String StartScreenState() {
            Thread.Sleep(100);
            String output = GameManager.Instance.StartScreen();
            if (output == "1") {
                ActivateProgramState = CreatePlayerScreenState;
                ActivateProgramState();
                return "CreatePlayerScreenState";
            }
            else if (output == "2") {
                ActivateProgramState = LoadPlayerScreenState;
                ActivateProgramState();
                return "LoadPlayerScreenState";
            }
            else if (output == "3") {
                ActivateProgramState = ExitScreenState;
                ActivateProgramState();
                return "ExitScreenState";
            }
            else {
                ActivateProgramState();
            }
            return "StartScreenState";
        }
        static String CreatePlayerScreenState() {
            String name = GameManager.Instance.CreatePlayerScreen();
            Console.WriteLine("Are you sure with that name? (Y/n): ");
            String input = Console.ReadLine();
            if (input.Contains("y", StringComparison.OrdinalIgnoreCase) || input == null) {
                Players aPlayer = new Players(name);
            }
            ActivateProgramState = MapScreenState;
            return "CreatePlayerScreenState";
        }
        static String LoadPlayerScreenState() {
            ActivateProgramState = MapScreenState;
            return "LoadPlayerScreenState";
        }
        static String MapScreenState() {
            Map.Instance.UpdateMap();
            Map.Instance.ShowMap();
            state = Players.Instance.MoveEnterStatusSaveExit();
            if (state == "Enter") {
                ActivateProgramState = PlaceScreenState;
            }
            else if (state == "Status") {
                ActivateProgramState = StatusScreenState;
            }
            else if (state == "Save") {
                ActivateProgramState = SaveScreenState;
            }
            else if (state == "Exit") {
                ActivateProgramState = ExitScreenState;
            }
            else {
                Console.WriteLine("Invalid input.");
                GameManager.Instance.PressEnterToContinue();
                ActivateProgramState();
            }
            return "MapScreenState";
        }
        static String StatusScreenState() {
            return "StatusScreenState";
        }
        static String PlaceScreenState() {
            return "PlaceScreenState";
        }
        static String SaveScreenState() {
            return "SaveScreenState";
        }
        static String ExitScreenState() {
            gameRunning = false;
            return "ExitScreenState";
        }

        static void Main(string[] args) {
            while (gameRunning) {
                ActivateProgramState();
            }
        }
    }
}