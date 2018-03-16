using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Maps
{
    class Cave: MapGenerator
    {
        protected int seed;
        public int Seed
        {
            get
            {
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
        /// Method that generates the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public override void GenerateMap(int _height, int _width)
        {
            this.height = _height;
            this.width = _width;

            map = new GameObject[height, width];
            fillMap();

            for (int i = 0; i < 5; i++)
            {
                smoothMap();
            }

            setHero();
            setExit();
        }

        /// <summary>
        /// Method that fills the map with GameObjects.
        /// </summary>
        protected override void fillMap()
        {
            if (useRandomSeed)
            {
                seed = DateTime.Now.GetHashCode();
            }

            Random randomSeed = new Random(seed.GetHashCode());

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        SetGameObject(i, j, GameObject.Wall);
                    }
                    else
                    {
                        SetGameObject(i, j, (randomSeed.Next(0, 100) < RandomFillPercent) ?
                            GameObject.Wall : GameObject.EmptySpace);
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
                        SetGameObject(i, j, GameObject.Wall);
                    }
                    else if (neightbourWalls < 4)
                    {
                        SetGameObject(i, j, GameObject.EmptySpace);
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
            int wallCount = -1;

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

        protected override void setExit()
        {
            base.setExit();
        }
    }
}
