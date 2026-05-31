using UnityEngine;

public class TileData 
{
    public int TileID;
    public TileType TileType;
    public int TilePosX;
    public int TilePosY;
}

public enum TileType
{
    Empty = 0, //black space
    Floor = 1,
    HorizontalWall = 2,
    VerticalWallLeft = 3,
    VerticalWallRight = 4,
    HorizontalDoor = 5,
    VerticalDoorLeft = 6,
    VerticalDoorRight = 7,
    Equipment = 8,
}

public static class TemporaryRoomTileData
{
    public static readonly int[][] RoomData1 = new []
    {
        new int[] {3, 2, 5, 2, 2, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {2, 2, 2, 2, 2, 2},
    };
    
    public static readonly int[][] RoomData2 = new []
    {
        new int[] {3, 2, 5, 2, 2, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {3, 1, 1, 1, 1, 4},
        new int[] {2, 2, 2, 2, 2, 2},
    };
}
