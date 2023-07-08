using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tweens
{
    public static IEnumerator Swap(Transform transform1, Transform transform2, float swapDuration, System.Action onComplete)
    {
        Vector2 position1 = transform1.position;
        Vector2 position2 = transform2.position;
        float elapsedTime = 0f;

        while (elapsedTime < swapDuration)
        {
            float t = elapsedTime / swapDuration;

            // Swap positions
            transform1.position = Vector2.Lerp(position1, position2, t);
            transform2.position = Vector2.Lerp(position2, position1, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure, that position is set at the end of the tween
        transform1.position = position2;
        transform2.position = position1;

        onComplete?.Invoke();
    }

    /// <summary>
    /// Change the scale of each tile in in a list of tiles from intitial scale to finalScale  
    /// </summary>
    public static IEnumerator ChangeTilesScale(ICollection<Tile> tiles, Vector2 initialScale, Vector2 finalScale, float scaleDuration)
    {
        float elapsedTime = 0;

        while (elapsedTime < scaleDuration)
        {
            // Change each tiles' scale simultaniously
            foreach (Tile tile in tiles)
            {
                tile.transform.localScale = Vector2.Lerp(initialScale, finalScale, elapsedTime / scaleDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure, that each tiles' scale is set to finalScale
        foreach (Tile tile in tiles)
        {
            tile.transform.localScale = finalScale;
        }
    }

    public static IEnumerator ChangeRectTransformScale(RectTransform rectTransform, Vector2 initialScale, Vector2 finalScale, float scaleDuration)
    {
        float elapsedTime = 0;

        while (elapsedTime < scaleDuration)
        {
            rectTransform.localScale = Vector2.Lerp(initialScale, finalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = finalScale;
    }

    public static IEnumerator RotateRectTransform(RectTransform rectTransform, float duration, float rotationSpeed, System.Action onComplete)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onComplete?.Invoke();
    }
}
