using System;
using System.IO;

public class Scoreboard
{
    private FileStream _fileStream;
    private string SavePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "scoreboard.txt");

    private readonly int[] scores = new int[10];
    public int[] Scores { get { return scores; } }

    public Scoreboard()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }

        if (File.Exists(SavePath))
        {
            string[] lines = File.ReadAllLines(SavePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (int.TryParse(lines[i], out int score))
                {
                    scores[i] = score;
                }
            }
        }
    }

    public void AddScore(int score)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (score > scores[i])
            {
                for (int j = scores.Length - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }
                scores[i] = score;
                break;
            }
        }
        SaveScores();
    }

    public void SaveScores()
    {
        File.WriteAllLines(SavePath, Array.ConvertAll(scores, s => s.ToString()));
    }
}
