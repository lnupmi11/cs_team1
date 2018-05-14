using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyFights
{
    public class Maze
    {
        private Cell[,] myMaze;
        private int width;
        private int height;
        Random rdm;
        Tuple<int, int> exit;
        Tuple<int, int> player;
        public Cell[,] MyMaze
        {
            get
            {
                return myMaze;
            }

            set
            {
                myMaze = value;
            }
        }

        public Tuple<int, int> Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        public Tuple<int, int> Exit
        {
            get
            {
                return exit;
            }

            set
            {
                exit = value;
            }
        }

        public Maze(int width, int height, Random rdm)
        {
            this.width = width;
            this.height = height;
            this.myMaze = new Cell[height, width];
            this.rdm = rdm;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.myMaze[row, col] = new Cell();
                }
            }
            Dig(0, 0, new Stack<Tuple<int, int>>());
            DigExit();
            DigPlayer();
            InitOpponent();
        }

        private Tuple<int, int> SelectNeighbour(int row, int col)
        {
            Random myRdm = this.rdm;
            List<Tuple<int, int>> myList = new List<Tuple<int, int>>();

            if (row > 0 && !this.myMaze[row - 1, col].Visited)
            {
                myList.Add(Tuple.Create(row - 1, col));
            }
            if (row < this.myMaze.GetLength(0) - 1 && !this.myMaze[row + 1, col].Visited)
            {
                myList.Add(Tuple.Create(row + 1, col));
            }
            if (col > 0 && !this.myMaze[row, col - 1].Visited)
            {
                myList.Add(Tuple.Create(row, col - 1));
            }
            if (col < this.myMaze.GetLength(1) - 1 && !this.myMaze[row, col + 1].Visited)
            {
                myList.Add(Tuple.Create(row, col + 1));
            }
            if (myList.Count == 0)
            {
                return Tuple.Create(-1, -1);
            }
            else
            {
                int rdmInt = rdm.Next(myList.Count);
                return myList[rdmInt];
            }
        }

        private void DestroyWall(int row, int col, int nextRow, int nextCol)
        {
            int diffX = row - nextRow;
            if (diffX == 1)
            {
                this.myMaze[row, col].N_wall = false;
                this.myMaze[nextRow, nextCol].S_wall = false;
            }
            if (diffX == -1)
            {
                this.myMaze[row, col].S_wall = false;
                this.myMaze[nextRow, nextCol].N_wall = false;
            }
            int diffY = col - nextCol;
            if (diffY == 1)
            {
                this.myMaze[row, col].W_wall = false;
                this.myMaze[nextRow, nextCol].E_wall = false;
            }
            if (diffY == -1)
            {
                this.myMaze[row, col].E_wall = false;
                this.myMaze[nextRow, nextCol].W_wall = false;
            }
        }

        private void InitOpponent()
        {
            Random random = new Random();
            int nbCellRequired = Convert.ToInt32((this.width * this.height) * 0.1);
            while (nbCellRequired > 0)
            {
                int row = random.Next(this.height);
                int col = random.Next(this.width);
                if (this.myMaze[row, col].Opponent == null && !this.myMaze[row, col].Exit)
                {
                    myMaze[row, col].Opponent = new Opponent(random.Next(1, 11));
                    nbCellRequired -= 1;
                }
            }
        }

        private void Dig(int row, int col, Stack<Tuple<int, int>> stack)
        {

            this.myMaze[row, col].Visited = true;
            var neighbour = SelectNeighbour(row, col);
            int nextRow = neighbour.Item1;
            int nextCol = neighbour.Item2;

            if (nextRow != -1)
            {
                stack.Push(Tuple.Create(row, col));
                DestroyWall(row, col, nextRow, nextCol);
                Dig(nextRow, nextCol, stack);

            }
            else if (stack.Count > 0)
            {
                var a = stack.Pop();
                Dig(a.Item1, a.Item2, stack);
            }
        }

        private void DigExit()
        {
            int row = rdm.Next(this.height);
            int col = rdm.Next(this.width);
            myMaze[row, col].Exit = true;
            this.exit = Tuple.Create(row, col);
        }

        private void DigPlayer()
        {
            int row, col;
            row = rdm.Next(this.height);
            col = rdm.Next(this.width);
            myMaze[row, col].Player = new Player(100);
            this.player = Tuple.Create(row, col);
        }

        public void printCell(int row, int col)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            if (this.myMaze[row, col].N_wall)
            {
                Console.SetCursorPosition(col * 4, (row * 4));
                Console.Write("*****");
            }
            if (this.myMaze[row, col].S_wall)
            {
                Console.SetCursorPosition(col * 4, ((row + 1) * 4));
                Console.Write("*****");
            }
            if (this.myMaze[row, col].W_wall)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(col * 4, ((row * 4) + i));
                    Console.Write("*");
                }
            }
            if (this.myMaze[row, col].E_wall)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition((col + 1) * 4, ((row * 4) + i));
                    Console.Write("*");
                }
            }
            if (this.myMaze[row, col].Exit)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition((col * 4) + 2, (row * 4) + 2);
                Console.Write("▬");
            }
            if(this.myMaze[row,col].Player != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((col * 4) + 2, (row * 4) + 2);
                Console.Write("&");
            }
            if (this.myMaze[row, col].Opponent != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition((col * 4) + 2, (row * 4) + 2);
                Console.Write(this.myMaze[row, col].Opponent.Damage);
            }
        }

        public void PlaceOpponents(List<Opponent> weapons)
        {
            Random random = new Random();
            while (weapons.Count > 0)
            {
                int row = random.Next(this.height);
                int col = random.Next(this.width);
                if (this.myMaze[row, col].Opponent == null && !this.myMaze[row, col].Exit && !this.myMaze[row, col].Fighter)
                {
                    myMaze[row, col].Opponent = weapons[0];
                    weapons.Remove(weapons[0]);
                    printCell(row, col);
                }
            }
        }
    }
}
