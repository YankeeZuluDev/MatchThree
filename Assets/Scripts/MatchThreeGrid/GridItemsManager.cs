using UnityEngine;

/// <summary>
/// This class is responsible for managing tile items
/// </summary>
public class GridItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Item[] items;

    public void SetRandomTileItem(Tile tile) => tile.SetTileItem(GetRandomItem());

    private Item GetRandomItem() => items[Random.Range(0, items.Length)];

    public bool AreSameItems(Item a, Item b) => a.ItemType == b.ItemType;
}
