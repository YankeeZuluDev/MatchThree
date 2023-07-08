using UnityEngine;
using Zenject;

/// <summary>
/// A class for the tile, that contains tile`s row and col in the grid and an item
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconSpriteRenderer;

    private int col;
    private int row;
    private Item item;
    private IInputHandler inputHandler;

    public int Col => col;
    public int Row => row;
    public Item Item => item;

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        this.inputHandler = inputHandler;
    }

    public void SetTileItem(Item item)
    {
        // Set tile item
        this.item = item;

        // Set tile icon sprite
        iconSpriteRenderer.sprite = item.IconSprite;
    }

    public void SetTileIndicies(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void OnMouseDown() => inputHandler.PressTile(this);

    private void OnMouseEnter() => inputHandler.EnterTile(this);

    private void OnMouseUp() => inputHandler.ResetSelection();
}
