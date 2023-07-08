using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for loading, saving and adding the score
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int minLevelGoalScore;
    [SerializeField] private int maxLevelGoalScore;
    [SerializeField] private GameEvent onLevelCompleteEvent;

    private int totalScore;
    private int currentScore;
    private int levelGoalScore;
    private UiManager uIManager;
    private AudioManager audioManager;

    [Inject]
    private void Construct(UiManager uIManager, AudioManager audioManager)
    {
        this.uIManager = uIManager;
        this.audioManager = audioManager;
    }

    private void Start()
    {
        totalScore = LoadTotalScore();
        ResetCurrentScoreAndLevelGoal();
    }

    private int LoadTotalScore() => PlayerPrefs.GetInt("Score", 0);

    private void SaveTotalScore() => PlayerPrefs.SetInt("Score", totalScore);

    public void AddScore(int amount)
    {
        totalScore += amount;
        currentScore += amount;

        UpdateScoreUI();

        if (currentScore >= levelGoalScore)
        {
            ResetCurrentScoreAndLevelGoal();
            SaveTotalScore();
            audioManager.PlaySFX(AudioID.LevelComplete);
            onLevelCompleteEvent.Raise();
        }
    }

    private int GetRandomLevelGoalScore() => Random.Range(minLevelGoalScore, maxLevelGoalScore);

    private void UpdateScoreUI()
    {
        uIManager.SetTotalScoreText(totalScore);
        uIManager.SetCurrentScoreText(currentScore, levelGoalScore);
    }

    private void ResetCurrentScoreAndLevelGoal()
    {
        currentScore = 0;
        levelGoalScore = GetRandomLevelGoalScore();
        UpdateScoreUI();
    }
}
