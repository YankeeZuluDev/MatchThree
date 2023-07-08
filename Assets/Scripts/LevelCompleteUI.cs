using System.Collections;
using UnityEngine;

/// <summary>
/// This class is responsible for animating level complete UI and invoking new level event when animation is completed
/// </summary>
public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private RectTransform levelCompleteTextRectTransform;
    [SerializeField] private float changeScaleDuration;
    [SerializeField] private GameEvent startNewLevelEvent;

    public void AnimateLevelCompleteUI()
    {
        StartCoroutine(AnimateLevelCompleteTextAndStartNewLevel());
    }

    private IEnumerator AnimateLevelCompleteTextAndStartNewLevel()
    {
        yield return Tweens.ChangeRectTransformScale(levelCompleteTextRectTransform, Vector2.zero, Vector2.one, changeScaleDuration);
        yield return Tweens.ChangeRectTransformScale(levelCompleteTextRectTransform, Vector2.one, Vector2.zero, changeScaleDuration);
        
        startNewLevelEvent?.Raise();
    }
}
