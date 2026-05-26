using UnityEngine;

public class TileData 
{
    public int TileID;
    public TileType TileType;
    public int TilePosX;
    public int TilePosY;
    public Color Color;
}

public enum TileType
{
    Empty,
    Wall,
    Door,
    Equipment,
}