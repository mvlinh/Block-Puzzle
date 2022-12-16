using Gemmob;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : SingletonBind<GamePlayUI>
{
    [SerializeField] Text txtScore;
    [SerializeField] Text txtBestScore;
    [SerializeField] GameObject Home;
    [SerializeField] GameObject Pause;
    [SerializeField] GameObject GameOver;
    [SerializeField] Button btn_Pause;
  
    private void Start()
    {
        Home.SetActive(true);
        Pause.SetActive(false);
        GameOver.SetActive(false);
        btn_Pause.onClick.AddListener(OpenPausePanel);
        txtScore.text = "0";
        txtBestScore.text = PlayerPrefs.GetInt("bestScore").ToString();

        EventDispatcher.Instance.AddListener(EventKey.ScoreChanged, OnScoreChanged);
        EventDispatcher.Instance.AddListener<EventKey.ScoreChanged2>(OnScoreChanged2);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EventDispatcher.Instance.RemoveListener(EventKey.ScoreChanged, OnScoreChanged);
        EventDispatcher.Instance.RemoveListener<EventKey.ScoreChanged2>(OnScoreChanged2);
    }

    private void OnScoreChanged()
    {
        Debug.Log("Score Changed");
    }

    private void OnScoreChanged2(EventKey.ScoreChanged2 value)
    {
        txtScore.text = value.Score.ToString();
        txtBestScore.text = value.BestScore.ToString();
    }

    public void UpdateUIScore(int score, int bestScore)
    {
        txtScore.text = score.ToString();
        txtBestScore.text = bestScore.ToString();
        PlayerPrefs.SetInt("score", score);
    }
    public void OpenPausePanel()
    {
        Pause.SetActive(true);
    }
    public void OpenGameOverPanel()
    {
        GameOver.SetActive(true);
    }
}
