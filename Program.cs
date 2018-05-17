using System;
using System.Collections.Generic;
using LabyFights;

namespace ConsoleApplication1
{
    struct StaticFields
    {
        public static int Difficulty;
        public static string Name;
    }

    class Menu
    {
        public static void FirstMenu(int chosenOption)
        {
            Console.WriteLine(chosenOption == 1 ? "-> New Game    <-" : "   New Game");
            Console.WriteLine(chosenOption == 2 ? "-> Leaderboard <-" : "   Leaderboard");
            Console.WriteLine(chosenOption == 3 ? "-> Exit        <-" : "   Exit");
        }

        public static void SecondMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter your name:");
            StaticFields.Name = Console.ReadLine();
        }

        public static void ThirdMenu(int chosenOption)
        {
            Console.Clear();
            Console.WriteLine(chosenOption == 1 ? "-> Easy    <-" : "   Easy");
            Console.WriteLine(chosenOption == 2 ? "-> Medium  <-" : "   Medium");
            Console.WriteLine(chosenOption == 3 ? "-> Hard    <-" : "   Hard");
        }

        public static void LeaderboardMenu()
        {
            Console.Clear();
            Console.WriteLine(Leaderboard.OpenLeaderboard());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int chosenOption = 1;
            ConsoleKey key = new ConsoleKey();
            while (true)
            {
                while (key != ConsoleKey.Enter && key != ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    Menu.FirstMenu(chosenOption);
                    key = Console.ReadKey().Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (chosenOption == 1)
                                chosenOption = 3;
                            chosenOption--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (chosenOption == 3)
                                chosenOption = 1;
                            chosenOption++;
                            break;
                        default:
                            break;
                    }
                }

                if (chosenOption == 1)
                {
                    Console.WriteLine("New game");
                    Menu.SecondMenu();
                    key = new ConsoleKey();
                    while (key != ConsoleKey.Enter && key != ConsoleKey.RightArrow)
                    {
                        Console.Clear();
                        Menu.ThirdMenu(chosenOption);
                        key = Console.ReadKey().Key;

                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                if (chosenOption == 1)
                                    chosenOption = 3;
                                chosenOption--;
                                break;
                            case ConsoleKey.DownArrow:
                                if (chosenOption == 3)
                                    chosenOption = 1;
                                chosenOption++;
                                break;
                            default:
                                break;
                        }
                    }

                    StaticFields.Difficulty = chosenOption;
                    Play.NewGame();
                }
                else if (chosenOption == 2)
                {
                    while (key != ConsoleKey.Escape)
                    {
                        Menu.LeaderboardMenu();
                        key = Console.ReadKey().Key;
                    }
                }
            }
        }
    }
}
