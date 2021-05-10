using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveStateController
{

    private static string saveFilePath = "/highscore.save";

    public static void SaveToFile(int highScore)
    {
        File.WriteAllText(Application.dataPath + saveFilePath, highScore.ToString());
    }

    public static int LoadFromFile()
    {
        if (File.Exists(Application.dataPath + saveFilePath))
        {
            string lastHighscore = File.ReadAllText(Application.dataPath + saveFilePath);
            int highScoreNumber = 0;
            int.TryParse(lastHighscore, out highScoreNumber);
            return highScoreNumber;
        }
        else
        {
            return 0;
        }
    }
}
