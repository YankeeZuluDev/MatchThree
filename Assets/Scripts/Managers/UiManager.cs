using TMPro;
using UnityEngine;

/// <summary>
/// This class is responsible for setting UI text
/// </summary>
public class UiManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start() => canvas.worldCamera = Camera.main;

    public void SetTotalScoreText(int totalScore) => totalScoreText.text = totalScore.ToString();

    public void SetCurrentScoreText(int currentScore, int levelGoalScore) => currentScoreText.text = $"{currentScore} / {levelGoalScore}";

    public void SetLevelText(int level) => levelText.text = $"Level {level}";
}
