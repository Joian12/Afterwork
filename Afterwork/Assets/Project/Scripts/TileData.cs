using UnityEngine;

public class TileData 
{
    public int TileID;
    public TileType TileType;
    public int TilePosX;
    public int TilePosY;
    public Color Color; // testing
}

public enum TileType
{
    Empty = 0, //black space
    Floor = 1,
    HorizontalWall = 2,
    VerticalWallLeft = 3,
    VerticalWallRight = 4,
    Door = 5,
    Equipment = 6,
}

public static class TemporaryTileData
{
    public static int[][] RoomData = new []
    {
        new int[] {3, 2, 2, 2, 2, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {2, 2, 2, 2, 2, 2},
    };
}
