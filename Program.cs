using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabyFights;
using System.Threading;

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
            //ToDo
            //while (myMaze.Player != myMaze.Exit)
            //{
            //    ConsoleKeyInfo name = Console.ReadKey();
            //    Tuple<int, int> coordinat;
            //    if (name.KeyChar == 'w')
            //    {
            //        coordinat = new Tuple<int, int>(myMaze.Player.Item1, myMaze.Player.Item2 + 1);
            //    }
            //    else if (name.KeyChar == 's')
            //    {
            //        coordinat = new Tuple<int, int>(myMaze.Player.Item1, myMaze.Player.Item2 - 1);
            //    }
            //    else if (name.KeyChar == 'd')
            //    {
            //        coordinat = new Tuple<int, int>(myMaze.Player.Item1 + 1, myMaze.Player.Item2);
            //    }
            //    else if(name.KeyChar == 'a')
            //    {
            //        coordinat = new Tuple<int, int>(myMaze.Player.Item1 + 1, myMaze.Player.Item2);
            //    }
            //    else
            //    {
            //        coordinat = new Tuple<int, int>(0, 0);
            //    }

            //    myMaze = new Maze(width, height, mainRdm, myMaze.Exit, coordinat, 100);
            //    Console.Clear();
            //    for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
            //        {
            //            myMaze.printCell(i, j);
            //        }
            //    }
            //}
            Console.ReadKey();
        }
    }
}
