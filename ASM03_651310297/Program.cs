using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ASM03_651310297 {
    internal class Program {
        static Random aRandom = new Random();
        static bool gameRunning = true;
        delegate void State();
        static String state = "";
        static State ActivateProgramState;
        static Program() {
            Thread.Sleep(100);
            ActivateProgramState = MaximizePleaseScreenState;
        }
        static void MaximizePleaseScreenState() {
            Thread.Sleep(100);
            GameManager.Instance.MaximizePleaseScreen();
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = StoryScreenState;
        }
        static void StoryScreenState() {
            Thread.Sleep(100);
            GameManager.Instance.StoryScreen();
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = StartScreenState;
        }
        static void StartScreenState() {
            Thread.Sleep(100);
            String output = GameManager.Instance.StartScreen();
            if (output == "1") {
                ActivateProgramState = CreatePlayerScreenState;
                ActivateProgramState();
            }
            else if (output == "2") {
                ActivateProgramState = LoadPlayerScreenState;
                ActivateProgramState();
            }
            else if (output == "3") {
                ActivateProgramState = ExitScreenState;
                ActivateProgramState();
            }
            else {
                ActivateProgramState();
            }
        }
        static void CreatePlayerScreenState() {
            String name = GameManager.Instance.CreatePlayerScreen();
            Console.Write("Are you sure with that name? (Y/n): ");
            String input = Console.ReadLine();
            if (input.Contains("y", StringComparison.OrdinalIgnoreCase) || input == "") {
                Players.Instance.name = name;
            }
            ActivateProgramState = MapScreenState;
        }
        static void LoadPlayerScreenState() {
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
        }
        static void MapScreenState() {
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
        }
        static void StatusScreenState() {
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
        }
        static void PlaceScreenState() {
            Console.WriteLine(Players.Instance.position);
            switch (Players.Instance.position) {
                case 0:
                    ActivateProgramState = TownScreenState;
                    ActivateProgramState();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    ActivateProgramState = BattleScreenState;
                    ActivateProgramState();
                    break;
            }
        }

        static void TownScreenState() {
            String[] words = new String[] { "1. Shop", "2. Inn", "3. Exit" };
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 13);
            Console.Write("Input menu number to continue: ");
            String input = Console.ReadLine();
            if (input == "1") {
                ActivateProgramState = ShopScreenState;
            }
            else if (input == "2") {
                ActivateProgramState = InnScreenState;
            }
            else if (input == "3") {
                ActivateProgramState = MapScreenState;
            }
            else {
                Console.WriteLine("Invalid input.");
                GameManager.Instance.PressEnterToContinue();
                ActivateProgramState();
            }
        }

        static void ShopScreenState() {
        }
        static void InnScreenState() {
            String[] words = new String[] { "You have a fully sleep. zzZZ..", "HP restores to its max." };
            Console.Clear();
            Thread.Sleep(250);
            Players.Instance.HP = Players.Instance.maxHP;
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 13);
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = TownScreenState;
        }

        static void BattleScreenState() {
            switch (Players.Instance.position) {
                case 1:
                    int rng = aRandom.Next(1, 101);
                    if (rng >= 85) {
                        BigSlimes aBigSlime = new BigSlimes();
                        while (aBigSlime.HP >= 0) {
                            Console.Clear();
                            Thread.Sleep(250);
                            Sprites.Instance.PrintSprite(Sprites.Instance.bigSlime, 120, 12);
                            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
                            Console.SetCursorPosition(0, 0);
                            GameManager.Instance.PressEnterToContinue();
                            ActivateProgramState = MapScreenState;
                            break;
                        }
                    }
                    else {
                        Slimes aSlime = new Slimes();
                        while (aSlime.HP >= 0) {
                            Console.Clear();
                            Thread.Sleep(250);
                            Sprites.Instance.PrintSprite(Sprites.Instance.slime, 120, 30);
                            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
                            Console.SetCursorPosition(0, 0);
                            GameManager.Instance.PressEnterToContinue();
                            ActivateProgramState = MapScreenState;
                            break;
                        }
                    }
                    break;
                case 2:
                    Zombies aZombie = new Zombies();
                    while (aZombie.HP >= 0) {
                        Console.Clear();
                        Thread.Sleep(250);
                        Sprites.Instance.PrintSprite(Sprites.Instance.zombie, 120, 21);
                        Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
                        Console.SetCursorPosition(0, 0);
                        GameManager.Instance.PressEnterToContinue();
                        ActivateProgramState = MapScreenState;
                        break;
                    }
                    break;
                case 3:
                    Phoenixes aPhoenix = new Phoenixes();
                    while (aPhoenix.HP >= 0) {
                        Console.Clear();
                        Thread.Sleep(250);
                        Sprites.Instance.PrintSprite(Sprites.Instance.phoenix, 120, 2);
                        Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
                        Console.SetCursorPosition(0, 0);
                        GameManager.Instance.PressEnterToContinue();
                        ActivateProgramState = MapScreenState;
                        break;
                    }
                    break;
                case 4:
                    Dragons aDragon = new Dragons();
                    while (aDragon.HP >= 0) {
                        Console.Clear();
                        Thread.Sleep(250);
                        Sprites.Instance.PrintSprite(Sprites.Instance.dragon, 120, 1);
                        Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
                        Console.SetCursorPosition(0, 0);
                        GameManager.Instance.PressEnterToContinue();
                        ActivateProgramState = MapScreenState;
                        break;
                    }
                    break;
            }
        }

        static void SaveScreenState() {
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
        }
        static void ExitScreenState() {
            gameRunning = false;
        }

        static void Main(string[] args) {
            while (gameRunning) {
                ActivateProgramState();
            }
        }
    }
}