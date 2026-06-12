using UnityEngine;

public abstract class TileAsset : ScriptableObject
{
    public Texture Sprite;
    public int TileID;
    public string TileName;
    public InteriorObjectType interiorObjectType;
}