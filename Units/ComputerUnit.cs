using System;
using Game.Objects;

namespace Game.Units
{
    class ComputerUnit: Unit
    {
        public ComputerUnit() : base()
        {

        }

        public ConsoleKey MakeMove(GameObject[,] _battlefield,
            uint _thisUnitIPosition, uint _thisUnitJPosition,
            uint _unitIPosition, uint _unitJPosition)
        {
            if (_battlefield[_thisUnitIPosition - 1, _thisUnitJPosition] == GameObject.Hero ||
                _battlefield[_thisUnitIPosition + 1, _thisUnitJPosition] == GameObject.Hero ||
                _battlefield[_thisUnitIPosition, _thisUnitJPosition - 1] == GameObject.Hero ||
                _battlefield[_thisUnitIPosition, _thisUnitJPosition + 1] == GameObject.Hero)
            {
                return ConsoleKey.A;
            }

            else
            {
                return WhereToMove(_battlefield,
                    _thisUnitIPosition, _thisUnitJPosition,
                    _unitIPosition, _unitJPosition);
            }
        }

        protected ConsoleKey WhereToMove(GameObject[,] _battlefield,
            uint _thisUnitIPosition, uint _thisUnitJPosition,
            uint _unitIPosition, uint _unitJPosition)
        {
            var random = new Random();
            int randomDestiantion = random.Next(0, 2);
            if (_thisUnitIPosition < _unitIPosition && randomDestiantion == 0)
            {
                if (_battlefield[_thisUnitIPosition + 1, _thisUnitJPosition] != GameObject.Wall)
                {
                    return ConsoleKey.DownArrow;
                }
                else
                {
                    random = new Random();
                    var whereToMove = random.Next(0, 2);

                    if (whereToMove == 0 && _battlefield[_thisUnitIPosition, _thisUnitJPosition - 1] != GameObject.Wall)
                    {
                        return ConsoleKey.LeftArrow;
                    }
                    else if (whereToMove == 1 &&
                             _battlefield[_thisUnitIPosition, _thisUnitJPosition + 1] != GameObject.Wall)
                    {
                        return ConsoleKey.RightArrow;
                    }
                    else
                    {
                        return ConsoleKey.UpArrow;
                    }
                }
            }

            else if (_thisUnitIPosition > _unitIPosition && randomDestiantion == 0)
            {
                if (_battlefield[_thisUnitIPosition - 1, _thisUnitJPosition] != GameObject.Wall)
                {
                    return ConsoleKey.UpArrow;
                }
                else
                {
                    random = new Random();
                    var whereToMove = random.Next(0, 2);

                    if (whereToMove == 0 && _battlefield[_thisUnitIPosition, _thisUnitJPosition - 1] != GameObject.Wall)
                    {
                        return ConsoleKey.LeftArrow;
                    }
                    else if (whereToMove == 1 &&
                             _battlefield[_thisUnitIPosition, _thisUnitJPosition + 1] != GameObject.Wall)
                    {
                        return ConsoleKey.RightArrow;
                    }
                    else
                    {
                        return ConsoleKey.DownArrow;
                    }
                }
            }

            else if (_thisUnitJPosition < _unitJPosition && randomDestiantion == 1)
            {
                if (_battlefield[_thisUnitIPosition, _thisUnitJPosition + 1] != GameObject.Wall)
                {
                    return ConsoleKey.RightArrow;
                }
                else
                {
                    var whereToMove = random.Next(0, 2);

                    if (whereToMove == 0 && _battlefield[_thisUnitIPosition - 1, _thisUnitJPosition] != GameObject.Wall)
                    {
                        return ConsoleKey.UpArrow;
                    }
                    else if (whereToMove == 1 &&
                             _battlefield[_thisUnitIPosition + 1, _thisUnitJPosition] != GameObject.Wall)
                    {
                        return ConsoleKey.DownArrow;
                    }
                    else
                    {
                        return ConsoleKey.LeftArrow;
                    }
                }
            }

            else if (_thisUnitJPosition > _unitJPosition && randomDestiantion == 1)
            {
                if (_battlefield[_thisUnitIPosition, _thisUnitJPosition - 1] != GameObject.Wall)
                {
                    return ConsoleKey.LeftArrow;
                }
                else
                {
                    var whereToMove = random.Next(0, 2);

                    if (whereToMove == 0 && _battlefield[_thisUnitIPosition - 1, _thisUnitJPosition] != GameObject.Wall)
                    {
                        return ConsoleKey.UpArrow;
                    }
                    else if (whereToMove == 1 &&
                             _battlefield[_thisUnitIPosition + 1, _thisUnitJPosition] != GameObject.Wall)
                    {
                        return ConsoleKey.DownArrow;
                    }
                    else
                    {
                        return ConsoleKey.RightArrow;
                    }
                }
            }

            return ConsoleKey.Escape;
        }
    }
}
