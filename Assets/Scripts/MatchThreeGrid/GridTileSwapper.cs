using System.Collections;
using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for swapping tiles in board
/// </summary>
public class GridTileSwapper : MonoBehaviour
{
    [SerializeField] private float swapDuration;
    [SerializeField] private float tileChangeScaleDuration;

    private MatchThreeGrid matchThreeGrid;
    private GridMatchCalculator gridMatchCalculator;
    private bool isSwapping;

    public bool IsSwapping => isSwapping;

    [Inject]
    private void Construct(MatchThreeGrid matchThreeGrid, GridMatchCalculator gridMatchCalculator)
    {
        this.matchThreeGrid = matchThreeGrid;
        this.gridMatchCalculator = gridMatchCalculator;
    }

    public void SwapTilesInGrid(Tile firstTile, Tile secondTile)
    {
        // Swap tiles in grid
        Tile tempTile = matchThreeGrid.TileGrid[secondTile.Row, secondTile.Col];
        matchThreeGrid.TileGrid[secondTile.Row, secondTile.Col] = matchThreeGrid.TileGrid[firstTile.Row, firstTile.Col];
        matchThreeGrid.TileGrid[firstTile.Row, firstTile.Col] = tempTile;

        // Swap tiles indicies
        int r = secondTile.Row, c = secondTile.Col;
        secondTile.SetTileIndicies(firstTile.Row, firstTile.Col);
        firstTile.SetTileIndicies(r, c);
    }

    public IEnumerator Swap(Tile firstTile, Tile secondTile, System.Action onTileSwapped)
    {
        if (!matchThreeGrid.AreNeighbours(firstTile, secondTile))
        {
            // Exit if tiles are not neighbours
            yield break;
        }

        isSwapping = true; // state machine instead of flags. One more state: calculating matches

        // Swap tile gameobjects
        yield return Tweens.Swap(firstTile.transform, secondTile.transform, swapDuration, onTileSwapped);

        // Check if swap will result in at least one of the tiles matching with it`s neighbours
        if (!gridMatchCalculator.WillMatchAfterSwap(firstTile, secondTile))
        {
            // Swap back if swap is invalid and exit
            yield return Tweens.Swap(firstTile.transform, secondTile.transform, swapDuration, onTileSwapped);
            isSwapping = false;
            yield break;
        }

        // Swap in the grid if swap if valid
        SwapTilesInGrid(firstTile, secondTile);

        yield return gridMatchCalculator.ReplaceMatchingTiles(tileChangeScaleDuration, true);

        isSwapping = false;
    }
}
