using UnityEngine;
using UnityEngine.Tilemaps;

public enum ResourceType { Iron, Copper, None }

[CreateAssetMenu(fileName = "New Resource Tile", menuName = "Tiles/Resource Tile")]
public class ResourceTile : Tile
{
    public ResourceType type;
}
