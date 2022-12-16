using Gemmob;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : SingletonBind<GameOver>
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button btnClose;
    [SerializeField] Text score;
    [SerializeField] Text bestScore;
    private void Start()
    {
        btnClose.onClick.AddListener(TapToClose);
    }
    private void OnEnable()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
    }
    private void TapToClose()
    {
        Reload();
        gameOverPanel.SetActive(false);
    }
    public void Reload()
    {
        ScoreController.Instance.DestroyScore();
        BoardController.Instance.ReloadBoard();
        for (int i = 0; i < SpawnController.Instance.ListBlock.Count; i++)
        {
            Destroy(SpawnController.Instance.ListBlock[i].gameObject);
        }
        SpawnController.Instance.ListBlock.Clear();
    }
}
