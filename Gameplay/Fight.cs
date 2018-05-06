using System;
using System.Threading;
using System.Timers;
using Game.Units;
using Game.Objects;

namespace Game.Gameplay
{
    sealed class Fight
    {
        private const uint battlefieldHeight = 7;
        private const uint battlefieldWidth = 12;

        private uint heroIPosition = 3;
        private uint heroJPosition = 3;

        private uint unitIPosition = 3;
        private uint unitJPosition = 8;

        private readonly GameObject[,] battlefield = new GameObject[battlefieldHeight, battlefieldWidth];

        public static int StartFight(ref Hero _hero,ref Unit _unit)
        {
            var fight = new Fight(_hero, _unit);

            if (_hero == null)
            {
                return -1;
            }

            if (_unit == null)
            {
                return 1;
            }

            var keyInfo = new ConsoleKey();

            while (_hero.HealthPoints > 0 && _unit.HealthPoints > 0 && keyInfo != ConsoleKey.Escape
            ) // || exit condition)
            {
                _hero.Energy = 2;
                _unit.Energy = 2;

                while (_hero.Energy > 0 && _hero.HealthPoints >= 0 && _unit.HealthPoints >= 0)
                {
                    Console.Clear();
                    Console.Write(fight.FightMap(_hero, _unit));

                    keyInfo = Console.ReadKey(true).Key;
                    fight.WhatToDO(_hero, _unit, fight.heroIPosition, fight.heroJPosition, keyInfo);
                }

                while (_unit.Energy > 0 && _hero.HealthPoints >= 0 && _unit.HealthPoints >= 0)
                {
                    if (_unit is ComputerUnit)
                    {
                        Console.Clear();
                        Console.Write(fight.FightMap(_hero, _unit));

                        ComputerUnit computerUnit = _unit as ComputerUnit;

                        //TODO: add timer
                        Console.ReadKey();
                        keyInfo = computerUnit.MakeMove(fight.battlefield, fight.unitIPosition, fight.unitJPosition,
                            fight.heroIPosition, fight.heroJPosition);
                        fight.WhatToDO(computerUnit, _hero, fight.unitIPosition, fight.unitJPosition, keyInfo);


                    }
                }
            }

            if (_hero.HealthPoints <= 0)
            {
                return -1;
            }
            else if (_unit.HealthPoints <= 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void WhatToDO(Unit _currentUnit, Unit _secondUnit, uint _iPosition, uint _jPosition, ConsoleKey _keyInfo)
        {
            switch (_keyInfo)
            {
                case ConsoleKey.UpArrow:
                    if (Move(_currentUnit, _iPosition - 1, _jPosition))
                    {
                        _currentUnit.Energy--;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (Move(_currentUnit, _iPosition + 1, _jPosition))
                    {
                        _currentUnit.Energy--;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (Move(_currentUnit, _iPosition, _jPosition + 1))
                    {
                        _currentUnit.Energy--;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (Move(_currentUnit, _iPosition, _jPosition - 1))
                    {
                        _currentUnit.Energy--;
                    }

                    break;
                case ConsoleKey.A:
                    if (Attack(_currentUnit, _secondUnit, _iPosition, _jPosition))
                    {
                        _secondUnit.HealthPoints -= _currentUnit.Damage;
                        _currentUnit.Energy--;
                        Console.Beep();
                    }

                    break;
                case ConsoleKey.R:
                    if (RangeAttack(_currentUnit, _secondUnit, _iPosition, _jPosition) && _currentUnit is IRanger)
                    {
                        IRanger ranger = _currentUnit as IRanger;
                        _secondUnit.HealthPoints -= ranger.RangeDamage;
                        _currentUnit.Energy--;
                        Console.Beep();
                    }

                    break;
                default:
                    break;
            }
        }

        private bool Move(Unit _unit, uint _iPosition, uint _jPosition)
        {
            if (_iPosition >= battlefieldHeight || _jPosition >= battlefieldWidth ||
                battlefield[_iPosition, _jPosition] != GameObject.EmptySpace) 
            {
                return false;
            }

            if (_unit is Hero)
            {
                var unit = _unit as Hero;

                battlefield[heroIPosition, heroJPosition] = GameObject.EmptySpace;
                heroIPosition = _iPosition;
                heroJPosition = _jPosition;
                battlefield[heroIPosition, heroJPosition] = unit.UnitIcon;

            }
            else 
            {
                battlefield[unitIPosition, unitJPosition] = GameObject.EmptySpace;
                unitIPosition = _iPosition;
                unitJPosition = _jPosition;
                battlefield[unitIPosition, unitJPosition] = _unit.UnitIcon;
            }

            return true;
        }


        private string FightMap(Hero _hero, Unit _unit)
        {
            string map = "";

            for (var i = 0; i < battlefieldHeight; i++)
            {
                for (var j = 0; j < battlefieldWidth; j++)
                {
                    map += (char) battlefield[i, j];
                }

                map += "\n";
            }

            map += $"Hero:{_hero.HealthPoints} - Enemy: {_unit.HealthPoints}\n";
            map += $"   -{_hero.Energy}-       -{_unit.Energy}-\n";

            return map;
        }

        private bool Attack(Unit _unit, Unit _attackedUnit, uint _iPosition, uint _jPosition)
        {
            if (battlefield[_iPosition + 1, _jPosition] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition - 1, _jPosition] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition, _jPosition + 1] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition, _jPosition - 1] == _attackedUnit.UnitIcon)
            {
                return true;
            }
            return false;
        }

        private bool RangeAttack(Unit _unit, Unit _attackedUnit, uint _iPosition, uint _jPosition)
        {
            if (!(_unit is IRanger))
            {
                return false;
            }

            var unit = _unit as IRanger;
            var leftIPosition = ((_iPosition - unit.ShootingRange) < 0) ? 0 : _iPosition - unit.ShootingRange;
            var rightIPosition = ((_iPosition + unit.ShootingRange) >= battlefieldHeight)
                ? battlefieldHeight
                : _iPosition + unit.ShootingRange;
            var leftJPosition = ((_jPosition - unit.ShootingRange) < 0) ? 0 : _jPosition - unit.ShootingRange;
            var rightJPosition = ((_jPosition + unit.ShootingRange) >= battlefieldWidth)
                ? battlefieldWidth
                : _jPosition + unit.ShootingRange;

            for (var i = leftIPosition; i <= rightIPosition; i++)
            {
                for (var j = leftJPosition; j <= rightJPosition; j++)
                {
                    if (battlefield[i, j] == _attackedUnit.UnitIcon)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        private bool MagickAttack(Unit _unit, Unit _attackedUnit, uint _unitIPosition, uint _unitJPosition,
            uint _attackedUnitIPosition, uint _attackedUnitJPosistion)
        {
            if (_unit is IMage)
            {
                var unit = _unit as IMage;

                return unit.MagickAttack(ref _attackedUnit, ref _unitIPosition, ref _unitJPosition,
                    ref _attackedUnitIPosition, ref _attackedUnitJPosistion);
            }

            return false;
        }

        private Fight(Hero _hero, Unit _unit)
        {
            var random = new Random(); 

            for (int i = 0; i < battlefieldHeight; i++)
            {
                for (int j = 0; j < battlefieldWidth; j++)
                {
                    if (i == 0 || j == 0 || i == battlefieldHeight - 1 || j == battlefieldWidth - 1)
                    {
                        battlefield[i, j] = GameObject.Wall;
                    }
                    else
                    {
                        if (random.Next(0, 11) == 0)
                        {
                            battlefield[i, j] = GameObject.Wall;
                        }
                        else
                        {
                            battlefield[i, j] = GameObject.EmptySpace;
                        }
                    }
                }
            }

            battlefield[heroIPosition, heroJPosition] = _hero.UnitIcon;
            battlefield[unitIPosition, unitJPosition] = _unit.UnitIcon;
        }
    }
}
