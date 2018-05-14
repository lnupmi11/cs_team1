using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LabyFights
{
    public class Fighter
    {
        int currentLife;
        Maze maze;
        int currentRow;
        int currentCol;

        public Fighter(Maze maze, int row, int col)
        {
            this.maze = maze;
            this.currentCol = col;
            this.currentRow = row;
            this.currentLife = maze.MyMaze[maze.Player.Item1, maze.Player.Item2].Player.Damage;
            this.maze.MyMaze[row, col].Fighter = true;
            this.maze.printCell(row, col);
            Thread threadWhosMovingFighter = new Thread(Move);
            threadWhosMovingFighter.Start();

        }

        private void Move()
        {
            var coordinat = maze.Player;
            maze.MyMaze[coordinat.Item1, coordinat.Item2].Player = null;
            maze.Player = Tuple.Create(currentRow, currentCol);
            coordinat = maze.Player;
            maze.MyMaze[coordinat.Item1, coordinat.Item2].Player = new Player(currentLife);
            Thread.Sleep(1000);
            Console.Clear();
            for (int i = 0; i < maze.MyMaze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.MyMaze.GetLength(1); j++)
                {
                    maze.printCell(i, j);
                }
            }
            Thread.Sleep(2000);
        }

    }
}
