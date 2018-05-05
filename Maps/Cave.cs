using System;
using Game.Objects;

namespace Game.Maps
{
    class Cave: MapGenerator
    {
        public Cave(uint _height, uint _width)
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
            FillMap();

            for (int i = 0; i < 3; i++)
            {
                smoothMap();
            }

            SetHero();
            SetExit();
        }

        /// <summary>
        /// Method that fills the map with GameObjects.
        /// </summary>
        protected override void FillMap()
        {
            var randomSeed = new Random(Seed);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        map[i, j] = GameObject.Wall;
                    }
                    else
                    {
                        map[i, j] = (randomSeed.Next(0, 100) < RandomFillPercent) ?
                            GameObject.Wall : GameObject.EmptySpace;
                    }
                }
            }
        }

        /// <summary>
        /// Method that smooth the wall lines.
        /// </summary>
        private void smoothMap()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int neightbourWalls = getSurroundingWallCount(i, j);

                    if (neightbourWalls > 4)
                    {
                        map[i, j] = GameObject.Wall;
                    }
                    else if (neightbourWalls < 4)
                    {
                        map[i, j] = GameObject.EmptySpace;
                    }
                }
            }
        }

        /// <summary>
        /// Supporting method that counts surrounding GameObject.Wall's.
        /// </summary>
        /// <param name="_iPosition"></param>
        /// <param name="_jPosition"></param>
        /// <returns></returns>
        private int getSurroundingWallCount(int _iPosition, int _jPosition)
        {
            var wallCount = -1;

            for (int neightbourI = _iPosition - 1; neightbourI <= _iPosition + 1; neightbourI++)
            {
                for (int neightbourJ = _jPosition - 1; neightbourJ <= _jPosition + 1; neightbourJ++)
                {
                    if (neightbourI >= 0 && neightbourI < height && neightbourJ >= 0 && neightbourJ < width)
                    {
                        if (map[neightbourI, neightbourJ] == GameObject.Wall)
                        {
                            wallCount++;
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }
    }
}
