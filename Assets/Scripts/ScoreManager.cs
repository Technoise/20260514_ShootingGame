using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text bestScoreText;

    private int currentScore;
    private int bestScore;

    private void Start()
    {
        currentScore = 0;
        bestScore = 0;
        UpdateScoreText();
    }

    public void AddScore()
    {
        currentScore += 1;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }

        UpdateScoreText();
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
