using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class AppliancePlacementHandler : MonoBehaviour, IEditModeHandler
{
    [SerializeField] private LayerMask _floorMask;

    private Camera _camera;
    private const float MaxDistance = 100f;
    private const float GhostAlpha = 0.4f;
    
    private GameObject _selectedPrefab;
    private GameObject _ghost;
    private EquipmentTileAsset _selectedEquipmentAsset;

    private readonly List<PlacedAppliance> _placedAppliances = new();
    private List<Vector3> _allOccupiedSpaces = new();
    private readonly List<GhostMaterialData> _ghostMaterials = new();

    private struct GhostMaterialData
    {
        public Material Material;
        public Color OriginalColor;
    }

    private void Awake()
    {
        this._camera = Camera.main;
    }

    private void OnEnable()
    {
        TileSelectionController.OnEquipmentSelect += SelectAppliance;
        
        AppliancePersistenceManager.Instance.LoadAppliances();
        this._allOccupiedSpaces = AppliancePersistenceManager.Instance.GetAppliances().ConvertAll(x => new Vector3(x.CellX, 0, x.CellZ));
    }

    private void OnDisable()
    {
        TileSelectionController.OnEquipmentSelect -= SelectAppliance;
    }

    private void Update()
    {
        if (this._selectedPrefab == null || this._ghost == null) return;

        HandleGhostPlacement();
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

        if (tileAsset.TilePrefab == null) return;

        this._ghost = Instantiate(tileAsset.TilePrefab);
        InitializeGhostMaterials(this._ghost);
    }

    private void HandleGhostPlacement()
    {
        Ray ray = this._camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, MaxDistance, this._floorMask))
        {
            this._ghost.SetActive(false);
            CheckForCancellation();
            return;
        }

        this._ghost.SetActive(true);
    
        Vector3 targetLocalPos = hit.transform.localPosition;
        
        Vector3 ghostPosition = new Vector3(targetLocalPos.x, 0f, targetLocalPos.z);
    
        this._ghost.transform.position = ghostPosition;
    
        bool isOccupied = this._allOccupiedSpaces.Contains(targetLocalPos);
        UpdateGhostVisuals(isOccupied);

        if (Input.GetMouseButtonDown(0) && isOccupied == false && EventSystem.current.IsPointerOverGameObject() == false)
        {
            PlaceAppliance(targetLocalPos);
        }

        CheckForCancellation();
    }

    private void CheckForCancellation()
    {
        if (Input.GetMouseButtonDown(1)) // testing
        {
            ClearSelection();
        }
    }
    
    private void PlaceAppliance( Vector3 cellPosition)
    {
        GameObject placed = Instantiate(this._selectedPrefab, cellPosition, Quaternion.identity);
        
        var data = new PlacedApplianceData
        {
            TileID = this._selectedEquipmentAsset.TileID,
            PrefabName = this._selectedPrefab.name,
            CellX = cellPosition.x,
            CellZ = cellPosition.z,
            CellY = cellPosition.y,
        };

        this._placedAppliances.Add(new PlacedAppliance { SceneObject = placed, Data = data });
        this._allOccupiedSpaces.Add(cellPosition);
        
        AppliancePersistenceManager.Instance?.AddOrUpdateAppliance(data);
    }

    private void InitializeGhostMaterials(GameObject ghost)
    {
        this._ghostMaterials.Clear();

        foreach (var renderer1 in ghost.GetComponentsInChildren<Renderer>())
        {
            foreach (var mat in renderer1.materials)
            {
                mat.SetFloat("_Mode", 3f);
                mat.SetInt("_SrcBlend",  (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend",  (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite",    0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                this._ghostMaterials.Add(new GhostMaterialData
                {
                    Material = mat,
                    OriginalColor = mat.color
                });
            }
        }
        
        UpdateGhostVisuals(isOccupied: false);
    }

    private void UpdateGhostVisuals(bool isOccupied)
    {
        foreach (var ghostMat in this._ghostMaterials)
        {
            if (isOccupied)
            {
                ghostMat.Material.color = new Color(1f, 0f, 0f, GhostAlpha);
            }
            else
            {
                Color normalColor = ghostMat.OriginalColor;
                normalColor.a = GhostAlpha;
                ghostMat.Material.color = normalColor;
            }
        }
    }

    private void ClearGhost()
    {
        if (this._ghost != null)
        {
            Destroy(this._ghost);
            this._ghost = null;
        }

        this._ghostMaterials.Clear();
    }

    private void ClearSelection()
    {
        this._selectedPrefab = null;
        this._selectedEquipmentAsset = null;
        ClearGhost();
    }
    
    private sealed class PlacedAppliance
    {
        public GameObject SceneObject;
        public PlacedApplianceData Data;
    }
}