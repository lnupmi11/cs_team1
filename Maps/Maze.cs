using System;
using System.Collections.Generic;
using System.Linq;
using Game.Objects;

namespace Game.Maps
{
    /// <summary>
    /// Work in progress.
    /// </summary>
    class Maze : MapGenerator
    {
        public Maze(uint _height, uint _width)
        {
            GenerateMap(_height, _width);
        }

        /// <summary>
        /// Method that generates the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public override void GenerateMap(uint _height, uint _width)
        {
            base.GenerateMap(_height, _width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = GameObject.Wall;
                }
            }

            FillMap();
        }

        /// <summary>
        /// Method that fills the map with maze.
        /// </summary>
        protected override void FillMap()
        {
            var mazeStartingIPosition = 1;
            var mazeStartingJPosition = 1;
            var generateNewMap = false;

            if (height % 2 != 1)
            {
                height++;
                generateNewMap = true;
            }
            if (width % 2 != 1)
            {
                width++;
                generateNewMap = true;
            }
            if (generateNewMap)
            {
                GenerateMap(height, width);
            }

            var randomSide = new Random(Seed);
            AddWay(mazeStartingIPosition, mazeStartingJPosition, randomSide);

            SetHero();
            SetExit();
        }

        /// <summary>
        /// Method that sets the hero object on the beginning of the maze.
        /// </summary>
        protected override void SetHero()
        {
            this.heroIPosition = 1;
            this.heroJPosition = 1;

            map[heroIPosition, heroJPosition] = GameObject.Hero;
        }

        /// <summary>
        /// Method that sets the exit object.
        /// </summary>
        protected override void SetExit()
        {
            var randomSide = new Random();
            var randomPosition = new Random();
            uint exitIPosition;
            uint exitJPosition;

            var side = randomSide.Next(0, 4);

            switch (side)
            {
                case 0:
                    exitIPosition = 0;
                    exitJPosition = (uint)randomPosition.Next(3, (int)width - 4);

                    if (exitJPosition % 2 != 1)
                    {
                        exitJPosition++;
                    }
                    break;
                case 1:
                    exitIPosition = (uint)randomPosition.Next(3, (int)height - 4);
                    exitJPosition = width - 1;

                    if (exitIPosition % 2 != 1)
                    {
                        exitIPosition++;
                    }
                    break;
                case 2:
                    exitIPosition = height - 1;
                    exitJPosition = (uint)randomPosition.Next(1, (int)width - 2);

                    if (exitJPosition % 2 != 1)
                    {
                        exitJPosition++;
                    }
                    break;
                default:
                    exitIPosition = (uint)randomPosition.Next(1, (int)height - 2); ;
                    exitJPosition = 0;

                    if(exitIPosition%2 != 1)
                    {
                        exitIPosition++;
                    }
                    break;
            }

            map[exitIPosition, exitJPosition] = GameObject.Exit;
        }

        /// <summary>
        /// Method that builds the maze on the map.
        /// </summary>
        /// <param name="_i"></param>
        /// <param name="_j"></param>
        /// <param name="_randomSide"></param>
        private void AddWay(int _i, int _j, Random _randomSide)
        {
            var exits = new bool[4];

            while(CheckExitsExistance(exits))
            {
                int choosedSide = _randomSide.Next(0, 4);
                if (exits[choosedSide])
                {
                    choosedSide = (choosedSide + 1) % 3;
                }
                switch (choosedSide)
                {
                    case 0:
                        if (_i - 2 > 0  && map[_i - 2, _j] != GameObject.EmptySpace)
                        {
                            SetRoute(_i, _j, -1, 0);
                            AddWay(_i - 2, _j, _randomSide);
                        }
                        break;
                    case 1:
                        if (_i + 2 < height - 1 && map[_i + 2, _j] != GameObject.EmptySpace)
                        {
                            SetRoute(_i, _j, 1, 0);
                            AddWay(_i + 2, _j, _randomSide);
                        }
                        break;
                    case 2:
                        if (_j - 2 > 0 && map[_i, _j - 2] != GameObject.EmptySpace) 
                        {
                            SetRoute(_i, _j, 0, -1);
                            AddWay(_i, _j - 2, _randomSide);
                        }
                        break;
                    case 3:
                        if (_j + 2 < width - 1 && map[_i, _j + 2] != GameObject.EmptySpace)
                        {
                            SetRoute(_i, _j, 0, 1);
                            AddWay(_i, _j + 2, _randomSide);
                        }
                        break;
                    default:
                        break;
                }
                exits[choosedSide] = true;
            }
        }

        /// <summary>
        /// Method that sets the route to the next node.
        /// </summary>
        /// <param name="_iPosition"></param>
        /// <param name="_jPosition"></param>
        /// <param name="_iAddition"></param>
        /// <param name="_jAddition"></param>
        private void SetRoute(int _iPosition, int _jPosition, int _iAddition, int _jAddition)
        {
            map[_iPosition + _iAddition, _jPosition + _jAddition] = GameObject.EmptySpace;
            map[_iPosition + _iAddition + _iAddition, _jPosition + _jAddition + _jAddition] = GameObject.EmptySpace;
        }

        /// <summary>
        /// Method that checks if the exit is allready existing.
        /// </summary>
        /// <param name="_exits"></param>
        /// <returns></returns>
        private bool CheckExitsExistance(bool[] _exits)
        {
            var numberOfSides = 0;
            foreach (var exit in _exits)
            {
                if(exit== true)
                {
                    numberOfSides++;
                }
            }

            return numberOfSides != 4;
        }
    }
}
