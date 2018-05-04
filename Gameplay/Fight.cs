using System;
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

        private GameObject[,] battlefield = new GameObject[battlefieldHeight, battlefieldWidth];

        public static int StartFight( ref Hero _hero, ref Unit _unit)
        {
            Fight fight = new Fight();

            if (_hero == null)
            {
                return -1;
            }

            if (_unit==null)
            {
                return 1;
            }

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            while(_hero.HP > 0 || _unit.HP > 0 || keyInfo.Key != ConsoleKey.Escape) // || exit condition)
            {
                Console.Clear();
                Console.Write(fight.FightMap);

                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        fight.Move(_hero, fight.heroIPosition - 1, fight.heroJPosition);
                        break;
                    case ConsoleKey.DownArrow:
                        fight.Move(_hero, fight.heroIPosition + 1, fight.heroJPosition);
                        break;
                    case ConsoleKey.RightArrow:
                        fight.Move(_hero, fight.heroIPosition, fight.heroJPosition + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        fight.Move(_hero, fight.heroIPosition, fight.heroJPosition - 1);
                        break;
                    case ConsoleKey.A:
                        if (fight.Attack(_hero, _unit, fight.heroIPosition, fight.heroJPosition))
                        {
                            Console.Beep();
                        }
                        break;
                    default:
                        break;
                }
            }

            return 0;
        }

        private void Move(Unit _unit, uint _iPosition, uint _jPosition)
        {
            if (_iPosition >= battlefieldHeight || _jPosition >= battlefieldWidth ||
                battlefield[_iPosition, _jPosition] != GameObject.EmptySpace) 
            {
                return;
            }

            if (_unit is Hero)
            {
                Hero unit = _unit as Hero;

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
        }
        

        private string FightMap
        {
            get
            {
                string map = "";

                for (var i = 0; i < battlefieldHeight; i++)
                {
                    for (var j = 0; j < battlefieldWidth; j++)
                    {
                        map += (char)battlefield[i, j];
                    }
                    map += "\n";
                }

                return map;
            }
        }

        private bool Attack(Unit _unit, Unit _attackedUnit, uint _iPosition, uint _jPosition)
        {
            if (battlefield[_iPosition + 1, _jPosition] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition - 1, _jPosition] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition, _jPosition + 1] == _attackedUnit.UnitIcon ||
                battlefield[_iPosition, _jPosition - 1] == _attackedUnit.UnitIcon)
            {
                _attackedUnit.HP -= _unit.Damage;
                return true;
            }
            return false;
        }

        private Fight()
        {
            Random random = new Random(); 
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

            battlefield[heroIPosition, heroJPosition] = GameObject.Hero;
            battlefield[unitIPosition, unitJPosition] = GameObject.Exit;
        }
    }
}
