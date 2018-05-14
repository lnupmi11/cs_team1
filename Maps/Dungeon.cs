using System;
using Game.Objects;
using Game.Units;

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

        private bool[,] roomExist;


        private uint minHeight = 5;
        private uint minWidth = 5;

        public Dungeon(uint _height, uint _width)
        {
            GenerateMap(_height, _width);
        }

        /// <summary>
        /// Method that set the max width and the height of the map.
        /// </summary>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        public override void GenerateMap(uint _height, uint _width)
        {
            base.GenerateMap(_height, _width);

            minHeight += _height;
            minWidth += _width;

            while (minHeight % 5 != 0) 
            {
                minHeight++;
            }
            while (minWidth % 5 != 0) 
            {
                minWidth++;
            }

            this.height = minHeight;
            this.width = minWidth;

            map = new GameObject[height, width];
            roomExist = new bool[minHeight / roomSize, minWidth / roomSize];

            roomExist[0, 0] = true;

            FillMap();
            SetHero();
            
        }

        /// <summary>
        /// Method that add the starting room.
        /// </summary>
        protected override void FillMap()
        {
            SetRoom(0, 0);
            map[roomSize - 1, (roomSize / 2)] = GameObject.EmptySpace;
        }

        /// <summary>
        /// Method that sets the hero position.
        /// </summary>
        protected override void SetHero()
        {
            this.heroIPosition = roomSize / 2;
            this.heroJPosition = roomSize / 2;

            map[heroIPosition, heroJPosition] = GameObject.Hero;
        }

        /// <summary>
        /// Method that changes the hero position.
        /// </summary>
        /// <param name="_heroIPosition"></param>
        /// <param name="_heroJPosition"></param>
        /// <returns></returns>
        public override int MoveHero(ref Hero _hero, uint _heroIPosition, uint _heroJPosition)
        {
            var moveResult = base.MoveHero(ref _hero, _heroIPosition, _heroJPosition);

            if (moveResult == 0)
            {
                AddRoom(_heroIPosition, _heroJPosition);
            }

            return moveResult;
        }

        /// <summary>
        /// Method that creates a new room if the hero is on the border.
        /// </summary>
        /// <param name="_iPosition"></param>
        /// <param name="_jPosition"></param>
        private void AddRoom(uint _iPosition, uint _jPosition)
        {
            if (_iPosition % 5 == 0 && _iPosition != 0 &&
                     !roomExist[_iPosition / roomSize - 1, _jPosition / roomSize])
            {
                SetRoom(_iPosition - roomSize, _jPosition - (roomSize / 2));
                roomExist[_iPosition / roomSize - 1, _jPosition / roomSize] = true;
                map[_iPosition - 1, _jPosition] = GameObject.EmptySpace;
            }
            else if (_iPosition % 5 == 4 && _iPosition != height - 1 &&
                     !roomExist[_iPosition / roomSize + 1, _jPosition / roomSize])
            {
                SetRoom(_iPosition + 1, _jPosition - (roomSize / 2));
                roomExist[_iPosition / roomSize + 1, _jPosition / roomSize] = true;
                map[_iPosition + 1, _jPosition] = GameObject.EmptySpace;
            }
            else if (_jPosition % 5 == 0 && _jPosition != 0 &&
                     !roomExist[_iPosition / roomSize, _jPosition / roomSize - 1])
            {
                SetRoom(_iPosition - (roomSize/2), _jPosition - (roomSize));
                roomExist[_iPosition / roomSize, _jPosition / roomSize - 1] = true;
                map[_iPosition, _jPosition - 1] = GameObject.EmptySpace;
            }
            else if (_jPosition % 5 == 4 && _jPosition != width-1 &&
                     !roomExist[_iPosition / roomSize, _jPosition / roomSize + 1])
            {
                SetRoom(_iPosition - (roomSize / 2), _jPosition + 1);
                roomExist[_iPosition / roomSize, _jPosition / roomSize + 1] = true;
                map[_iPosition, _jPosition + 1] = GameObject.EmptySpace;
            }
        }

        /// <summary>
        /// Method that set the fills the room.
        /// </summary>
        /// <param name="_iStartPosition"></param>
        /// <param name="_jStartPosition"></param>
        private void SetRoom(uint _iStartPosition, uint _jStartPosition)
        {
            var randomExit = new Random();
            var chanceToSpawn = new Random();
            for (var i = _iStartPosition; i < _iStartPosition + roomSize; i++)
            {
                for (var j = _jStartPosition; j < _jStartPosition + roomSize; j++)
                {
                    if (i == _iStartPosition || i == _iStartPosition + roomSize - 1 || j == _jStartPosition || j == _jStartPosition + roomSize - 1)
                    {
                        if (i == _iStartPosition + roomSize / 2 || j == _jStartPosition + roomSize / 2)
                        {
                            if (randomExit.Next(0, 101) > 20 && i != 0 && i != minHeight - 1 && j != 0 && j != minWidth - 1)
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
                        map[i, j] = GameObject.Exit;
                    }
                    else
                    {
                        if (chanceToSpawn.Next(0, 21) <= 3)
                        {
                            map[i, j] = GameObject.Enemy;
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
}
