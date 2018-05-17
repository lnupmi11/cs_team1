using System;
using System.Collections.Generic;
using System.Net;
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
            Console.WriteLine(chosenOption == 3 ? "-> About       <-" : "   About");
            Console.WriteLine(chosenOption == 4 ? "-> Exit        <-" : "   Exit");
        }

        public static void NameMenu()
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

        public static void About()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\t\t Hello\nIt`s little guide how play this game\n\n" +
                              "Game have two parts: first it`s labyrinth and u have to find exit; second fight with your enemy\n\n" +
                              "*Labyrinth: You have four moves\n" +
                              "W - UP\n" +
                              "S - DOWN\n" +
                              "A - LEFT\n" +
                              "D - RIGHT\n" +
                              "Your position represent by &\n" +
                              "Numbers - it`s your enemy , and number represent how many hp ur oponent have\n\n" +
                              "*Fight with enemy: You have there moves\n" +
                              "<- - LEFT\n" +
                              "-> - RIGHT\n" +
                              "SPACE - FIRE\n" +
                              "Your goal it`s kill enemy as many times as he has health and back to labyrinth");
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
                                chosenOption = 4;
                            chosenOption--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (chosenOption == 4)
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
                    Menu.NameMenu();
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
                else if (chosenOption == 3)
                {
                    while (key != ConsoleKey.Escape)
                    {
                        Menu.About();
                        key = Console.ReadKey().Key;
                    }
                }

            }
        }
    }
}
