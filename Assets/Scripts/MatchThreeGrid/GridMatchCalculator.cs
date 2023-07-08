using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for calculating matches in board
/// </summary>
public class GridMatchCalculator : MonoBehaviour
{
    /// <summary>
    /// Min amount of tiles with same item in a row or column to be considered as a match
    /// </summary>
    [SerializeField] private int minTilesToMatch;
    [SerializeField] private int scorePerMatchingTile;

    private MatchThreeGrid matchThreeGrid;
    private GridTileSwapper gridTileSwapper;
    private GridItemsManager gridItemsManager;
    private ScoreManager scoreManager;
    private AudioManager audioManager;

    [Inject]
    private void Construct(MatchThreeGrid matchThreeGrid, GridTileSwapper gridTileSwapper, GridItemsManager gridItemsManager, ScoreManager scoreManager, AudioManager audioManager)
    {
        this.matchThreeGrid = matchThreeGrid;
        this.gridTileSwapper = gridTileSwapper;
        this.gridItemsManager = gridItemsManager;
        this.scoreManager = scoreManager;
        this.audioManager = audioManager;
    }

    /// <summary>
    /// Check if swapping will result in at least one of the tiles is matching with it`s neighbours 
    /// </summary>
    /// <returns> Returns true if at least one of the tiles matching, false if not</returns>
    public bool WillMatchAfterSwap(Tile firstTile, Tile secondTile)
    {
        // Swap
        gridTileSwapper.SwapTilesInGrid(firstTile, secondTile);

        // Check if either tiles have either horizontal or vertical matches 
        bool willMatch = CheckMatch(firstTile) || CheckMatch(secondTile);

        // Swap back
        gridTileSwapper.SwapTilesInGrid(secondTile, firstTile);

        return willMatch;
    }

    private bool CheckMatch(Tile tile)
    {
        return HasMatchInRow(tile) || HasMatchInColumn(tile);
    }

    private bool HasMatchInRow(Tile tile)
    {
        int leftColumn = 0, rightColumn = 1;

        // Iterate while end of the row is not reached 
        while (rightColumn < matchThreeGrid.Columns)
        {
            // Shift window if start tile and end tiles are matching
            while (rightColumn < matchThreeGrid.Columns && gridItemsManager.AreSameItems(matchThreeGrid.TileGrid[tile.Row, leftColumn].Item, matchThreeGrid.TileGrid[tile.Row, rightColumn].Item))
            {
                rightColumn++;
            }

            // Return true if number of matching tiles >= minTilesToMatch 
            if (rightColumn - leftColumn >= minTilesToMatch)
            {
                return true;
            }

            // Update sliding window
            leftColumn = rightColumn;
            rightColumn = leftColumn + 1;
        }

        // Return false if no matches was found
        return false;
    }

    private bool HasMatchInColumn(Tile tile)
    {
        int leftRow = 0, rightRow = 1;

        // Iterate while end of the column is not reached 
        while (rightRow < matchThreeGrid.Columns)
        {
            // Shift window if start tile and end tile are matching
            while (rightRow < matchThreeGrid.Columns && gridItemsManager.AreSameItems(matchThreeGrid.TileGrid[leftRow, tile.Col].Item, matchThreeGrid.TileGrid[rightRow, tile.Col].Item))
            {
                rightRow++;
            }

            // Add to matches list if number of matching tiles >= minTilesToMatch
            if (rightRow - leftRow >= minTilesToMatch)
            {
                return true;
            }

            // Update sliding window
            leftRow = rightRow;
            rightRow = leftRow + 1;
        }

        return false;
    }

    /// <summary>
    /// Traverse entire board and calculate matches using sliding window technique 
    /// </summary>
    private HashSet<Tile> CalculateMatchingTiles()
    {
        HashSet<Tile> matches = new();

        #region Calculte horizontally-matching tiles

        // Iterate through every row
        for (int r = 0; r < matchThreeGrid.Rows; r++)
        {
            int leftColumn = 0, rightColumn = 1;

            // Iterate while end of the row is not reached 
            while (rightColumn < matchThreeGrid.Columns)
            {
                // Shift window if start tile and end tiles are matching
                while (rightColumn < matchThreeGrid.Columns && gridItemsManager.AreSameItems(matchThreeGrid.TileGrid[r, leftColumn].Item, matchThreeGrid.TileGrid[r, rightColumn].Item))
                {
                    rightColumn++;
                }

                // Add to matches list if number of matching tiles >= minTilesToMatch 
                if (rightColumn - leftColumn >= minTilesToMatch)
                {
                    for (int i = leftColumn; i < rightColumn; i++)
                    {
                        matches.Add(matchThreeGrid.TileGrid[r, i]);
                    }
                }

                // Update sliding window
                leftColumn = rightColumn;
                rightColumn = leftColumn + 1;
            }
        }

        #endregion

        #region Calculate vertically-matching tiles

        // Iterate through every column
        for (int c = 0; c < matchThreeGrid.Columns; c++)
        {
            int leftRow = 0, rightRow = 1;

            // Iterate while end of the column is not reached 
            while (rightRow < matchThreeGrid.Columns)
            {
                // Shift window if start tile and end tile are matching
                while (rightRow < matchThreeGrid.Columns && gridItemsManager.AreSameItems(matchThreeGrid.TileGrid[leftRow, c].Item, matchThreeGrid.TileGrid[rightRow, c].Item))
                {
                    rightRow++;
                }

                // Add to matches list if number of matching tiles >= minTilesToMatch
                if (rightRow - leftRow >= minTilesToMatch)
                {
                    for (int i = leftRow; i < rightRow; i++)
                    {
                        matches.Add(matchThreeGrid.TileGrid[i, c]);
                    }
                }

                // Update sliding window
                leftRow = rightRow;
                rightRow = leftRow + 1;
            }
        }

        #endregion

        return matches;
    }

    public IEnumerator ReplaceMatchingTiles(float tileChangeScaleDuration, bool shouldAddScore)
    {
        // Calculate unique horizontal and vertical matches
        HashSet<Tile> matches = CalculateMatchingTiles();

        // Exit if there is no matches
        if (matches.Count == 0) yield break;

        // Play match audio effect
        audioManager.PlaySFX(AudioID.Match);

        // Shrink matching tiles scale
        yield return Tweens.ChangeTilesScale(matches, Vector2.one, Vector2.zero, tileChangeScaleDuration);

        // Set new random item for all matching tiles and add score
        foreach (Tile tile in matches)
        {
            gridItemsManager.SetRandomTileItem(tile);

            if (shouldAddScore)
                scoreManager.AddScore(scorePerMatchingTile);
        }

        // Expand matching tiles scale
        yield return Tweens.ChangeTilesScale(matches, Vector2.zero, Vector2.one, tileChangeScaleDuration);

        // Repeat untill there is no more mathes
        yield return ReplaceMatchingTiles(tileChangeScaleDuration, shouldAddScore);
    }
}
