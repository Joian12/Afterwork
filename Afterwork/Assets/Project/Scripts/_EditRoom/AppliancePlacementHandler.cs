using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class AppliancePlacementHandler : MonoBehaviour, IEditModeHandler
{
    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private Grid _grid;

    private Camera _camera;
    private const float MaxDistance = 100f;
    
    private GameObject _selectedPrefab;

    private GameObject _ghost;
    
    private EquipmentTileAsset _selectedEquipmentAsset;

    // Runtime tracking of what's been placed this session
    private readonly List<PlacedAppliance> _placedAppliances = new();

    private void Awake()
    {
        this._camera = Camera.main;
    }

    private void OnEnable()
    {
        TileSurfaceController.OnEquipmentSelect += SelectAppliance;
    }

    private void Update()
    {
        if (this._selectedPrefab == null || this._ghost == null)
        {
            return;
        }

        Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, this._floorMask))
        {
            Vector3Int cell = this._grid.WorldToCell(hit.point);
            Vector3 snapped = this._grid.GetCellCenterWorld(cell);
            
            this._ghost.SetActive(true);
            this._ghost.transform.position = snapped;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceAppliance(snapped);
            }
        }
        else
        {
            this._ghost.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1)) // testing
        {
            ClearSelection();
        }
    }

    public void Enable()
    {
        this.enabled = true;
    }

    public void Disable()
    {
        this.enabled = false;
        ClearGhost();
    }
    
    public void SelectAppliance(EquipmentTileAsset tileAsset)
    {
        this._selectedEquipmentAsset = tileAsset;
        this._selectedPrefab = tileAsset.TilePrefab;
        ClearGhost();

        if (tileAsset.TilePrefab == null)
            return;

        this._ghost = Instantiate(tileAsset.TilePrefab);
        MakeGhostTransparent(this._ghost, 0.4f);
    }
    
    private void PlaceAppliance(Vector3 worldPosition)
    {
        GameObject placed = Instantiate(this._selectedPrefab, worldPosition, Quaternion.identity);

        float x = worldPosition.x;
        
        var data = new PlacedApplianceData
        {
            TileID = this._selectedEquipmentAsset.TileID,
            PrefabName = this._selectedPrefab.name,
            CellX = worldPosition.x,
            CellY = worldPosition.y,
            CellZ = worldPosition.z
        };

        this._placedAppliances.Add(new PlacedAppliance { SceneObject = placed, Data = data });
        
        AppliancePersistenceManager.Instance?.AddOrUpdateAppliance(data);
    }

    private void ClearGhost()
    {
        if (this._ghost != null)
        {
            Destroy(this._ghost);
            this._ghost = null;
        }
    }

    private void ClearSelection()
    {
        this._selectedPrefab = null;
        this._selectedEquipmentAsset = null;
        ClearGhost();
    }
    
    private static void MakeGhostTransparent(GameObject ghost, float alpha)
    {
        foreach (var r in ghost.GetComponentsInChildren<Renderer>())
        {
            foreach (var mat in r.materials)
            {
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;

                // Switch Standard shader to Transparent mode
                mat.SetFloat("_Mode", 3f);
                mat.SetInt("_SrcBlend",  (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend",  (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite",    0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }
    }
    
    private sealed class PlacedAppliance
    {
        public GameObject SceneObject;
        public PlacedApplianceData Data;
    }
}