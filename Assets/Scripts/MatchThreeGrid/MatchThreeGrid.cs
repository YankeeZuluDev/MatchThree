using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for initializeing and populating the board
/// </summary>
public class MatchThreeGrid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileScaleMultiplier;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    private Tile[,] tileGrid;
    private GridMatchCalculator gridMatchCalculator;
    private TileFactory tileFactory;

    public Tile[,] TileGrid => tileGrid;
    public int Rows => rows;
    public int Columns => columns;

    [Inject]
    private void Construct(GridMatchCalculator gridMatchCalculator, TileFactory tileFactory)
    {
        this.gridMatchCalculator = gridMatchCalculator;
        this.tileFactory = tileFactory;
    }

    private void Start()
    {
        tileGrid = new Tile[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                tileFactory.Create(tilePrefab, (r, c));
            }
        }

        // Allign grid with camera
        AllignGridWithCamera();

        // Replace until there is no more matches on the board
        StartCoroutine(gridMatchCalculator.ReplaceMatchingTiles(0, false));
    }

    #region Util

    /// <summary>
    /// Check if tiles a and b are neighbouring vertically or horizontally
    /// </summary>
    public bool AreNeighbours(Tile a, Tile b) 
    {
        return Mathf.Abs(a.Row - b.Row) + Mathf.Abs(a.Col - b.Col) == 1;
    }

    public Vector2 GetTilePosition(int r, int c) => new Vector2(r, c) * tileScaleMultiplier;

    private void AllignGridWithCamera()
    {
        int midRow = (rows - 1) / 2;
        int midColumn = (columns - 1) / 2;

        transform.position = -GetTilePosition(midRow, midColumn);
    }

    #endregion
}
