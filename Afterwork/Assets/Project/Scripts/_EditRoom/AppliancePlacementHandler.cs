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

    // Runtime tracking of what's been placed this session
    private readonly List<PlacedAppliance> _placedAppliances = new();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_selectedPrefab == null || _ghost == null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, _floorMask))
        {
            Vector3Int cell = _grid.WorldToCell(hit.point);
            Vector3 snapped = _grid.GetCellCenterWorld(cell);

            _ghost.SetActive(true);
            _ghost.transform.position = snapped;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceAppliance(snapped, cell);
            }
        }
        else
        {
            _ghost.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
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
    
    public void SelectAppliance(GameObject prefab)
    {
        _selectedPrefab = prefab;
        ClearGhost();

        if (prefab == null)
            return;

        _ghost = Instantiate(prefab);
        MakeGhostTransparent(_ghost, 0.4f);
    }
    
    private void PlaceAppliance(Vector3 worldPosition, Vector3Int cell)
    {
        GameObject placed = Instantiate(_selectedPrefab, worldPosition, Quaternion.identity);

        var data = new PlacedApplianceData
        {
            PrefabName = _selectedPrefab.name,
            CellX = cell.x,
            CellY = cell.y,
            CellZ = cell.z
        };

        _placedAppliances.Add(new PlacedAppliance { SceneObject = placed, Data = data });
        
        AppliancePersistenceManager.Instance?.AddOrUpdateAppliance(data);
    }

    private void ClearGhost()
    {
        if (_ghost != null)
        {
            Destroy(_ghost);
            _ghost = null;
        }
    }

    private void ClearSelection()
    {
        _selectedPrefab = null;
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