using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Gameplay;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            GameProcess game = new GameProcess();
            game.StartGame();
            game.ShowMap();

        }

    }
}
