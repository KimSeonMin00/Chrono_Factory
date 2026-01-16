using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Resource Tile", menuName = "Tiles/Resource Tile")]
public class ResourceTile : Tile
{
    public ItemData data;
}
