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
    }
}