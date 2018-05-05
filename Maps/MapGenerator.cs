using System;
using Game.Objects;

namespace Game.Maps
{
    abstract class MapGenerator
    {
        protected uint height;
        protected uint width;

        /// <summary>
        /// The percent of non allowed to move blocks on the map.
        /// </summary>
        public uint RandomFillPercent;

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
                if (value <= 70)
                {
                    this.seed = value;
                }
                else
                {
                    this.seed = 70;
                }
            }
        }

        protected bool useRandomSeed = true;

        protected uint heroIPosition;
        protected uint heroJPosition;

        public Tuple<uint, uint> HeroPosition
        {
            get
            {
                return new Tuple<uint, uint>(heroIPosition, heroJPosition);
            }
        }

        /// <summary>
        /// Method that set the width and the height of the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public virtual void GenerateMap(uint _height, uint _width)
        {
            this.height = (_height < 10) ? 10 : _height;

            this.width = (_width < 10) ? 10 : _width;
            

            map = new GameObject[height, width];
        }

        /// <summary>
        /// Method that fills the map with GameObjects.
        /// </summary>
        protected abstract void FillMap();

        /// <summary>
        /// Method that sets the hero position.
        /// </summary>
        protected virtual void SetHero()
        {
            heroIPosition = height/2;
            heroJPosition = width/2;

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
        protected virtual void SetExit()
        {
            var randomSide = new Random();
            var randomPosition = new Random();

            uint exitIPosition;
            uint exitJPosition;

            var side=randomSide.Next(0, 4);

            switch (side)
            {
                case 0:
                    exitIPosition = 0;
                    exitJPosition = (uint)randomPosition.Next(3, (int)width - 4);

                    while (map[exitIPosition + 1, exitJPosition] != GameObject.EmptySpace)
                    {
                        exitIPosition++;
                    }

                    SetDoor(exitIPosition, exitJPosition, true);
                    break;
                case 1:
                    exitIPosition = (uint)randomPosition.Next(3, (int)height - 4);
                    exitJPosition = width - 1;

                    while (map[exitIPosition, exitJPosition - 1] != GameObject.EmptySpace)
                    {
                        exitJPosition--;
                    }

                    SetDoor(exitIPosition, exitJPosition, false);
                    break;
                case 2:
                    exitIPosition = height - 1;
                    exitJPosition = (uint)randomPosition.Next(1, (int)width - 2);

                    while (map[exitIPosition - 1, exitJPosition] != GameObject.EmptySpace)
                    {
                        exitIPosition--;
                    }

                    SetDoor(exitIPosition, exitJPosition, true);
                    break;
                default:
                    exitIPosition = (uint)randomPosition.Next(1, (int)height - 2);
                    exitJPosition = 0;

                    while (map[exitIPosition, exitJPosition + 1] != GameObject.EmptySpace)
                    {
                        exitJPosition++;
                    }

                    SetDoor(exitIPosition, exitJPosition, false);
                    break;
            }
        }

        /// <summary>
        /// Method that set the door GameObjects on the map.
        /// </summary>
        /// <param name="_iPosition"></param>
        /// <param name="_jPosition"></param>
        /// <param name="_IsLine"></param>
        protected void SetDoor(uint _iPosition, uint _jPosition, bool _IsLine)
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
        /// Method that changes the hero position
        /// </summary>
        /// <param name="_heroIPosition"></param>
        /// <param name="_heroJPosition"></param>
        public virtual int MoveHero(uint _heroIPosition, uint _heroJPosition)
        {
            if (_heroIPosition < height && _heroJPosition < width )
            {
                if (map[_heroIPosition, _heroJPosition] == GameObject.Wall)
                {
                    return -1;
                }

                if (map[_heroIPosition, _heroJPosition] == GameObject.Exit)
                {
                    return 1;
                }

                map[heroIPosition, heroJPosition] = GameObject.EmptySpace;

                this.heroIPosition = _heroIPosition;
                this.heroJPosition = _heroJPosition;

                map[heroIPosition, heroJPosition] = GameObject.Hero;
                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Method that shows the map
        /// </summary>
        public void PrintMap()
        {
            if (map != null)
            {
                var visibleArea = 10;
                for (var i = heroIPosition - visibleArea; i <= heroIPosition + visibleArea; i++)
                {
                    for (var j = heroJPosition - visibleArea; j <= heroJPosition + visibleArea; j++)
                    {
                        if (i < 0 || i >= height || j < 0 || j >= width)
                        {
                            Console.Write((char) GameObject.EmptySpace);
                        }
                        else
                        {
                            Console.Write((char)map[i, j]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
