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
        static bool TryGo(Tuple<int, int> coordinat,ref Maze myMaze)
        {
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Exit)
            {
                Console.Clear();
                Console.WriteLine($"You win {StaticFields.Name}");
                Console.WriteLine($"Score: {myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Score}");
                Leaderboard.AddScore(StaticFields.Name,
                    myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Score);
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            if (myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent != null)
            {
                var health = myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Damage;
                if (Battle.process(ref health,myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent.Damage))
                {
                    myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Score += myMaze.MyMaze[coordinat.Item1, coordinat.Item2].Opponent.Damage * 100;
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
            int width = 0;
            int height = 0;
            switch (StaticFields.Difficulty)
            {
                case 1:
                    width = 10;
                    height = 5;
                    break;

                case 2:
                    width = 20;
                    height = 10;
                    break;

                case 3:
                    width = 35;
                    height = 15;
                    break;
            }

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
            while (myMaze.MyMaze[myMaze.Player.Item1, myMaze.Player.Item2].Player.Damage != 0)
            {
                ConsoleKey key = Console.ReadKey().Key;
                Tuple<int, int> coordinate;

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        coordinate = Tuple.Create(myMaze.Player.Item1 - 1, myMaze.Player.Item2);
                        if (!myMaze.MyMaze[coordinate.Item1 + 1, coordinate.Item2].N_wall && TryGo(coordinate,ref myMaze))
                        {
                            Fighter fighter = new Fighter(myMaze, coordinate.Item1, coordinate.Item2);
                        }
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        coordinate = Tuple.Create(myMaze.Player.Item1, myMaze.Player.Item2 - 1);
                        if (!myMaze.MyMaze[coordinate.Item1, coordinate.Item2 + 1].W_wall && TryGo(coordinate,ref myMaze))
                        {
                            Fighter fighter = new Fighter(myMaze, coordinate.Item1, coordinate.Item2);
                        }
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        coordinate = Tuple.Create(myMaze.Player.Item1, myMaze.Player.Item2 + 1);
                        if (!myMaze.MyMaze[coordinate.Item1, coordinate.Item2 - 1].E_wall && TryGo(coordinate,ref myMaze))
                        {
                            Fighter fighter = new Fighter(myMaze, coordinate.Item1, coordinate.Item2);
                        }
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        coordinate = Tuple.Create(myMaze.Player.Item1 + 1, myMaze.Player.Item2);
                        if (!myMaze.MyMaze[coordinate.Item1 - 1, coordinate.Item2].S_wall && TryGo(coordinate,ref myMaze))
                        {
                            Fighter fighter = new Fighter(myMaze, coordinate.Item1, coordinate.Item2);
                        }
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("You lose");
            System.Threading.Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}

