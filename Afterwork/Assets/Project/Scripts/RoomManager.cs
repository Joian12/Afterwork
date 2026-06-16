using System.Collections.Generic;
using UnityEngine;

//USE MVVM PLEASE
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    
    [SerializeField] private List<TileSurface> _tileSurfaces;
    [SerializeField] private TilePersistenceManager _tilePersistenceManager;
    [SerializeField] private List<TileAsset> _tileAssets;
    
    public static TileSurface SelectedTileSurface;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeRoomTile();
        InitializeEquipmentPlacement();
    }

    private void InitializeEquipmentPlacement()
    {
        AppliancePersistenceManager.Instance.LoadAppliances();
        
        List<PlacedApplianceData> appliances = AppliancePersistenceManager.Instance.GetAppliances();
        
        if (appliances == null || appliances.Count == 0)
        {
            return;
        }

        foreach (PlacedApplianceData appliance in appliances)
        {
            EquipmentTileAsset equipmentAsset = GetTileAssets().Find(x => x.TileID == appliance.TileID && x is EquipmentTileAsset) as EquipmentTileAsset;
            
            if (equipmentAsset == null)
            {
                continue;
            }
            
            Vector3 worldPosition = new Vector3(appliance.CellX, 0, appliance.CellZ);
            
            Instantiate(equipmentAsset.TilePrefab, worldPosition, Quaternion.identity);
        }
    }
    
    private void InitializeRoomTile()
    {
        this._tilePersistenceManager.LoadTileSurfaceData();
        
        List<TileSurfaceData> tileSurfaceDataList = this._tilePersistenceManager.GetTileSurfaceDataList();

        if (tileSurfaceDataList == null || tileSurfaceDataList.Count == 0)
        {
            return;
        }
    
        foreach (var tileSurface in this._tileSurfaces)
        {
            TileSurfaceData data = tileSurfaceDataList.Find(x => x.TilePos == tileSurface.TilePos);
            if (data == null)
            {
                continue;
            }
        
            TileAsset asset = this._tileAssets.Find(x => x.TileID == data.TileId && x.interiorObjectType == data.interiorObjectType);
        
            if (asset != null)
            {
                tileSurface.SetSurfaceTile(asset);
            }
            else
            {
                Debug.LogWarning($"Missing TextureTileAsset for surface type: {data.interiorObjectType}");
            }
        }
    }
    
    public List<TileAsset> GetTileAssets()
    {
        return _tileAssets;
    }
}

public interface IMountable
{
    InteriorObjectType SurfaceType { get; }
    int PositionX { get; }
    int PositionY { get; }
}

public sealed class DefaultMountable : IMountable
{
    public InteriorObjectType SurfaceType { get; }
    public int PositionX { get; }
    public int PositionY { get; }
}
