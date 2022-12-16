using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventKey
{
    public const int ScoreChanged = 100;
    public const int CoinChanged = 101;

    public struct ScoreChanged2 : IEventParams
    {
        public int Score;
        public int BestScore;
    }
}
