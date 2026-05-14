using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private const string BestScoreKey = "BestScore";

    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text bestScoreText;

    private int currentScore;
    private int bestScore;

    private void Start()
    {
        currentScore = 0;
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        UpdateScoreText();
    }

    public void AddScore()
    {
        currentScore += 1;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveBestScore();
        }

        UpdateScoreText();
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt(BestScoreKey, bestScore);
        PlayerPrefs.Save();
    }

    private void UpdateScoreText()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "현재 점수 : " + currentScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "최고 점수 : " + bestScore;
        }
    }
}
