using System.Runtime.InteropServices;
using System.Xml.Linq;

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
            return "MapScreenState";
        }
        static String LoadPlayerScreenState() {
            bool choose = false;
            while (!choose) {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(100);
                List<XElement> playerList = XMLOperator.Instance.LoadPlayer().Elements("player").ToList();
                foreach (XElement player in playerList) {
                    Console.WriteLine($"Name: {player.Attribute("name").Value}");
                    Console.WriteLine($"HP: {player.Element("HP").Value}/{player.Element("maxHP").Value}");
                    Console.WriteLine($"Level: {player.Element("level").Value}");
                    Console.WriteLine($"EXP: {player.Element("EXP").Value}/{player.Element("maxEXP").Value}");
                    Console.WriteLine($"Gold: {player.Element("gold").Value}\n");
                }
                Console.Write("\nInput player's name to load: ");
                String name = Console.ReadLine();
                if (XMLOperator.Instance.LoadPlayer().Elements("player").Any(item => item.Attribute("name").Value == name)) {
                    XElement player = XMLOperator.Instance.LoadPlayer().Elements("player").Where(item => item.Attribute("name").Value == name).FirstOrDefault();
                    Players.Instance.name = player.Attribute("name").Value;
                    Players.Instance.HP = int.Parse(player.Element("HP").Value);
                    Players.Instance.maxHP = int.Parse(player.Element("maxHP").Value);
                    Players.Instance.level = int.Parse(player.Element("level").Value);
                    Players.Instance.EXP = int.Parse(player.Element("EXP").Value);
                    Players.Instance.maxEXP = int.Parse(player.Element("maxEXP").Value);
                    Players.Instance.ATK = int.Parse(player.Element("ATK").Value);
                    Players.Instance.DEF = int.Parse(player.Element("DEF").Value);
                    Players.Instance.AGI = int.Parse(player.Element("AGI").Value);
                    Players.Instance.position = int.Parse(player.Element("position").Value);
                    Players.Instance.gold = int.Parse(player.Element("gold").Value);

                    choose = true;
                }
                else {
                    Console.WriteLine("Invalid name.");
                    GameManager.Instance.PressEnterToContinue();
                }
            }
            ActivateProgramState = MapScreenState;
            return "MapScreenState";
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