using System.Collections.Generic;
using UnityEngine;

//USE MVVM PLEASE
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    
    [SerializeField] private List<TileSurface> _tileSurfaces;
    [SerializeField] private TilePersistenceManager _tilePersistenceManager;
    [SerializeField] private List<TextureTileAsset> _tileAssets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeRoomTile();
    }
    
    private void InitializeRoomTile()
    {
        _tilePersistenceManager.LoadTileSurfaceData();
        
        List<TileSurfaceData> tileSurfaceDataList = _tilePersistenceManager.GetTileSurfaceDataList();

        if (tileSurfaceDataList == null || tileSurfaceDataList.Count == 0)
        {
            return;
        }
    
        foreach (var tileSurface in _tileSurfaces)
        {
            TileSurfaceData data = tileSurfaceDataList.Find(x => x.TilePos == tileSurface.TilePos);
            if (data == null)
            {
                continue;
            }
        
            TextureTileAsset asset = _tileAssets.Find(x => x.TileID == data.TileId);
        
            if (asset != null)
            {
                tileSurface.SetSurfaceTile(asset);
            }
            else
            {
                Debug.LogWarning($"Missing TextureTileAsset for surface type: {data.TileSurfaceType}");
            }
        }
    }
    
    public List<TextureTileAsset> GetTileAssets()
    {
        return _tileAssets;
    }
}

public interface IMountable
{
    TileSurfaceType SurfaceType { get; }
    int PositionX { get; }
    int PositionY { get; }
}

public sealed class DefaultMountable : IMountable
{
    public TileSurfaceType SurfaceType { get; }
    public int PositionX { get; }
    public int PositionY { get; }
}
