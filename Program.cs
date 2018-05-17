using System;
using System.Collections.Generic;
using LabyFights;

namespace ConsoleApplication1
{
    class Program
    {
        static bool TryGo(Tuple<int,int> coordinat, Maze myMaze)
        {
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Exit)
            {
                Console.Clear();
                Console.WriteLine("You win");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent != null)
            {
                //TODO
                myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent = null;
                return true;
            }
            return true;
        }

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
                    var coordinat = Tuple.Create(myMaze.Player.Item1 - 1, myMaze.Player.Item2);
                    if (!myMaze.MyMaze[coordinat.Item1 + 1, coordinat.Item2].N_wall && TryGo(coordinat, myMaze))
                    {
                        Fighter fighter = new Fighter(myMaze, coordinat.Item1, coordinat.Item2);
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
                else if(key.Key == ConsoleKey.A)
                {
                    var coordinat = Tuple.Create(myMaze.Player.Item1, myMaze.Player.Item2 - 1);
                    if (!myMaze.MyMaze[coordinat.Item1, coordinat.Item2 + 1].W_wall && TryGo(coordinat, myMaze))
                    {
                        Fighter fighter = new Fighter(myMaze, coordinat.Item1, coordinat.Item2);
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
                else if(key.Key == ConsoleKey.D)
                {
                    var coordinat = Tuple.Create(myMaze.Player.Item1, myMaze.Player.Item2 + 1);
                    if (!myMaze.MyMaze[coordinat.Item1, coordinat.Item2 - 1].E_wall && TryGo(coordinat, myMaze))
                    {
                        Fighter fighter = new Fighter(myMaze, coordinat.Item1, coordinat.Item2);
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
                else if(key.Key == ConsoleKey.S)
                {
                    var coordinat = Tuple.Create(myMaze.Player.Item1 + 1, myMaze.Player.Item2);
                    if (!myMaze.MyMaze[coordinat.Item1 - 1, coordinat.Item2].S_wall && TryGo(coordinat, myMaze))
                    {
                        Fighter fighter = new Fighter(myMaze, coordinat.Item1, coordinat.Item2);
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
        }
    }
}
