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
            if (_thisUnitIPosition < _unitIPosition)
            {
                if (_battlefield[_thisUnitIPosition + 1, _thisUnitJPosition] != GameObject.Wall)
                {
                    return ConsoleKey.DownArrow;
                }
                else
                {
                    var random = new Random();
                    return random.Next(0, 2) == 0 ? ConsoleKey.RightArrow : ConsoleKey.LeftArrow;
                }
            }

            else if (_thisUnitIPosition > _unitIPosition)
            {
                if (_battlefield[_thisUnitIPosition - 1, _thisUnitJPosition] != GameObject.Wall)
                {
                    return ConsoleKey.UpArrow;
                }
                else
                {
                    var random = new Random();
                    return random.Next(0, 2) == 0 ? ConsoleKey.RightArrow : ConsoleKey.LeftArrow;
                }
            }

            else if (_thisUnitJPosition < _unitJPosition)
            {
                if (_battlefield[_thisUnitIPosition, _thisUnitJPosition + 1] != GameObject.Wall)
                {
                    return ConsoleKey.RightArrow;
                }
                else
                {
                    var random = new Random();
                    return random.Next(0, 2) == 0 ? ConsoleKey.UpArrow : ConsoleKey.DownArrow;
                }
            }

            else if (_thisUnitJPosition > _unitJPosition)
            {
                if (_battlefield[_thisUnitIPosition, _thisUnitJPosition] != GameObject.Wall)
                {
                    return ConsoleKey.LeftArrow;
                }
                else
                {
                    var random = new Random();
                    return random.Next(0, 2) == 0 ? ConsoleKey.RightArrow : ConsoleKey.LeftArrow;
                }
            }

            return ConsoleKey.Escape;
        }
    }
}
