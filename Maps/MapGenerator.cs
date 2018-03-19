using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Maps
{
    abstract class MapGenerator
    {
        protected int height;
        protected int width;

        public int RandomFillPercent;

        protected GameObject[,] map;

        protected int seed;
        public int Seed
        {
            get
            {
                if (useRandomSeed)
                {
                    seed = DateTime.Now.GetHashCode();
                    useRandomSeed = false;
                }
                return this.seed;
            }
            set
            {
                this.useRandomSeed = false;
                this.seed = value;
            }
        }

        protected bool useRandomSeed = true;

        /// <summary>
        /// Method that set the width and the height of the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public virtual void GenerateMap(int _height, int _width)
        {
            this.height = _height;
            this.width = _width;

            map = new GameObject[height, width];
        }

        /// <summary>
        /// Method that fills the map with GameObjects.
        /// </summary>
        protected abstract void fillMap();

        /// <summary>
        /// Method that sets the hero position.
        /// </summary>
        protected virtual void setHero()
        {
            int heroIPosition = height/2;
            int heroJPosition = width/2;

            while (map[heroIPosition, heroJPosition] != GameObject.EmptySpace)
            {
                heroIPosition--;
                heroJPosition--;
            }

            map[heroIPosition, heroJPosition] = GameObject.Hero;
        }

        /// <summary>
        /// Method that creates an exit on the map.
        /// </summary>
        protected virtual void setExit()
        {
            Random randomSide = new Random();
            Random randomPosition = new Random();
            int exitIPosition;
            int exitJPosition;

            int side=randomSide.Next(0, 4);

            switch (side)
            {
                case 0:
                    exitIPosition = 0;
                    exitJPosition = randomPosition.Next(3, width - 4);

                    while (map[exitIPosition + 1, exitJPosition] != GameObject.EmptySpace)
                    {
                        exitIPosition++;
                    }

                    setDoor(exitIPosition, exitJPosition, true);
                    break;
                case 1:
                    exitIPosition = randomPosition.Next(3, height - 4);
                    exitJPosition = width - 1;

                    while (map[exitIPosition, exitJPosition - 1] != GameObject.EmptySpace) 
                    {
                        exitJPosition--;
                    }

                    setDoor(exitIPosition, exitJPosition, false);
                    break;
                case 2:
                    exitIPosition = height-1;
                    exitJPosition = randomPosition.Next(1, width - 2);

                    while (map[exitIPosition - 1, exitJPosition] != GameObject.EmptySpace) 
                    {
                        exitIPosition--;
                    }

                    setDoor(exitIPosition, exitJPosition, true);
                    break;
                default:
                    exitIPosition = randomPosition.Next(1, height - 2); ;
                    exitJPosition = 0;

                    while (map[exitIPosition, exitJPosition + 1] != GameObject.EmptySpace) 
                    {
                        exitJPosition++;
                    }

                    setDoor(exitIPosition, exitJPosition, false);
                    break;
            }
        }

        /// <summary>
        /// Method that set the door GameObjects on the map.
        /// </summary>
        /// <param name="_iPosition"></param>
        /// <param name="_jPosition"></param>
        /// <param name="_IsLine"></param>
        protected void setDoor(int _iPosition, int _jPosition, bool _IsLine)
        {
            map[_iPosition, _jPosition] = GameObject.Exit;
            if (_IsLine)
            {
                map[_iPosition, _jPosition + 1] = GameObject.Exit;
                map[_iPosition, _jPosition - 1] = GameObject.Exit;
            }
            else
            {
                map[_iPosition + 1, _jPosition] = GameObject.Exit;
                map[_iPosition - 1, _jPosition] = GameObject.Exit;
            }
        }

        /// <summary>
        /// Method that shows the map
        /// </summary>
        public void PrintMap()
        {
            
            if (map != null)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write((char)map[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
