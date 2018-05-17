using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBattle;
using ConsoleApplication1;

namespace LabyFights
{
    public class Play
    {
        static bool TryGo(Tuple<int, int> coordinat, Maze myMaze)
        {
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Exit)
            {
                Console.Clear();
                Console.WriteLine("You win");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
                string name = StaticFields.name;
            }
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent != null)
            {
                var health = myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Damage;
                if (Battle.process(ref health,myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent.Damage))
                {
                    myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Damage = health;
                    myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static void NewGame()
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
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.W)
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
                        System.Threading.Thread.Sleep(500);
                        for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
                        {
                            for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
                            {
                                myMaze.printCell(i, j);
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.A)
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
                        System.Threading.Thread.Sleep(500);
                        for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
                        {
                            for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
                            {
                                myMaze.printCell(i, j);
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.D)
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
                        System.Threading.Thread.Sleep(500);
                        for (int i = 0; i < myMaze.MyMaze.GetLength(0); i++)
                        {
                            for (int j = 0; j < myMaze.MyMaze.GetLength(1); j++)
                            {
                                myMaze.printCell(i, j);
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.S)
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
                        System.Threading.Thread.Sleep(500);
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
                    if(myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Damage == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("You lose");
                        System.Threading.Thread.Sleep(5000);
                        Environment.Exit(0);
                    }
                    Console.Clear();
                    Console.WriteLine("Your move is incorrect");
                    System.Threading.Thread.Sleep(500);
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
