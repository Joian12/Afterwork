using UnityEngine;

public static class TileDataProvider
{
    public static TileData CreateTileData(int tileID, TileType tileType)
    {
        return new TileData
        {
            TileID = tileID,
            TileType = tileType,
        };
    }
}