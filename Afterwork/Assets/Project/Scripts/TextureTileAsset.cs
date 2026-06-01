using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "TileAsset", fileName = "Tile")]
public class TextureTileAsset : ScriptableObject
{
    public int TileID;
    public Texture Sprite;
    public TileSurfaceType TileSurfaceType;
}