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
                    break;
            }            
        }
    }
}
