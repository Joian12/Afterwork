using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

//USE MVVM PLEASE
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    
    [SerializeField] private List<TileSurface> _tileSurfaces;
    [SerializeField] private TilePersistenceManager _tilePersistenceManager;
    [SerializeField] private List<TileAsset> _tileAssets;
    
    [SerializeField] private NavMeshSurface _navMeshSurface;
    
    public static TileSurface SelectedTileSurface;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitializeRoom());
    }

    private IEnumerator InitializeRoom()
    {
        yield return UpdateTilePositionID();

        yield return InitializeRoomTile();
        
        yield return InitializeEquipmentPlacement();

        yield return UpdateNavMesh();;
    }

    private IEnumerator UpdateTilePositionID()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < _tileSurfaces.Count; i++)
        {
            this._tileSurfaces[i].SetTilePosition(i);
        }
    }
    
    private IEnumerator UpdateNavMesh()
    {
        yield return new WaitForEndOfFrame();

        this._navMeshSurface.BuildNavMesh();
    }

    private IEnumerator InitializeEquipmentPlacement()
    {
        yield return new WaitForEndOfFrame();

        AppliancePersistenceManager.Instance.LoadAppliances();
        
        List<PlacedApplianceData> appliances = AppliancePersistenceManager.Instance.GetAppliances();
        
        if (appliances == null || appliances.Count == 0)
        {
            yield break;
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
    
    private IEnumerator InitializeRoomTile()
    {
        yield return new WaitForEndOfFrame();

        this._tilePersistenceManager.LoadTileSurfaceData();
        
        List<TileSurfaceData> tileSurfaceDataList = this._tilePersistenceManager.GetTileSurfaceDataList();

        if (tileSurfaceDataList == null || tileSurfaceDataList.Count == 0)
        {
            yield break;
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
