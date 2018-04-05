using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Labyrinth
{
    private int width, height;
    private double complexity = 0.75, density = 0.75;

    public int Width { get { return width; } set { width = value; } }
    public int Height { get { return height; } set { height = value; } }

    public Labyrinth()
    {
        width = 11;
        height = 11;
    }

    public Labyrinth(int _width,int _height)
    {
        width = _width;
        height = _height;
    }

    public Labyrinth(Labyrinth previousLabyrinth)
    {
        width = previousLabyrinth.width;
        height = previousLabyrinth.height;
    }

    public List<List<bool>> Maze()
    {
        int[] shape = new int[2];
        shape[0] = (height / 2) * 2 + 1;
        shape[1] = (width / 2) * 2 + 1;
        complexity = Convert.ToInt64(complexity * (5 * (shape[0] + shape[1])));
        density = Convert.ToInt64(density * ((shape[0] / 2) * (shape[1] / 2)));

        List<List<bool>> Z = new List<List<bool>>();
        for(int i = 0; i < shape[0]; i++)
        {
            List<bool> tmp = new List<bool>();
            for(int j = 0; j < shape[1]; j++)
            {
                tmp.Add(false);
            }
            Z.Add(tmp);
        }

        for (int i = 0; i < Z[0].Count; i++)
        {
            Z[0][i] = true;
            Z[i][0] = true;
        }
        for (int i = 0; i < Z[Z.Count - 1].Count; i++)
        {
            Z[Z.Count - 1][i] = true;
            Z[i][Z.Count - 1] = true;
        }
        //foreach (var it in Z)
        //{
        //    foreach (var it2 in it)
        //    {
        //        Console.Write(it2);
        //    }
        //    Console.Write('\n');
        //}
        //Console.WriteLine("Hi");
        for (int i = 0; i < density; i++)
        {
            Random rand = new Random();
            int x = rand.Next(0, shape[1] / 2) * 2;
            int y = rand.Next(0, shape[0] / 2) * 2;
            Z[y][x] = true;
            for(int j = 0; j < complexity; j++)
            {
                List<Tuple<int,int>> neighbours = new List<Tuple<int, int>>();
                if(x > 1)
                {
                    neighbours.Add(Tuple.Create(y, x - 2));
                }
                if (x < shape[1] - 2)
                {
                    neighbours.Add(Tuple.Create(y, x + 2));
                }
                if (y > 1)
                {
                    neighbours.Add(Tuple.Create(y - 2, x));
                }
                if (y < shape[0] - 2)
                {
                    neighbours.Add(Tuple.Create(y + 2, x));
                }
                if(neighbours.Count > 0)
                {
                    int index = rand.Next(0, neighbours.Count - 1);
                    int _y = neighbours[index].Item1;
                    int _x = neighbours[index].Item2;
                    if(Z[_y][_x] == false)
                    {
                        Z[_y][_x] = true;
                        Z[_y + (y - _y) / 2][_x + (x - _x) / 2] = true;
                        x = _x;
                        y = _y;
                    }
                }
            }
        }
        return Z;
    }
}

