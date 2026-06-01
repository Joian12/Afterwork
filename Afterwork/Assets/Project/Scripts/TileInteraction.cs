using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _maxDistance = 100;
    [SerializeField] private GameObject _tileSurfacePopUpEditor;
    private bool _isPopUpEditorOpen = false;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OpenTileSurfacePopUpEditor();
        }
    }
    
    public void OpenTileSurfacePopUpEditor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.nearClipPlane;
        
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask.value))
        {
            GameObject tile = hit.transform.gameObject;
            
            // open pop up for texture selection
            _tileSurfacePopUpEditor.SetActive(_isPopUpEditorOpen == false);

            _isPopUpEditorOpen = !_isPopUpEditorOpen;

            Debug.Log(tile.name);
            
        }
    }
}
