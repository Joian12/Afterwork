using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "TileAsset", fileName = "Tile")]
public class TextureTileAsset : ScriptableObject
{
    public int TileID;
    public string TileName;
    public Texture Sprite;
    [FormerlySerializedAs("TileSurfaceType")] public InteriorObjectType interiorObjectType;
}