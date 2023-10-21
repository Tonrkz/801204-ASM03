using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Map {
        private static Map instance;
        public static Map Instance {
            get {
                if (instance == null) {
                    instance = new Map();
                }
                return instance;
            }
        }
        public readonly bool[,] adjacencyMatrix = new bool[,] { { true, true, true, false, false },
                                                                { true, true, false, true, false },
                                                                { true, false, true, true, false },
                                                                { false, true, true, true, true },
                                                                { false, false, false, true, true } };

        public char[][] map = new char[][] {  "                  ======  2.PLAIN  =================  4.VOLCANO  ".ToCharArray() ,
                                              "            ======                           ===          ||     ".ToCharArray(),
                                              "  1.TOWN  ==                              ===             ||     ".ToCharArray(),
                                              "            ======                     ===                ||     ".ToCharArray(),
                                              "                  ======  3.FOREST  ===                   ||     ".ToCharArray(),
                                              "                                                      5.CASTLE   ".ToCharArray() };
        public void ShowMap() {
            Console.Clear();
            Thread.Sleep(250);
            int y = Console.WindowHeight / 2 - 10;
            foreach (var word in map) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 13);
        }

        public void UpdateMap() {
            switch (Players.Instance.position) {
                case 0:
                    map = new char[][] {  "                  ======  2.PLAIN  =================  4.VOLCANO  ".ToCharArray() ,
                                          "            ======                           ===          ||     ".ToCharArray(),
                                          " [1.TOWN] ==                              ===             ||     ".ToCharArray(),
                                          "            ======                     ===                ||     ".ToCharArray(),
                                          "                  ======  3.FOREST  ===                   ||     ".ToCharArray(),
                                          "                                                      5.CASTLE   ".ToCharArray() };
                    break;
                case 1:
                    map = new char[][] {  "                  ====== [2.PLAIN] =================  4.VOLCANO  ".ToCharArray() ,
                                          "            ======                           ===          ||     ".ToCharArray(),
                                          "  1.TOWN  ==                              ===             ||     ".ToCharArray(),
                                          "            ======                     ===                ||     ".ToCharArray(),
                                          "                  ======  3.FOREST  ===                   ||     ".ToCharArray(),
                                          "                                                      5.CASTLE   ".ToCharArray() };
                    break;
                case 2:
                    map = new char[][] {  "                  ======  2.PLAIN  =================  4.VOLCANO  ".ToCharArray() ,
                                          "            ======                           ===          ||     ".ToCharArray(),
                                          "  1.TOWN  ==                              ===             ||     ".ToCharArray(),
                                          "            ======                     ===                ||     ".ToCharArray(),
                                          "                  ====== [3.FOREST] ===                   ||     ".ToCharArray(),
                                          "                                                      5.CASTLE   ".ToCharArray() };
                    break;
                case 3:
                    map = new char[][] {  "                  ======  2.PLAIN  ================= [4.VOLCANO] ".ToCharArray() ,
                                          "            ======                           ===          ||     ".ToCharArray(),
                                          "  1.TOWN  ==                              ===             ||     ".ToCharArray(),
                                          "            ======                     ===                ||     ".ToCharArray(),
                                          "                  ======  3.FOREST  ===                   ||     ".ToCharArray(),
                                          "                                                      5.CASTLE   ".ToCharArray() };
                    break;
                case 4:
                    map = new char[][] {  "                  ======  2.PLAIN  =================  4.VOLCANO  ".ToCharArray() ,
                                          "            ======                           ===          ||     ".ToCharArray(),
                                          "  1.TOWN  ==                              ===             ||     ".ToCharArray(),
                                          "            ======                     ===                ||     ".ToCharArray(),
                                          "                  ======  3.FOREST  ===                   ||     ".ToCharArray(),
                                          "                                                      [5.CASTLE] ".ToCharArray() };
                    break;
            }
        }

        public void ShowBigSlimeBattle() {
            Console.Clear();
            Thread.Sleep(250);
            Sprites.Instance.PrintSprite(Sprites.Instance.bigSlime, 120, 12);
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
            Console.SetCursorPosition(0, 41);
        }

        public void ShowSlimeBattle() {
            Console.Clear();
            Thread.Sleep(250);
            Sprites.Instance.PrintSprite(Sprites.Instance.slime, 120, 30);
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
            Console.SetCursorPosition(0, 41);
        }

        public void ShowZombieBattle() {
            Console.Clear();
            Thread.Sleep(250);
            Sprites.Instance.PrintSprite(Sprites.Instance.zombie, 120, 21);
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
            Console.SetCursorPosition(0, 41);
        }

        public void ShowPhoenixBattle() {
            Console.Clear();
            Thread.Sleep(250);
            Sprites.Instance.PrintSprite(Sprites.Instance.phoenix, 120, 2);
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
            Console.SetCursorPosition(0, 41);
        }

        public void ShowDragonBattle() {
            Console.Clear();
            Thread.Sleep(250);
            Sprites.Instance.PrintSprite(Sprites.Instance.dragon, 120, 1);
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 18);
            Console.SetCursorPosition(0, 41);
        }
    }
}