using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Maps;

namespace Game.Gameplay
{
    class GameProcess
    {
        private MapGenerator gameMap;

        public void StartGame()
        { 
            gameMap = new Dungeon();
            gameMap.RandomFillPercent = 48;

            gameMap.GenerateMap(30, 60);
            
        }

        public void ShowMap()
        {
            Console.SetWindowSize(61, 32);
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            gameMap.PrintMap();
            gameMap.MoveHero(2, 4);
            Console.ReadKey();

            Console.Clear();
            gameMap.PrintMap();
            gameMap.MoveHero(4, 7);
            Console.ReadKey();

            Console.Clear();
            gameMap.PrintMap();
            gameMap.MoveHero(7, 5);
            Console.ReadKey();

            Console.Clear();
            gameMap.PrintMap();
            Console.ReadKey();

        }
    }
}
