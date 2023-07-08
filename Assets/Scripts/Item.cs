using UnityEngine;

/// <summary>
/// This is a container for sprite and item type pair 
/// </summary>

[CreateAssetMenu(fileName ="Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private ItemTypes itemType;

    public Sprite IconSprite => iconSprite;
    public ItemTypes ItemType => itemType;
}
