using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LabyFights
{
    class Leaderboard
    {
        public static void AddScore(string name, int score)
        {
            string fileName = "Leaderboard.txt";

            List<Tuple<string, int>> scores = new List<Tuple<string, int>>();
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var readedLine = line.Split(' ');
                        scores.Add(new Tuple<string, int>(readedLine[0], Int32.Parse(readedLine[1])));
                    }
                }
            }

            var i = 0;
            if (scores.Count != 0)
            {
                while (i < scores.Count && score < scores[i].Item2)
                {
                    i++;
                }
            }

            scores.Insert(i,new Tuple<string, int>(name,score));

            if (i < 10)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    for (i = 0; i < 10; i++)
                    {
                        if(i==scores.Count)
                        {
                            break;
                        }
                        else
                        {
                            writer.WriteLine($"{scores[i].Item1} {scores[i].Item2}");
                        }
                    }
                }
            }
        }

        public static string OpenLeaderboard()
        {
            string leaderboard = "Leaderboard\n";

            if (File.Exists("Leaderboard.txt"))
            {
                using (StreamReader reader = new StreamReader("Leaderboard.txt"))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        leaderboard += (i + 1) + "." + reader.ReadLine() + "\n";
                    }
                }
            }

            return leaderboard;
        }
    }
}
