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
            Console.Write("Are you sure with that name? (Y/n): ");
            String input = Console.ReadLine();
            if (input.Contains("y", StringComparison.OrdinalIgnoreCase) || input == "") {
                Players.Instance.name = name;
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
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(100);
            Console.WriteLine($"Name: {Players.Instance.name}");
            Console.WriteLine($"HP: {Players.Instance.HP}/{Players.Instance.maxHP}");
            Console.WriteLine($"Level: {Players.Instance.level}");
            Console.WriteLine($"EXP: {Players.Instance.EXP}/{Players.Instance.maxEXP}");
            Console.WriteLine($"ATK: {Players.Instance.ATK}");
            Console.WriteLine($"DEF: {Players.Instance.DEF}");
            Console.WriteLine($"AGI: {Players.Instance.AGI}");
            Console.WriteLine($"Gold: {Players.Instance.gold}");
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = MapScreenState;
            return "MapScreenState";
        }
        static String PlaceScreenState() {
            return "PlaceScreenState";
        }
        static String SaveScreenState() {
            XMLOperator.Instance.SavePlayer();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(100);
            Console.WriteLine($"Name: {Players.Instance.name}");
            Console.WriteLine($"HP: {Players.Instance.HP}/{Players.Instance.maxHP}");
            Console.WriteLine($"Level: {Players.Instance.level}");
            Console.WriteLine($"EXP: {Players.Instance.EXP}/{Players.Instance.maxEXP}");
            Console.WriteLine($"ATK: {Players.Instance.ATK}");
            Console.WriteLine($"DEF: {Players.Instance.DEF}");
            Console.WriteLine($"AGI: {Players.Instance.AGI}");
            Console.WriteLine($"Gold: {Players.Instance.gold}");
            Console.WriteLine("\nSaved");
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = MapScreenState;
            return "MapScreenState";
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