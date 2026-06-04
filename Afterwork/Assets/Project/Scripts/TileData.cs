using UnityEngine;

public class TileData 
{
    public int TileID;
    public TileSurfaceType TileSurfaceType;
}

public enum TileSurfaceType
{
    Wall = 0,
    Floor = 1,
    Door = 3,
    Window = 4,
}
