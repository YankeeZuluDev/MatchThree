using UnityEngine;

/// <summary>
/// This class is responsible for setting the target framerate of the game
/// </summary>
public class TargetFrameRateSetter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate;

    private void Awake() => Application.targetFrameRate = targetFrameRate;
}
