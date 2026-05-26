using UnityEngine;

public static class TileDataProvider
{
    public static TileData CreateTileData(int tileID, TileType tileType, int tilePosX, int tilePosY)
    {
        return new TileData
        {
            TileID = tileID,
            TileType = tileType,
            TilePosX = tilePosX,
            TilePosY = tilePosY,
            Color = Random.ColorHSV(),
        };
    }
}