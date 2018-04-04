using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Maps
{
    /// <summary>
    /// Work in progress.
    /// TODO: to generate room next to the previous.
    /// </summary>
    class Dungeon: MapGenerator
    {
        private struct Room
        {
            public int NorthExit;
            public int SouthExit;
            public int WestExit;
            public int EastExit;
        }
        private int roomsHeight = 1;
        private int roomsWidth = 1;

        private Room[,] dungeonRooms;

        public void GenerateMap()
        {

        }

        protected override void fillMap()
        {
            
        }

        
    }
}
