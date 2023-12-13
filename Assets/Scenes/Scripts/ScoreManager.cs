using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ScoreManager : MonoBehaviour
{
    public ScoreData sd;

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score x)
    {
        sd.scores.Add(x);
    }
}
