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
