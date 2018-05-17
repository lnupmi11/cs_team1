using System;
using System.Collections.Generic;
using LabyFights;

namespace ConsoleApplication1
{
    class StaticFields
    {
        public static int complexity;
        public static string name;
    }

    class Menu
    {
        public static void FirstMenu()
        {
            Console.WriteLine("1) New game");
            Console.WriteLine("2) About");
            Console.WriteLine("3) Exit");
        }

        public static void NameMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter your name:");
            StaticFields.name = Console.ReadLine();
        }

        public static void ComplexityMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter your complexity:");
            Console.WriteLine("1) Easy");
            Console.WriteLine("2) Medium");
            Console.WriteLine("3) Hard");
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            string stream;
            Menu.FirstMenu();
            stream = Console.ReadLine();
            bool check = Int32.TryParse(stream, out choice);
            while(check != true || choice > 3 || choice < 1)
            {
                Console.Clear();
                Menu.FirstMenu();
                stream = Console.ReadLine();
                check = Int32.TryParse(stream, out choice);
            }
            switch (choice)
            {
                case 1:
                    Menu.NameMenu();
                    Menu.ComplexityMenu();
                    stream = Console.ReadLine();
                    check = Int32.TryParse(stream, out StaticFields.complexity);
                    while (check != true || StaticFields.complexity > 3 || StaticFields.complexity < 1)
                    {
                        Menu.ComplexityMenu();
                        stream = Console.ReadLine();
                        check = Int32.TryParse(stream, out StaticFields.complexity);
                    }
                    Play.NewGame();
                    break;
                case 2:
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
                    Console.ReadKey();
                    break;

                case 3:
                    break;

                default:
                    break;
            }   
        }
    }
}
