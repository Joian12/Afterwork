using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

//USE MVVM PLEASE
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public static TileSurface SelectedTileSurface;

    [SerializeField] private List<TileSurface> _tileSurfaces;
    [SerializeField] private TilePersistenceManager _tilePersistenceManager;
    [SerializeField] private List<TileAsset> _tileAssets;

    [SerializeField] private NavMeshSurface _navMeshSurface;

    private NavMeshData _navMeshData;

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
        yield return UpdateTilePositionID(); // temporary solution sorry

        yield return InitializeRoomTile();

        yield return InitializeEquipmentPlacement();

        yield return BuildNavMesh();
    }

    private IEnumerator UpdateTilePositionID()
    {
        yield return new WaitForEndOfFrame();

        for (var i = 0; i < this._tileSurfaces.Count; i++)
        {
            this._tileSurfaces[i].SetTilePosition(i);
        }
    }

    private IEnumerator BuildNavMesh()
    {
        yield return new WaitForEndOfFrame();

        this._navMeshSurface.BuildNavMesh();
    }

    private IEnumerator InitializeEquipmentPlacement()
    {
        yield return new WaitForEndOfFrame();

        AppliancePersistenceManager.Instance.LoadAppliances();

        var appliances = AppliancePersistenceManager.Instance.GetAppliances();

        if (appliances == null || appliances.Count == 0)
        {
            yield break;
        }

        foreach (var appliance in appliances)
        {
            var equipmentAsset =
                GetTileAssets()
                    .Find(x => x.TileID == appliance.TileID && x is EquipmentTileAsset) as EquipmentTileAsset;

            if (equipmentAsset == null) continue;

            var worldPosition = new Vector3(appliance.CellX, 0, appliance.CellZ);

            Instantiate(equipmentAsset.TilePrefab, worldPosition, Quaternion.identity);
        }
    }

    private IEnumerator InitializeRoomTile()
    {
        yield return new WaitForEndOfFrame();

        this._tilePersistenceManager.LoadTileSurfaceData();

        var tileSurfaceDataList = this._tilePersistenceManager.GetTileSurfaceDataList();

        if (tileSurfaceDataList == null || tileSurfaceDataList.Count == 0)
        {
            yield break;
        }

        foreach (var tileSurface in this._tileSurfaces)
        {
            var data = tileSurfaceDataList.Find(x => x.TilePos == tileSurface.TilePos);
            if (data == null)
            {
                continue;
            }

            var asset = this._tileAssets.Find(x =>
                x.TileID == data.TileId && x.interiorObjectType == data.interiorObjectType);

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
        return this._tileAssets;
    }
}