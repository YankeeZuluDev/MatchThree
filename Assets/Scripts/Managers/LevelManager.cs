using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for loading, saving and changing the level
/// </summary>
public class LevelManager : MonoBehaviour
{
    private UiManager uIManager;

    private int level;

    [Inject]
    private void Construct(UiManager uIManager)
    {
        this.uIManager = uIManager;
    }

    private void Start()
    {
        level = LoadLevel();
        UpdateLevelUI();
    }

    public int LoadLevel() => PlayerPrefs.GetInt("Level", 0);

    public void SaveLevel() => PlayerPrefs.SetInt("Level", level);

    private void UpdateLevelUI() => uIManager.SetLevelText(level);

    public void NextLevel()
    {
        level++;
        UpdateLevelUI();
    }
}
