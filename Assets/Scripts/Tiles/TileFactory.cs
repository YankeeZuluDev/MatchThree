using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for spawning tiles and initializing
/// </summary>
public class TileFactory : IFactory<GameObject, (int row, int column), Tile>
{
    private DiContainer container;
    private MatchThreeGrid matchThreeGrid;
    private GridItemsManager gridItemsManager;

    [Inject]
    public TileFactory(DiContainer container, MatchThreeGrid matchThreeGrid, GridItemsManager gridItemsManager)
    {
        this.container = container;
        this.matchThreeGrid = matchThreeGrid;
        this.gridItemsManager = gridItemsManager;
    }

    public Tile Create(GameObject tilePrefab, (int row, int column) indecies)
    {
        Tile tile = container.InstantiatePrefabForComponent<Tile>(
            tilePrefab, 
            matchThreeGrid.GetTilePosition(indecies.row, indecies.column), 
            Quaternion.identity, 
            matchThreeGrid.transform);

        gridItemsManager.SetRandomTileItem(tile);

        tile.SetTileIndicies(indecies.row, indecies.column);

        // Add tile to grid
        matchThreeGrid.TileGrid[indecies.row, indecies.column] = tile;

        return tile;
    }
}
