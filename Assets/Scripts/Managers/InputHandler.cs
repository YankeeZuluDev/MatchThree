using UnityEngine;
using Zenject;

/// <summary>
/// 
/// </summary>
public class InputHandler : MonoBehaviour, IInputHandler
{
    private GridTileSwapper gridTileSwapper;
    private Tile selectedTile;

    [Inject]
    private void Construct(GridTileSwapper gridTileSwapper)
    {
        this.gridTileSwapper = gridTileSwapper;
    }

    public void PressTile(Tile tile)
    {
        if (gridTileSwapper.IsSwapping) return;
        
        selectedTile = tile;
    }

    public void EnterTile(Tile tile)
    {
        if (selectedTile == null || gridTileSwapper.IsSwapping || selectedTile == tile) return;

        StartCoroutine(gridTileSwapper.Swap(selectedTile, tile, ResetSelection));
    }

    public void ResetSelection() => selectedTile = null;
}
