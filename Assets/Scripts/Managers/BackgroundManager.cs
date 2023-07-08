using UnityEngine;

/// <summary>
/// This class is responsible for setting random background
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backGroundSpriteRenderer;
    [SerializeField] private Sprite[] backgrounds;

    private void Start() => SetRandomBackground();

    private void SetRandomBackground() => backGroundSpriteRenderer.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
}
