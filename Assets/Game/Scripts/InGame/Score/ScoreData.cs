using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScoreData", menuName = "Resources/ScoreData", order = 1)]
public class ScoreData : ScriptableObject
{
    [SerializeField] int[] bonus;
    [SerializeField] int[] blockItem;
    [SerializeField] int bestScore;

    private int scorePerRow = 10;

    public int BestScore { get => bestScore;}

    private void OnEnable()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
    }
    public int GetScore(int blockIndex, int row)
    {
        return bonus[row - 1] + blockItem[blockIndex] + scorePerRow * row;
    }
}
