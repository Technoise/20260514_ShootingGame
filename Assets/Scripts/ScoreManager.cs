using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;

    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    public void AddScore()
    {
        currentScore += 1;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (currentScoreText == null)
        {
            return;
        }

        currentScoreText.text = "현재 점수 : " + currentScore;
    }
}
