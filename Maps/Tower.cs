using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Maps
{
    class Tower: MapGenerator
    {
        /// <summary>
        /// Method that set the width and the height of the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public override void GenerateMap(int _height, int _width)
        {
            base.GenerateMap(_height, _width);

            fillMap();
            setHero();
            setExit();
        }

        /// <summary>
        /// Method that creates a round border with random GameObjects inside.
        /// </summary>
        protected override void fillMap()
        {
            if (map != null || width != (height - 1) * 2 || (width - 1) % 4 != 0)
            {
                while ((width - 1) % 4 != 0)
                {
                    width++;
                }

                height = (width-1) / 2 + 1;
                base.GenerateMap(height, width);
            }

            double radius = (width - 1) / 4;
            double radiusIn = radius - 0.4;
            double radiusOut = radius + 0.4;
            int iPosition = 0;
            int jPosition = 0;
            Random randomObject = new Random(Seed);

            for (double i = radius; i >= -radius; --i)
            {
                for (double j = -radius; j < radiusOut; j += 0.5)
                {
                    double value = j * j + i * i;
                    if (value >= radiusIn * radiusIn - 2 && value <= radiusOut * radiusOut + 2)
                    {
                        map[iPosition, jPosition] = GameObject.Wall;
                    }
                    else if (value < radiusIn * radiusIn && value < radiusOut * radiusOut)
                    {
                        map[iPosition, jPosition] = (randomObject.Next(0, 100) < RandomFillPercent) ?
                                            GameObject.Wall : GameObject.EmptySpace;
                    }
                    else
                    {
                        map[iPosition, jPosition] = GameObject.EmptySpace;
                    }

                    jPosition++;
                    if (jPosition == radius * 4 + 1)
                    {
                        jPosition = 0;
                        iPosition++;
                    }
                }
            }
        }

        /// <summary>
        /// Method that creates an exit.
        /// </summary>
        protected override void setExit()
        {
            setDoor(height / 2, width / 4 * 3, false);
        }
    }
}
