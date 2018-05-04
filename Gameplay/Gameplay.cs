using System;
using Game.Maps;
using Game.Units;

namespace Game.Gameplay
{
    class GameProcess
    {
        private MapGenerator gameMap;

        public void StartGame()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            //gameMap = new Maze();
            //gameMap.RandomFillPercent = 48;

            //gameMap.GenerateMap(20, 20);
            test();
        }

        public void ShowMap()
        {
            Console.SetWindowSize(21, 22);

            ConsoleKeyInfo pressedButton = new ConsoleKeyInfo();
            int moveResult = 0;

            while (pressedButton.Key != ConsoleKey.Escape)
            {
                Console.Clear();
                gameMap.PrintMap();

                pressedButton = Console.ReadKey();
                switch (pressedButton.Key)
                {
                    case ConsoleKey.UpArrow:
                        moveResult = gameMap.MoveHero(gameMap.HeroPosition.Item1 - 1, gameMap.HeroPosition.Item2);
                        break;
                    case ConsoleKey.DownArrow:
                        moveResult = gameMap.MoveHero(gameMap.HeroPosition.Item1 + 1, gameMap.HeroPosition.Item2);
                        break;
                    case ConsoleKey.RightArrow:
                        moveResult = gameMap.MoveHero(gameMap.HeroPosition.Item1, gameMap.HeroPosition.Item2 + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        moveResult = gameMap.MoveHero(gameMap.HeroPosition.Item1, gameMap.HeroPosition.Item2 - 1);
                        break;
                    default:
                        break;
                }

                if (moveResult == 1)
                {
                    gameMap=new Tower();
                    gameMap.GenerateMap(20, 20);
                }
            }

        }

        public void test()
        {
            Hero hero = new Hero();
            Unit unit = new Unit();

            Fight.StartFight(ref hero, ref unit);
        }
    }
}
