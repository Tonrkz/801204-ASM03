﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM03_651310297 {
    internal class GameManager {
        static readonly GameManager instance = new GameManager();
        public static GameManager Instance {
            get { return instance; }
        }

        public void MaximizePleaseScreen() {
            Console.WindowWidth = 200;
            Console.WindowHeight = 70;
            Console.BufferWidth = 250;
            Console.BufferHeight = 100;
            Console.Clear();
            Console.WriteLine("For a proper game displaying.\nThis game has to play with maximized or fullscreen window only.\nPlease enter fullscreen or maximized mode in your console app.\nThank you.");
        }

        public void StoryScreen() {
            String[] words = new String[] {
                "Once upon a time, there was a castle.",
                "The castle was taken over by dragons.",
                "We need a hero to save the castle..."
            };
            int y = Console.WindowHeight / 2 - 4;
            foreach (String word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 20);
        }

        public String StartScreen() {
            Console.Clear();
            Sprites.Instance.PrintSprite(Sprites.Instance.player, 50, 30);
            Sprites.Instance.PrintSprite(Sprites.Instance.dragon, 120, 2);
            String[] words = new String[] { "1. New Game", "2. Load Game", "3. Exit" };
            int y = Console.WindowHeight / 2 - 4;
            foreach (String word in words) {
                Console.SetCursorPosition((Console.WindowWidth - word.Length) / 2, y);
                Console.WriteLine(word);
                y++;
            }
            Console.SetCursorPosition(0, y + 18);
            Console.Write("Input number to continue: ");
            return Console.ReadLine();
        }

        public void CreatePlayerScreen() {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

        }

        public void PressEnterToContinue() {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
