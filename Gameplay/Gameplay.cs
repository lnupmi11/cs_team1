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
            gameMap = new Maze();
            gameMap.RandomFillPercent = 48;

            gameMap.GenerateMap(30, 60);
            
        }

        public void ShowMap()
        {
            Console.SetWindowSize(61, 32);
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            gameMap.PrintMap();

            Console.ReadKey();
        }
    }
}
