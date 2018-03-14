using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Map
{
    class Tower: DungeonGenerator
    { 
        protected override void fillMap()
        {
            if (map != null && width != height * 2 - 1)
            {
                while (width % 4 - 1 != 0)
                {
                    width++;
                }

                height = width / 2 + 1;
            }

            double radius = (width - 1) / 4;
            double r_in = radius - 0.4;
            double r_out = radius + 0.4;
            int iPosition = 0;
            int jPosition = 0;
            Random randomObject = new Random();

            for (double i = radius; i >= -radius; --i)
            {
                for (double j = -radius; j < r_out; j += 0.5)
                {
                    double value = j * j + i * i;
                    if (value >= r_in * r_in - 1 && value <= r_out * r_out)
                    {
                        SetGameObject(iPosition, jPosition, GameObject.Wall);
                    }
                    else if (value < r_in * r_in && value < r_out * r_out)
                    {
                        SetGameObject(iPosition, jPosition, (randomObject.Next(0, 100) < RandomFillPercent) ?
                                            GameObject.Wall : GameObject.EmptySpace);
                    }
                    else
                    {
                        SetGameObject(iPosition, jPosition, GameObject.EmptySpace);
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

        protected override void setExit()
        {
            setDoor(height / 2, width / 4 * 3, false);
        }

        /// <summary>
        /// 
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
    }
}
