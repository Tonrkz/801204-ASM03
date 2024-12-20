﻿using System.Data.Common;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ASM03_651310297 {
    internal class Program {
        static Program() {
            Thread.Sleep(100);
            ActivateProgramState = MaximizePleaseScreenState;
        }
        static Random aRandom = new Random();
        static bool gameRunning = true;
        static bool dragonDead = false;
        delegate void State();
        static String state = "";
        static State ActivateProgramState;
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
            Console.Clear();
            Thread.Sleep(100);
            String output = GameManager.Instance.StartScreen();
            if (output == "1") {
                ActivateProgramState = CreatePlayerScreenState;
            }
            else if (output == "2") {
                ActivateProgramState = LoadPlayerScreenState;
            }
            else if (output == "3") {
                ActivateProgramState = ExitGameState;
            }
        }

        static void CreatePlayerScreenState() {
            String name = GameManager.Instance.CreatePlayerScreen();
            Console.Write("Are you sure with that name? (Y/n): ");
            String input = Console.ReadLine();
            if (input == "y" || input == "Y" || input == "") {
                if (name == "") {
                    name = "Player";
                }
                Players.Instance.SetAllDefault();
                Players.Instance.name = name;
                ActivateProgramState = MapScreenState;
            }
            else {
                ActivateProgramState = StartScreenState;
            }
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
                    Console.WriteLine($"Level: {player.Element("level").Value}");
                    Console.WriteLine($"HP: {player.Element("HP").Value}/{player.Element("maxHP").Value}");
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
                    Players.Instance.level = Byte.Parse(player.Element("level").Value);
                    Players.Instance.EXP = int.Parse(player.Element("EXP").Value);
                    Players.Instance.maxEXP = int.Parse(player.Element("maxEXP").Value);
                    Players.Instance.ATK = int.Parse(player.Element("ATK").Value);
                    Players.Instance.DEF = int.Parse(player.Element("DEF").Value);
                    Players.Instance.AGI = int.Parse(player.Element("AGI").Value);
                    Players.Instance.position = Byte.Parse(player.Element("position").Value);
                    Players.Instance.gold = int.Parse(player.Element("gold").Value);
                    Players.Instance.swordID = Byte.Parse(player.Element("swordID").Value);
                    Players.Instance.shieldID = Byte.Parse(player.Element("shieldID").Value);
                    dragonDead = bool.Parse(player.Element("dragonDefeat").Value);

                    List<XElement> swordsList = XMLOperator.Instance.LoadSwords().Elements("sword").ToList();
                    XElement chosenSword = swordsList.Find(x => x.Attribute("id").Value == player.Element("swordID").Value);
                    Players.Instance.sword = new Swords(Byte.Parse(chosenSword.Attribute("id").Value), chosenSword.Element("name").Value, int.Parse(chosenSword.Element("ATK").Value), int.Parse(chosenSword.Element("DEF").Value), int.Parse(chosenSword.Element("AGI").Value), int.Parse(chosenSword.Element("price").Value));

                    List<XElement> shieldsList = XMLOperator.Instance.LoadShields().Elements("shield").ToList();
                    XElement chosenShield = shieldsList.Find(x => x.Attribute("id").Value == player.Element("shieldID").Value);
                    Players.Instance.shield = new Shields(Byte.Parse(chosenShield.Attribute("id").Value), chosenShield.Element("name").Value, int.Parse(chosenShield.Element("ATK").Value), int.Parse(chosenShield.Element("DEF").Value), int.Parse(chosenShield.Element("AGI").Value), int.Parse(chosenShield.Element("price").Value));
                    choose = true;
                }
                else {
                    Console.WriteLine("Invalid name.");
                    GameManager.Instance.PressEnterToContinue();
                    ActivateProgramState = StartScreenState;
                    return;
                }
            }
            ActivateProgramState = MapScreenState;
        }

        static void MapScreenState() {
            Map.Instance.UpdateMap();
            Map.Instance.ShowMap();
            state = Players.Instance.MoveEnterStatusSaveExit();
            if (state == "Enter") {
                switch (Players.Instance.position) {
                    case 0:
                        ActivateProgramState = TownScreenState;
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        ActivateProgramState = BattleScreenState;
                        break;
                }
            }
            else if (state == "Status") {
                ActivateProgramState = StatusScreenState;
            }
            else if (state == "Save") {
                ActivateProgramState = SaveScreenState;
            }
            else if (state == "Exit") {
                ActivateProgramState = StartScreenState;
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
            Players.Instance.Status();
            ActivateProgramState = MapScreenState;
        }

        static void TownScreenState() {
            String[] words = new String[] { "Welcome to the town!", "", "1. Visit a shop.", "2. Sleep in hotel.", "3. Exit." };
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
            Shop.Instance.inShop = true;
            while (Shop.Instance.inShop) {
                Shop.Instance.shopState();
            }
            ActivateProgramState = TownScreenState;
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
                    if (rng >= 90) {
                        BigSlimes aBigSlime = new BigSlimes();
                        Players.Instance.isEscape = false;
                        while (aBigSlime.HP >= 0) {
                            Map.Instance.ShowBattle(aBigSlime.name);
                            if (Players.Instance.HP <= 0) {
                                Players.Instance.Dead(aBigSlime, dragonDead);
                                ActivateProgramState = StoryScreenState;
                                return;
                            }
                            Players.Instance.ChooseBattleAction(aBigSlime);
                            if (Players.Instance.isEscape) {
                                break;
                            }
                            Map.Instance.ShowBattle(aBigSlime.name);
                            aBigSlime.ChooseBattleAction();
                            aBigSlime.monsterAction();
                            if (aBigSlime.isEscape) {
                                break;
                            }
                        }
                        ActivateProgramState = MapScreenState;
                    }
                    else {
                        Slimes aSlime = new Slimes();
                        Players.Instance.isEscape = false;
                        while (aSlime.HP > 0) {
                            Map.Instance.ShowBattle(aSlime.name);
                            if (Players.Instance.HP <= 0) {
                                Players.Instance.Dead(aSlime, dragonDead);
                                ActivateProgramState = StoryScreenState;
                                return;
                            }
                            Players.Instance.ChooseBattleAction(aSlime);
                            if (Players.Instance.isEscape) {
                                break;
                            }
                            Map.Instance.ShowBattle(aSlime.name);
                            aSlime.ChooseBattleAction();
                            aSlime.monsterAction();
                            if (aSlime.isEscape) {
                                break;
                            }
                        }
                        ActivateProgramState = MapScreenState;
                    }
                    break;

                case 2:
                    rng = aRandom.Next(1, 101);
                    if (rng <= 20) {
                        BigSlimes aBigSlime = new BigSlimes();
                        Players.Instance.isEscape = false;
                        while (aBigSlime.HP >= 0) {
                            Map.Instance.ShowBattle(aBigSlime.name);
                            if (Players.Instance.HP <= 0) {
                                Players.Instance.Dead(aBigSlime, dragonDead);
                                ActivateProgramState = StoryScreenState;
                                return;
                            }
                            Players.Instance.ChooseBattleAction(aBigSlime);
                            if (Players.Instance.isEscape) {
                                break;
                            }
                            Map.Instance.ShowBattle(aBigSlime.name);
                            aBigSlime.ChooseBattleAction();
                            aBigSlime.monsterAction();
                            if (aBigSlime.isEscape) {
                                break;
                            }
                        }
                    }
                    else {
                        Zombies aZombie = new Zombies();
                        Players.Instance.isEscape = false;
                        while (aZombie.HP > 0) {
                            Map.Instance.ShowBattle(aZombie.name);
                            if (Players.Instance.HP <= 0) {
                                Players.Instance.Dead(aZombie, dragonDead);
                                ActivateProgramState = StoryScreenState;
                                return;
                            }
                            Players.Instance.ChooseBattleAction(aZombie);
                            if (Players.Instance.isEscape) {
                                break;
                            }
                            Map.Instance.ShowBattle(aZombie.name);
                            aZombie.ChooseBattleAction();
                            aZombie.monsterAction();
                            if (aZombie.isEscape) {
                                break;
                            }
                        }
                    }
                    ActivateProgramState = MapScreenState;
                    break;

                case 3:
                    if (true) {
                        BigSlimes aBigSlime = new BigSlimes();
                        Players.Instance.isEscape = false;
                        while (aBigSlime.HP >= 0) {
                            Map.Instance.ShowBattle(aBigSlime.name);
                            if (Players.Instance.HP <= 0) {
                                Players.Instance.Dead(aBigSlime, dragonDead);
                                ActivateProgramState = StoryScreenState;
                                return;
                            }
                            Players.Instance.ChooseBattleAction(aBigSlime);
                            if (Players.Instance.isEscape) {
                                break;
                            }
                            Map.Instance.ShowBattle(aBigSlime.name);
                            aBigSlime.ChooseBattleAction();
                            aBigSlime.monsterAction();
                            if (aBigSlime.isEscape) {
                                break;
                            }
                        }
                    }
                    ActivateProgramState = MapScreenState;
                    break;

                case 4:
                    Phoenixes aPhoenix = new Phoenixes();
                    Players.Instance.isEscape = false;
                    while (aPhoenix.HP >= 0) {
                        Map.Instance.ShowBattle(aPhoenix.name);
                        if (Players.Instance.HP <= 0) {
                            Players.Instance.Dead(aPhoenix, dragonDead);
                            ActivateProgramState = StoryScreenState;
                            return;
                        }
                        Players.Instance.ChooseBattleAction(aPhoenix);
                        if (Players.Instance.isEscape) {
                            break;
                        }
                        Map.Instance.ShowBattle(aPhoenix.name);
                        aPhoenix.ChooseBattleAction();
                        aPhoenix.monsterAction();
                        if (aPhoenix.isEscape) {
                            break;
                        }
                    }
                    ActivateProgramState = MapScreenState;
                    break;

                case 5:
                    Dragons aDragon = new Dragons();
                    Players.Instance.isEscape = false;
                    if (dragonDead) {
                        ActivateProgramState = EndGameScreenState;
                        break;
                    }
                    while (!dragonDead) {
                        Map.Instance.ShowBattle(aDragon.name);
                        if (Players.Instance.HP <= 0) {
                            Players.Instance.Dead(aDragon, dragonDead);
                            ActivateProgramState = StoryScreenState;
                            return;
                        }
                        Players.Instance.ChooseBattleAction(aDragon);
                        if (Players.Instance.isEscape) {
                            break;
                        }
                        if (aDragon.HP <= 0) {
                            ActivateProgramState = EndGameScreenState;
                            ActivateProgramState();
                            dragonDead = true;
                        }
                        Map.Instance.ShowBattle(aDragon.name);
                        aDragon.ChooseBattleAction();
                        aDragon.monsterAction();
                    }
                    ActivateProgramState = MapScreenState;
                    break;
            }
        }

        static void SaveScreenState() {
            XMLOperator.Instance.SavePlayer(dragonDead);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(100);
            Console.WriteLine($"Name: {Players.Instance.name}\nHP: {Players.Instance.HP}/{Players.Instance.maxHP}\nLevel: {Players.Instance.level}\nEXP: {Players.Instance.EXP}/{Players.Instance.maxEXP}\nATK: {Players.Instance.ATK}\nDEF: {Players.Instance.DEF}\nAGI: {Players.Instance.AGI}\nGold: {Players.Instance.gold}\n\nSword: {Players.Instance.sword.name}\nShield: {Players.Instance.shield.name}\n");
            Console.WriteLine("\nSaved.");
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = MapScreenState;
        }

        static void EndGameScreenState() {
            String[] words = new String[] { "You have defeated the Dragon.", "There is no dragon anymore.", "Peace has come to this world again." };
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 13);
            GameManager.Instance.PressEnterToContinue();
            ActivateProgramState = MapScreenState;
        }

        static void ExitGameState() {
            gameRunning = false;
            String[] words = new String[] { "Thank you for playing." };
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static void Main(string[] args) {
            ActivateProgramState();
            while (gameRunning) {
                Console.WindowWidth = 200;
                Console.WindowHeight = 70;
                Console.BufferWidth = 250;
                Console.BufferHeight = 100;
                Console.SetWindowSize(200, 70);
                Console.SetBufferSize(250, 100);
                ShowWindow(ThisConsole, MAXIMIZE);
                ActivateProgramState();
            }
        }
    }
}