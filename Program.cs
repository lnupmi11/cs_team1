using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyFights;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random mainRdm = new Random();
            int width = 15;
            int height = 10;
            List<Fighter> fighters = new List<Fighter>();
            Console.SetWindowSize((width * 4) + 2, (height * 4) + 2);
            Maze myMaze = new Maze(width, height, new Random());
            Console.Clear();
            for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
            {
                for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
                {
                    myMaze.printCell(i, j);
                }
            }
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.W)
                {
                    
                }
                else if(key.Key == ConsoleKey.A)
                {

                }
                else if(key.Key == ConsoleKey.D)
                {

                }
                else if(key.Key == ConsoleKey.S)
                {

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Your move is incorrect");
                    Console.ReadKey();
                    for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
                    {
                        for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
                        {
                            myMaze.printCell(i, j);
                        }
                    }
                }
            }
            Thread.Sleep(2000);
            Fighter fighter = new Fighter(myMaze, 1, 1);
            Console.ReadKey();
        }
    }
}
