using UnityEngine;

/// <summary>
/// This class is responsible for scaling a spite to match the screen size
/// </summary>

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteScreenSizeScaler : MonoBehaviour
{
    private void Start() => InitializeSpriteSizeScaler();

    /// <summary>
    /// Scale sprite to match screen size 
    /// </summary>
    private void InitializeSpriteSizeScaler()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(screenWidth / spriteWidth, screenHeight / spriteHeight, transform.localScale.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
}
