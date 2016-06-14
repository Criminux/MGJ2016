﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Highscoremanager
{

    public Highscoremanager()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        if (!File.Exists(Application.dataPath + "/high.score"))
        {
            highscore = 0;
            SaveHighscore();
            return;
        }

        using (FileStream stream = File.OpenRead(Application.dataPath + "/high.score"))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string input = reader.ReadToEnd();
                int value = 0;
                if (int.TryParse(input, out value))
                    highscore = value;
            }
        }
    }

    private int highscore;

    public int Highscore
    {
        get { return highscore; }
    }

    public bool IsHighscoreGreater(int input)
    {
        return highscore > input;
    }

    public void SetHigherScore(int input)
    {
        if (IsHighscoreGreater(input))
            return;

        highscore = input;
    }

    public void SaveHighscore()
    {
        using (FileStream stream = File.OpenWrite(Application.dataPath + "/high.score"))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(highscore);
            }
        }
    }
}