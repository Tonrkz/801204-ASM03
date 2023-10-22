using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASM03_651310297 {
    internal class Shop {
        private static Shop instance;
        public bool inShop = false;
        public static Shop Instance {
            get {
                if (instance == null) {
                    instance = new Shop();
                }
                return instance;
            }
        }

        List<XElement> swordList = XMLOperator.Instance.LoadSwords().Elements("sword").ToList();
        List<XElement> shieldList = XMLOperator.Instance.LoadShields().Elements("shield").ToList();

        public delegate void ShopState();
        public ShopState shopState;
        public Shop() {
            shopState = ShowShop;
        }

        public void ShowShop() {
            Console.Clear();
            Thread.Sleep(100);
            String[] words = new String[] { "Welcome to the shop!", "", $"You have {Players.Instance.gold} gold.", "", "1. Buy a sword.", "2. Buy a shield.", "3. Exit." };
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, 41);
            Console.Write("Input: ");
            string input = Console.ReadLine();
            switch (input) {
                case "1":
                    ShowSwords();
                    break;
                case "2":
                    break;
                case "3":
                    inShop = false;
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }

        public void ShowSwords() {
            Console.Clear();
            Thread.Sleep(100);
            for (int i = 1 ; i < 6 ; i++) {
                XElement sword = swordList[i];
                Console.WriteLine($"Name: {sword.Element("name").Value}\n" +
                                  $"ATK: {sword.Element("ATK").Value}\n" +
                                  $"DEF: {sword.Element("DEF").Value}\n" +
                                  $"AGI: {sword.Element("AGI").Value}\n" +
                                  $"Price: {sword.Element("price").Value}\n");
            }
            int x = 30;
            int y = 0;
            for (int i = 6 ; i <= 10; i++) {
                XElement sword = swordList[i];
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"Name: {sword.Element("name").Value}");
                y++;
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"ATK: {sword.Element("ATK").Value}");
                y++;
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"DEF: {sword.Element("DEF").Value}");
                y++;
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"AGI: {sword.Element("AGI").Value}");
                y++;
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"Price: {sword.Element("price").Value}");
                y++;
                Console.SetCursorPosition(x, y);
                Console.WriteLine("\n");
                y++;
            }
            Console.SetCursorPosition(0, 41);
            Console.Write("Input sword's name to buy: ");
            string input = Console.ReadLine();
            XElement chosenSword = swordList.Find(x => x.Element("name").Value == input);
            if (chosenSword != null) {
                if (Players.Instance.gold >= int.Parse(chosenSword.Element("price").Value)) {
                    Players.Instance.gold -= int.Parse(chosenSword.Element("price").Value);
                    Console.WriteLine($"\nYou bought {chosenSword.Element("name").Value}!");
                    Console.WriteLine($"You have {Players.Instance.gold} gold left.");
                    Console.WriteLine(chosenSword);
                    GameManager.Instance.PressEnterToContinue();
                    Players.Instance.sword.Dequip();
                    Players.Instance.sword = new Swords(Byte.Parse(chosenSword.Attribute("id").Value), chosenSword.Element("name").Value, int.Parse(chosenSword.Element("ATK").Value), int.Parse(chosenSword.Element("DEF").Value), int.Parse(chosenSword.Element("AGI").Value), int.Parse(chosenSword.Element("price").Value));
                    Players.Instance.sword.Equip();
                    shopState = ShowShop;
                }
                else {
                    Console.WriteLine("You don't have enough gold!");
                    GameManager.Instance.PressEnterToContinue();
                    shopState = ShowSwords;
                }
            }
            else {
                Console.WriteLine("Invalid input!");
                GameManager.Instance.PressEnterToContinue();
                shopState = ShowSwords;
            }
        }
    }
}
