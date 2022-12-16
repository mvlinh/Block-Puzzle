using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : SingletonBind<ScoreController>
{
    public ScoreData Data;
    private int score = 0;
    private int bestScore;
    public int Score { get => score;}

    private void Start()
    {
        bestScore = Data.BestScore;
    }
    public void UpdateScore(int blockIndex, int row)
    {
        score += Data.GetScore(blockIndex, row);
        bestScore = bestScore > score ? bestScore : score;
        PlayerPrefs.SetInt("bestScore", bestScore);
        EventDispatcher.Instance.Dispatch(new EventKey.ScoreChanged2() { Score = score, BestScore = bestScore });

        EventDispatcher.Instance.Dispatch(EventKey.ScoreChanged);

    }
    public void DestroyScore()
    {
        score = 0;
        GamePlayUI.Instance.UpdateUIScore(score, bestScore);

    }
}
