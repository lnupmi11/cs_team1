using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    /// <summary>
    /// Map that generate room next to the previous one.
    /// </summary>
    class Dungeon: MapGenerator
    {
        /// <summary>
        /// Constant size of the room
        /// </summary>
        private const int roomSize = 5;

        /// <summary>
        /// Exits coordinats on the different levels
        /// </summary>
        private List<Tuple<int, int>> levelBroders = new List<Tuple<int, int>>();

        private int maxHeight = 5;
        private int maxWidth = 5;

        /// <summary>
        /// Method that set the max width and the height of the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public override void GenerateMap(int _height, int _width)
        {
            maxHeight += _height;
            maxWidth += _width;

            while (maxHeight % 5 != 0) 
            {
                maxHeight++;
            }
            while (maxWidth % 5 != 0) 
            {
                maxWidth++;
            }

            this.height = roomSize;
            this.width = roomSize;

            map = new GameObject[maxHeight, maxWidth];
            levelBroders.Add(new Tuple<int, int>(0, roomSize - 1));

            fillMap();
            setHero();

        }

        /// <summary>
        /// Method that add the starting room.
        /// </summary>
        protected override void fillMap()
        {
            setRoom(0, 0);
        }

        /// <summary>
        /// Method that sets the hero position.
        /// </summary>
        protected override void setHero()
        {
            this.heroIPosition = roomSize / 2;
            this.heroJPosition = roomSize / 2;

            map[heroIPosition, heroJPosition] = GameObject.Hero;
        }

        public override void MoveHero(int _heroIPosition, int _heroJPosition)
        {
            base.MoveHero(_heroIPosition, _heroJPosition);
            addRoom(heroIPosition, heroJPosition);
        }

        /// <summary>
        /// Method that creates a room on the current coordinats.
        /// </summary>
        /// <param name="_iStartPosition"></param>
        /// <param name="_jStartPosition"></param>
        private void addRoom(int _iStartPosition, int _jStartPosition)
        {
            if (heroJPosition >= levelBroders[heroIPosition / 5].Item2)
            {
                setRoom(heroIPosition - roomSize / 2, heroJPosition + 1);
                levelBroders[heroIPosition / 5] = new Tuple<int, int>(levelBroders[heroIPosition / 5].Item1, heroJPosition + roomSize);
                map[heroIPosition, heroJPosition + 1] = GameObject.EmptySpace;
                this.width += roomSize;
            }
            else if (heroJPosition <= levelBroders[heroIPosition / 5].Item1)
            {
                setRoom(heroIPosition - roomSize / 2, heroJPosition - 5);
                levelBroders[heroIPosition / 5] = new Tuple<int, int>(heroJPosition - roomSize, levelBroders[heroIPosition / 5].Item2);
                map[heroIPosition, heroJPosition - 1] = GameObject.EmptySpace;
            }
            else if (heroIPosition >= levelBroders.Count * 5 - 1)
            {
                setRoom(heroIPosition + 1, heroJPosition - roomSize / 2);
                levelBroders.Add(new Tuple<int, int>(heroJPosition - roomSize / 2, heroJPosition + roomSize / 2));
                map[heroIPosition + 1, heroJPosition] = GameObject.EmptySpace;
                this.height += roomSize;
            }
        }

        /// <summary>
        /// Method that set the fills the room.
        /// </summary>
        /// <param name="_iStartPosition"></param>
        /// <param name="_jStartPosition"></param>
        private void setRoom(int _iStartPosition, int _jStartPosition)
        {
            Random randomExit = new Random();

            for (int i = _iStartPosition; i < _iStartPosition + roomSize; i++)
            {
                for (int j = _jStartPosition; j < _jStartPosition + roomSize; j++)
                {
                    if (i == _iStartPosition || i == _iStartPosition + roomSize - 1 || j == _jStartPosition || j == _jStartPosition + roomSize - 1)
                    {
                        if (i == _iStartPosition + roomSize / 2 || j == _jStartPosition + roomSize / 2)
                        {
                            if (randomExit.Next(0, 101) > 30 && i != 0 && i != maxHeight - 1 && j != 0 && j != maxWidth - 1)
                            {
                                map[i, j] = GameObject.EmptySpace;
                            }
                            else
                            {
                                map[i, j] = GameObject.Wall;
                            }
                        }
                        else
                        {
                            map[i, j] = GameObject.Wall;
                        }
                    }
                    else if (i == _iStartPosition + roomSize / 2 && j == _jStartPosition + roomSize / 2)
                    {
                        // TODO: random generation
                        map[i, j] = GameObject.Exit;
                    }
                    else
                    {
                        map[i, j] = GameObject.EmptySpace;
                    }
                }
            }
        }

    }
}
