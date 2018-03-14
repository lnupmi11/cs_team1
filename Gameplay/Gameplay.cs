using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Map;

namespace Game.Gameplay
{
    class GameProcess
    {
        private Cave gameMap;

        public void StartGame()
        { 
            gameMap = new Cave();
            gameMap.RandomFillPercent = 45;

            gameMap.GenerateMap(30, 60);
        }

        public void ShowMap()
        {
            Console.SetWindowSize(60, 31);
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            gameMap.PrintMap();

            Console.ReadKey();
        }
    }
}
