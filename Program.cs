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

        public static void SecondMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter your name:");
            StaticFields.name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter your complexity:");
            string stream = Console.ReadLine();
            bool check = Int32.TryParse(stream, out StaticFields.complexity);

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
            while(check != true)
            {
                Console.Clear();
                Menu.FirstMenu();
                stream = Console.ReadLine();
                check = Int32.TryParse(stream, out choice);
            }
            switch (choice)
            {
                case 1:
                    Menu.SecondMenu();
                    break;
            }
            
            Console.WriteLine("New game");
            Play.NewGame();
        }
    }
}
