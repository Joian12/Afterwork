using UnityEngine;

public static class TileDataProvider
{
    public static TileData CreateTileData(int tileID, TileSurfaceType tileSurfaceType)
    {
        return new TileData
        {
            TileID = tileID,
            TileSurfaceType = tileSurfaceType,
        };
    }
}