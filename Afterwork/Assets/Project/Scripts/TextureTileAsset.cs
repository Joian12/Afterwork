using UnityEngine;

[CreateAssetMenu(menuName = "TileAsset", fileName = "Tile")]
public class TextureTileAsset : ScriptableObject
{
    public int TileID;
    public Texture Sprite;
    public TileType TileType;
}